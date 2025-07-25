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
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BoxSmart_ERP.Services.PostgreSQLServices;

namespace BoxSmart_ERP
{
    public partial class EditRecentRequest : Form
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

        private PermissionService _permissionService;

        public EditRecentRequest(IConfiguration config, string requisitionNumber)
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
            _permissionService = new PermissionService(connectionString); // PostgreSQL connection string for permissions
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
            panelMoveWindow.BackColor = ColorTranslator.FromHtml("#1e1e37");
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
            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/EditRequestTitlebar.html");
            webViewTitlebar.Source = new Uri(htmlFilePath);


            await webViewRequests.EnsureCoreWebView2Async(null);

            string closeHTMLPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/EditRequest.html");
            webViewRequests.Source = new Uri(closeHTMLPath);
            webViewRequests.CoreWebView2.WebMessageReceived += RequestsMessageReceived;            
        }       

        public class RequestMessage
        {
            public string Command { get; set; }
            public int RequestId { get; set; }
            public string ItemDescription { get; set; }
        }


        private async void RequestsMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {

            var data = JsonSerializer.Deserialize<RequestMessage>(e.WebMessageAsJson);
            if (data != null)
            {
                Console.WriteLine($"Action: {data.Command}, ID: {data.RequestId}, Description: {data.ItemDescription}");
                if (data.Command == "delete-row")
                {
                    //Count rows for this requisition number
                    int rowCount = _dbService.GetRequestRowCountByRequisitionNumber(_requisitionNumber);
                    if (rowCount == 1)
                    {
                        MessageBox.Show("You cannot delete the last requested item for this requisition number.\nPlease add another item to enable delete" +
                            "\nIf you want to cancel this request, enable `Delete Last Item` checkbox in user preferences to effectively delete and cancel this request.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    bool hasPermission = await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "diecut_delete") || await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "rubberdie_delete");
                    if (!hasPermission)
                    {
                        MessageBox.Show("You do not have permission to delete this request item.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var result = MessageBox.Show($"Are you sure you want to delete the request item: {data.RequestId} : {data.ItemDescription}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            if (_dbService.DeleteToolingRequest(data.RequestId))
                            {
                                Debug.WriteLine($"Request {data.RequestId} deleted successfully.");
                                MessageBox.Show($"Request item: {data.RequestId} : {data.ItemDescription} deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                webViewRequests.CoreWebView2.Reload();
                                // Send a message to MainForm.cs to refh the requests list
                                MainForm mainForm = (MainForm)Application.OpenForms["MainForm"];
                                if (mainForm != null)
                                {
                                    mainForm.RefreshRequestsList();
                                }
                            }
                            else
                            {
                                Debug.WriteLine($"Failed to delete request {data.RequestId} : {data.ItemDescription}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } else if (data.Command == "submitrequest") {
                    // Handle the submit request logic here
                    try
                    {
                        //if (_dbService.SubmitToolingRequest(_requisitionNumber))
                        //{
                        //    MessageBox.Show($"Request {_requisitionNumber} updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    webViewRequests.CoreWebView2.Reload();
                        //    // Send a message to MainForm.cs to refresh the requests list
                        //    MainForm mainForm = (MainForm)Application.OpenForms["MainForm"];
                        //    if (mainForm != null)
                        //    {
                        //        mainForm.RefreshRequestsList();
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show($"Failed to submit request {_requisitionNumber}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error submitting request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }else if (data.Command == "additemok")
                {
                    //MessageBox.Show($"Additional Item Inserted command.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var result = MessageBox.Show($"Are you sure you want to add item: {data.ItemDescription}?", "Confirm Adding Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        //Send message back to webViewRequests to add the item
                        webViewRequests.CoreWebView2.PostWebMessageAsString("additemok");
                        OnMessageToWebView?.Invoke("additemok");
                        webViewRequests.CoreWebView2.Reload();
                    }
                }else if (data.Command == "additemsuccess") {                     
                    // Handle success message
                    MessageBox.Show($"Item:\"{data.ItemDescription}\" added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    webViewRequests.CoreWebView2.Reload();
                    // Send a message to MainForm.cs to refresh the requests list
                    MainForm mainForm = (MainForm)Application.OpenForms["MainForm"];
                    if (mainForm != null)
                    {
                        mainForm.RefreshRequestsList();
                    }
                } 
            }
        }
      
        public event Action<string> OnMessageToWebView;
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
            //Determine if the request is diecut then trigger to show the type of diecut rotary or flatbed 
            if (controlNumAttributes.DiecutMould)
            {
             
            }           

            await webViewRequests.CoreWebView2.ExecuteScriptAsync(
                   $"updateElementValue('headerText1', {System.Text.Json.JsonSerializer.Serialize(_requisitionNumber)});" +
                   $"toggleElement('request_type_rubberdie', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.Rubberdie)});" +
                   $"toggleElement('request_type_diecut', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.DiecutMould)});" +
                   $"updateInternalExternalVal('originExternal', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.RequestExternal)});" +
                   $"updateInternalExternalVal('originInternal', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.RequestInternal)});" +
                   $"updateOtherElementValue('due_date', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.DueDate)});" +
                   $"updateOtherElementValue('requested_date', {System.Text.Json.JsonSerializer.Serialize(controlNumAttributes.RequestedDate)});" +
                   $"updateOtherElementValue('requestOrigin', {System.Text.Json.JsonSerializer.Serialize(originRequest)});" +
                   $"updateOtherElementValue('requisition_number', {System.Text.Json.JsonSerializer.Serialize(_requisitionNumber)});" +
                   $"SetPriorityText('priority', 'Normal');"
               );            
            await webViewRequests.CoreWebView2.ExecuteScriptAsync("initializeDataTable();");
        }
    }
}
