using BoxSmart_ERP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Core;
using Npgsql;
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
    public partial class EditDiecutItem : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private readonly PostgreSQLServices _dbService;
        private readonly IConfiguration _config;

        private readonly string pgsqlHost;
        private readonly string pgsqlPort;
        private readonly string pgsqlDatabase;
        private readonly string pgsqlErrorDetails;

        private int _DiecutID;

        private PermissionService _permissionService;
        public EditDiecutItem(IConfiguration config, int diecutId)
        {
            InitializeComponent();
            _config = config;
            _DiecutID = diecutId;
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


            await webViewDiecuts.EnsureCoreWebView2Async(null);

            string closeHTMLPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/EditDiecut.html");
            webViewDiecuts.Source = new Uri(closeHTMLPath);
            webViewDiecuts.CoreWebView2.WebMessageReceived += RequestsMessageReceived;
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

                    bool hasPermission = await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "diecut_delete") || await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "rubberdie_delete");
                    if (!hasPermission)
                    {
                        MessageBox.Show("You do not have permission to delete this requested item.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                webViewDiecuts.CoreWebView2.Reload();
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
                }
                else if (data.Command == "submitrequest")
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error submitting request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (data.Command == "additemok")
                {
                    //MessageBox.Show($"Additional Item Inserted command.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var result = MessageBox.Show($"Are you sure you want to add item: {data.ItemDescription}?", "Confirm Adding Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        //Send message back to webViewRequests to add the item
                        webViewDiecuts.CoreWebView2.PostWebMessageAsString("additemok");
                        OnMessageToWebView?.Invoke("additemok");
                        webViewDiecuts.CoreWebView2.Reload();
                    }
                }
                else if (data.Command == "additemsuccess")
                {
                    // Handle success message
                    MessageBox.Show($"Item:\"{data.ItemDescription}\" added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    webViewDiecuts.CoreWebView2.Reload();
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

        private async void webViewDiecuts_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            string currentUsername = AppSession.CurrentUsername;
            string currentFullName = AppSession.CurrentFullName;
            int currentUserId = AppSession.CurrentUserId;
            string escapedUsername = currentUsername?.Replace("'", "\\'").Replace("\\", "\\\\") ?? "";
            string script = $"setLoggedUsername('{escapedUsername}');";
            await webViewDiecuts.CoreWebView2.ExecuteScriptAsync(script);

            script = $"setLoggedUserId('{currentUserId}');";
            await webViewDiecuts.CoreWebView2.ExecuteScriptAsync(script);

            await webViewDiecuts.EnsureCoreWebView2Async(null);
            webViewDiecuts.CoreWebView2.AddHostObjectToScript("requestsApi", _dbService);
            await UpdateMoreActionsWindow();
        }

        private async Task UpdateMoreActionsWindow()
        {

            DiecutAttributes DiecutItemProperties = new();
            DiecutItemProperties = GetRequestAttributes(_DiecutID);

            await webViewDiecuts.CoreWebView2.ExecuteScriptAsync(
                   $"setTextValue('diecutNumber', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.DiecutNumber)});" +
                   $"setTextValue('diecutDescription', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.ItemDescription)});"

               );
            //await webViewDiecuts.CoreWebView2.ExecuteScriptAsync("initializeDataTable();");
        }

        public class DiecutAttributes
        {
            public int DiecutId { get; set; }
            public string ItemDescription { get; set; }
            public string DiecutNumber { get; set; }
            public string ItemNotes { get; set; }
            public string RequisitionNumber { get; set; }
            public string DateCreated { get; set; }
        }

        public static DiecutAttributes GetRequestAttributes(int DiecutID)
        {
            var attributes = new DiecutAttributes();
            try
            {
                using (var conn = new NpgsqlConnection(Config.PostgreSQLConnection))
                {
                    conn.Open();
                    string sql = @"
                        SELECT item_description,diecut_number,notes,requisition_number,date_created 
                        FROM diecutview 
                        WHERE diecutid=@diecutId;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@diecutId", DiecutID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                attributes.DiecutNumber = reader.IsDBNull(reader.GetOrdinal("diecut_number")) ? null : reader.GetString(reader.GetOrdinal("diecut_number"));
                                attributes.ItemDescription = reader.IsDBNull(reader.GetOrdinal("item_description")) ? null : reader.GetString(reader.GetOrdinal("item_description"));                                
                                attributes.ItemNotes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"));
                                attributes.RequisitionNumber = reader.IsDBNull(reader.GetOrdinal("requisition_number")) ? null : reader.GetString(reader.GetOrdinal("requisition_number"));
                                attributes.DateCreated = reader.IsDBNull(reader.GetOrdinal("date_created")) ? null : reader.GetDateTime(reader.GetOrdinal("date_created")).ToString("MM-dd-yyyy"); // Format as needed                                  
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching maintenance attributes: {ex.Message}");
            }
            return attributes; 
        }
    }
}
