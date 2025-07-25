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
    public partial class NewRequest : Form
    {
        // P/Invoke declarations for moving the window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private PostgreSQLServices _dbService;
        private readonly InfoTech _machineService;
        private readonly IConfiguration _config;

        private readonly string pgsqlHost;
        private readonly string pgsqlPort;
        private readonly string pgsqlDatabase;
        private readonly string pgsqlErrorDetails;
        private static string connectionString;


        public NewRequest(IConfiguration config)
        {
            InitializeComponent();
            _config = config;

            //PostgresConn
            pgsqlHost = _config["ConnectionStrings:PostgreSQLHost"];
            pgsqlPort = _config["ConnectionStrings:PostgreSQLPort"];
            pgsqlDatabase = _config["ConnectionStrings:PostgreSQLDatabase"];
            pgsqlErrorDetails = _config["ConnectionStrings:ErrorDetails"];
        }
        protected override async void OnLoad(EventArgs e)
        {
            //base.OnLoad(e);
            //try
            //{
            //    IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(Config.PostgreSQLPassword);
            //    string plainTextPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);

            //    Config.PostgreSQLConnection = $"Host={pgsqlHost};Port={pgsqlPort};Username={Config.PostgreSQLUsername};Password={plainTextPassword};Database={pgsqlDatabase};Include Error Detail={pgsqlErrorDetails}";

            //    string connectionString = Config.PostgreSQLConnection;
            //    _dbService = new PostgreSQLServices(connectionString);
                
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("DB init failed: " + ex.Message);
            //    // Optionally return or disable features
            //}
           InitializeWebControl();
        }

        private async void InitializeWebControl()
        {
            try
            {
                if (webViewRequests == null || webViewRequests.IsDisposed)
                {
                    MessageBox.Show("webViewRequests is not available!");
                    return;
                }

                var userDataFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "BoxSmartERPWebView2"
                );
                var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
                await webViewRequests.EnsureCoreWebView2Async(env);

                string closeHTMLPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/NewRequest.html");
                webViewRequests.Source = new Uri(closeHTMLPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error initializing WebView2: {ex.Message}\n\n" +
                    $"StackTrace: {ex.StackTrace}\n\n" +
                    $"InnerException: {ex.InnerException?.Message}",
                    "Initialization Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                //Clipboard.SetText($"Error initializing WebView2: {ex.Message}\n\nStackTrace: {ex.StackTrace}\n\nInnerException: {ex.InnerException?.Message}");
            }
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
            string currentUsername = AppSession.CurrentUsername;
            string currentFullName = AppSession.CurrentFullName;
            int currentUserId = AppSession.CurrentUserId;
            string escapedUsername = currentUsername?.Replace("'", "\\'").Replace("\\", "\\\\") ?? "";
            string script = $"setLoggedUsername('{escapedUsername}');";
            await webViewRequests.CoreWebView2.ExecuteScriptAsync(script);

            script = $"setLoggedUserId('{currentUserId}');";
            await webViewRequests.CoreWebView2.ExecuteScriptAsync(script);

            await webViewRequests.EnsureCoreWebView2Async(null); // Ensure initialized if not already
            webViewRequests.CoreWebView2.AddHostObjectToScript("requestsApi", _dbService);
            TSDMetrics dashboardMetrics = new(); // Initialize DTO 

            try
            {
                // Call the updated GetPrintcardData method
                dashboardMetrics = _dbService.GetPrintcardSummaryMetrics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching requests data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // dashboardMetrics will retain default (0,0) in case of error, ensuring JavaScript still gets numbers
            }
            string nextControlNumber = "";
            if (!dashboardMetrics.NextControlNumber.Contains(' '))
            {
                int currentYear = DateTime.Now.Year;
                int controlNumberLength = dashboardMetrics.NextControlNumber.Length;
                if (controlNumberLength < 3)
                {
                    string nextControlNumberSuffix = $"{dashboardMetrics.NextControlNumber.PadLeft(3, '0')}";
                    nextControlNumber = $"S - {currentYear} / {nextControlNumberSuffix}";
                }
                else
                {
                    nextControlNumber = $"S - {currentYear} / {dashboardMetrics.NextControlNumber}";
                }
            }
            else
            {
                nextControlNumber = dashboardMetrics.NextControlNumber; // Use the original value if it contains a space                   
            }

            // Update the requisition_number input value using the JS function
            await webViewRequests.CoreWebView2.ExecuteScriptAsync($"document.getElementById('requisition_number').value = '{nextControlNumber}';");
            // Optionally, also call updateControlNumber if you want to update innerHTML (not typical for input)
            // await webViewRequests.CoreWebView2.ExecuteScriptAsync($"updateControlNumber('requisition_number', '{nextControlNumber}');");

        }

        private void NewRequest_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
