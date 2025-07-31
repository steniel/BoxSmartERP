using BoxSmart_ERP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Core;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BoxSmart_ERP.EditDiecutItem;

namespace BoxSmart_ERP
{
    public partial class DisposeDiecut : Form
    {
        private readonly PostgreSQLServices _dbService;
        private readonly IConfiguration _config;

        private readonly string pgsqlHost;
        private readonly string pgsqlPort;
        private readonly string pgsqlDatabase;
        private readonly string pgsqlErrorDetails;

        private int _DiecutID;

        private static readonly int statusActive = 15;

        private readonly PermissionService _permissionService;

        public DisposeDiecut(IConfiguration config, int diecutId)
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
            await webView21.EnsureCoreWebView2Async(null);
            string closeHTMLPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Dispose.html");
            webView21.Source = new Uri(closeHTMLPath);
            webView21.CoreWebView2.WebMessageReceived += DisposeWinMessagReceived;
        }

        private async void DisposeWinMessagReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var message = JsonSerializer.Deserialize<Dictionary<string, string>>(e.WebMessageAsJson);
            if (message != null && message.TryGetValue("Command", out string command))
            {
                if (command == "close")
                {
                    this.Close();
                }else if (command == "dispose")
                {
                    if (MessageBox.Show("Are you sure you want to dispose of this diecut item?", "Confirm Dispose", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            int SessionUserID = AppSession.CurrentUserId;
                            // Call the DisposeDiecutItem method
                            await _dbService.DisposeDiecutItem(_DiecutID, SessionUserID);
                            MessageBox.Show("Diecut item disposed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //refresh MainForm webView2 control
                            MainForm mainForm = (MainForm)Application.OpenForms["MainForm"];
                            if (mainForm != null)
                            {
                                mainForm.RefreshWebViewApps();
                            }
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred while disposing the diecut item. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void webView21_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            string currentUsername = AppSession.CurrentUsername;
            string currentFullName = AppSession.CurrentFullName;
            int currentUserId = AppSession.CurrentUserId;
            string escapedUsername = currentUsername?.Replace("'", "\\'").Replace("\\", "\\\\") ?? "";
            string script = $"setLoggedUsername('{escapedUsername}');";
            await webView21.CoreWebView2.ExecuteScriptAsync(script);

            script = $"setLoggedUserId('{currentUserId}');";
            await webView21.CoreWebView2.ExecuteScriptAsync(script);

            script = $"setDiecutID('{_DiecutID}');";
            await webView21.CoreWebView2.ExecuteScriptAsync(script);

            DiecutAttributes DiecutItemProperties = new();
            DiecutItemProperties = GetRequestAttributes(_DiecutID);

            await webView21.CoreWebView2.ExecuteScriptAsync(
                   $"setTextValue('diecutNumber', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.DiecutNumber)});" +
                   $"setTextValue('diecutDescription', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.ItemDescription)});" +
                   $"setTextValue('requisitionNumber', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.RequisitionNumber)});" +
                   $"setTextValue('customerName', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.CustomerName)});" +
                   $"setTextValue('dateCreated', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.DateCreated)});" +
                   $"setTextValue('itemNotes', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.ItemNotes)});" +                                                      
                   $"setTextValue('currentMachine', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.Machine)});" +
                   $"setTextValue('printcardNumber', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.PrintcardNumber)});"
               );        
        }
    }
}

