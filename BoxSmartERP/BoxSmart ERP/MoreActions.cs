using BoxSmart_ERP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BoxSmart_ERP.Services.PostgreSQLServices;

namespace BoxSmart_ERP
{
    public partial class MoreActions : Form
    {
        // P/Invoke declarations for moving the window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private readonly PostgreSQLServices _dbService;
        private readonly IConfiguration _config;
        private string _requisitionNumber;

        private readonly string pgsqlHost;
        private readonly string pgsqlPort;
        private readonly string pgsqlDatabase;
        private readonly string pgsqlErrorDetails;        

        public MoreActions(IConfiguration config, string requisitionNumber)
        {
            InitializeComponent();
            _config = config;
            _requisitionNumber = requisitionNumber;
            pgsqlHost = _config["ConnectionStrings:PostgreSQLHost"];
            pgsqlPort = _config["ConnectionStrings:PostgreSQLPort"];
            pgsqlDatabase = _config["ConnectionStrings:PostgreSQLDatabase"];
            pgsqlErrorDetails = _config["ConnectionStrings:ErrorDetails"];

            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(Config.PostgreSQLPassword);
            string plainTextPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            Config.PostgreSQLConnection = $"Host={pgsqlHost};Port={pgsqlPort};Username={Config.PostgreSQLUsername};Password={plainTextPassword};Database={pgsqlDatabase};Include Error Detail={pgsqlErrorDetails}";

            string connectionString = Config.PostgreSQLConnection;
            _dbService = new PostgreSQLServices(connectionString);
            InitializeWebControl();
        }
        private async void InitializeWebControl()
        {
            //Topbar
            Panel panelMoveWindow = new()
            {
                Size = new Size(773, 36),
                Location = new Point(258, 0)
            };
            panelMoveWindow.Visible = false;
            this.Controls.Add(panelMoveWindow);

            panelMoveWindow.BringToFront();
            panelMoveWindow.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelMoveWindow.BackColor = ColorTranslator.FromHtml("#3F4144");
            panelMoveWindow.Show();
            panelMoveWindow.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };
            await webViewTitlebar.EnsureCoreWebView2Async(null);
            webViewTitlebar.CoreWebView2.WebMessageReceived += WebViewTitlebar_WebMessageReceived;
            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/MoreActionsTitleBar.html");
            webViewTitlebar.Source = new Uri(htmlFilePath);


            await webViewRequests.EnsureCoreWebView2Async(null);

            string closeHTMLPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/MoreActions.html");
            webViewRequests.Source = new Uri(closeHTMLPath);
        }
        private void WebViewTitlebar_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string message = e.WebMessageAsJson;

                if (message.Contains("btMinimizeClicked"))
                    this.WindowState = FormWindowState.Minimized;
                else if (message.Contains("btMaximizeClicked"))
                {
                    this.WindowState = this.WindowState == FormWindowState.Maximized
                        ? FormWindowState.Normal
                        : FormWindowState.Maximized;
                }
                else if (message.Contains("btCloseClicked"))
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing message: " + ex.Message);
            }
        }
        private async void webViewRequests_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            await webViewRequests.EnsureCoreWebView2Async(null);
            webViewRequests.CoreWebView2.AddHostObjectToScript("requestsApi", _dbService);

            await UpdateMoreActionsWindow();
        }

        private async Task UpdateMoreActionsWindow()
        {

            RequestAttributes controlNumAttributes = new();
            controlNumAttributes = _dbService.GetRequestAttributes(_requisitionNumber);
            Debug.WriteLine(_requisitionNumber);
            //request_type_rubberdie request_type_diecut    
            string jsSetRequisitionNumber = $"window.requisitionNumber = {System.Text.Json.JsonSerializer.Serialize(_requisitionNumber)};";
            await webViewRequests.CoreWebView2.ExecuteScriptAsync(jsSetRequisitionNumber);

            string originRequest;
            if (controlNumAttributes.RequestInternal) 
                originRequest = "Internal Request"; 
            else 
                originRequest = "External Request";

            await webViewRequests.CoreWebView2.ExecuteScriptAsync(
                   $"updateElementValue('headerText1', {System.Text.Json.JsonSerializer.Serialize(_requisitionNumber)});" +
                   $"toggleElement('request_type_rubberdie', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.Rubberdie)});" +
                   $"toggleElement('request_type_diecut', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.DiecutMould)});" +
                   $"updateOtherElementValue('due_date', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.DueDate)});" +
                   $"updateOtherElementValue('requested_date', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.RequestedDate)});" +
                   $"updateOtherElementValue('requestOrigin', {System.Text.Json.JsonSerializer.Serialize(originRequest)});" +
                   $"updateOtherElementValue('requisition_number', {System.Text.Json.JsonSerializer.Serialize(_requisitionNumber)});"
               );
            await webViewRequests.CoreWebView2.ExecuteScriptAsync("initializeDataTable();");
        }

        private async void webViewTitlebar_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //await UpdateMoreActionsWindow();
        }

        private void webViewRequests_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string action = e.WebMessageAsJson.Replace("\"", "");
            if (action == "updateSuccess")
            {
                MessageBox.Show("Request updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Close();
            }
        }
    }
}
