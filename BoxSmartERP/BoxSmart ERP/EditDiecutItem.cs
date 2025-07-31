using BoxSmart_ERP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BoxSmart_ERP
{
    public partial class EditDiecutItem : Form
    {
        private readonly PostgreSQLServices _dbService;
        private readonly IConfiguration _config;

        private readonly string pgsqlHost;
        private readonly string pgsqlPort;
        private readonly string pgsqlDatabase;
        private readonly string pgsqlErrorDetails;

        private int _DiecutID;

        private static int statusActive = 15;
        private static string updateTypeHeader = "";

        private PermissionService _permissionService;
        public EditDiecutItem(IConfiguration config, int diecutId, string updateType = "")
        {
            InitializeComponent();
            _config = config;
            _DiecutID = diecutId;
            if (updateType != "")            
                updateTypeHeader = updateType;            
            else            
                updateTypeHeader = "";
            
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
           
            //await webViewTitlebar.EnsureCoreWebView2Async(null);
            //webViewTitlebar.CoreWebView2.WebMessageReceived += WebViewTitlebar_WebMessageReceived;
            //string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/EditRequestTitlebar.html");
            //webViewTitlebar.Source = new Uri(htmlFilePath);


            await webViewDiecuts.EnsureCoreWebView2Async(null);

            string closeHTMLPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/EditDiecut.html");
            webViewDiecuts.Source = new Uri(closeHTMLPath);
            webViewDiecuts.CoreWebView2.WebMessageReceived += RequestsMessageReceived;
        }
        public class DiecutUpdateMsg
        {
            public int DiecutNumber { get; set; }
            public int MachineID { get; set; }
            public string Command { get; set; }
        }

        private async void RequestsMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var data = JsonSerializer.Deserialize<DiecutUpdateMsg>(e.WebMessageAsJson);
            if (data != null)
            {
                if (data.Command == "close")
                {
                    this.Close();
                }
                if (data.DiecutNumber > 0)
                {
                    if (data.Command == "update")
                    {
                        if (UpdateDiecutItem(_DiecutID, data.DiecutNumber, data.MachineID))
                        {
                            string MachineName = "";
                            if (data.MachineID > 0)
                            {
                                using (var conn = new NpgsqlConnection(Config.PostgreSQLConnection))
                                {
                                    conn.Open();
                                    string sql = "SELECT trim(code) || ' - ' || trim(machine_name) as machine_name FROM flexo_machines WHERE id = @machineId;";
                                    using (var cmd = new NpgsqlCommand(sql, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@machineId", data.MachineID);
                                        MachineName = (string)cmd.ExecuteScalar() ?? "Unknown Machine";
                                    }
                                }
                            }
                            string statusMessage = "";
                            if (data.MachineID > 0)                                
                                statusMessage = $"This diecut was updated with diecut # {data.DiecutNumber} and was assigned to machine: {MachineName}.";
                            else
                                statusMessage = $"This diecut was updated with diecut # {data.DiecutNumber}.";

                            var response = new
                            {
                                status = "success",
                                message = statusMessage
                            };
                            string jsonResponse = JsonSerializer.Serialize(response);
                            webViewDiecuts.CoreWebView2.PostWebMessageAsJson(jsonResponse);
                            MainForm mainForm = (MainForm)Application.OpenForms["MainForm"];
                            if (mainForm != null)
                            {
                                mainForm.RefreshWebViewApps();
                            }
                        }else
                        {
                            var errorResponse = new
                            {
                                status = "error",
                                message = "Failed to update diecut item."
                            };
                            string jsonResponse = JsonSerializer.Serialize(errorResponse);
                            webViewDiecuts.CoreWebView2.PostWebMessageAsJson(jsonResponse);
                        }
                    }                    
                    else
                    {
                        var errorResponse = new
                        {
                            status = "error",
                            message = $"Invalid command received: {data.Command}"
                        };
                        string jsonResponse = JsonSerializer.Serialize(errorResponse);
                        webViewDiecuts.CoreWebView2.PostWebMessageAsJson(jsonResponse);
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
            LoadMachinesToWebView();
        }

        private async Task UpdateMoreActionsWindow()
        {

            DiecutAttributes DiecutItemProperties = new();
            DiecutItemProperties = GetRequestAttributes(_DiecutID);

            await webViewDiecuts.CoreWebView2.ExecuteScriptAsync(
                   $"setTextValue('diecutNumber', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.DiecutNumber)});" +
                   $"setTextValue('diecutDescription', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.ItemDescription)});" +
                   $"setTextValue('requisitionNumber', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.RequisitionNumber)});" +
                   $"setTextValue('customerName', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.CustomerName)});" +
                   $"setTextValue('dateCreated', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.DateCreated)});" +
                   $"setTextValue('itemNotes', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.ItemNotes)});" +
                   $"setTextValue('itemPriority', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.Priority)});" +
                   $"setTextValue('assignedMachine', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.Machine)});" +
                   $"setTextValue('assignedTo', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.AssignedTo)});" +
                   $"setTextValue('currentMachine', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.Machine)});"
               );
            if (updateTypeHeader == "assign_machine")
            {
                await webViewDiecuts.ExecuteScriptAsync(
                     "document.querySelector('#headerText1 h2').innerHTML = '<i class=\"fa fa-edit\" id=\"titleHeader\"></i>Assign Diecut To Converting Machine';" +
                     "document.querySelector('#assign_machine').checked = true;" +
                     "document.getElementById('machineNameWrapper').removeAttribute('hidden');" +
                     "document.getElementById('assign_machine').disabled = true;" +
                     "document.getElementById('diecutNumber').readOnly = true;" +
                     $"setTextValue('currentMachine', {System.Text.Json.JsonSerializer.Serialize(DiecutItemProperties.Machine)});"
                );                
            }
        }

        public class DiecutAttributes
        {
            public int DiecutId { get; set; }
            public string CustomerName { get; set; }    
            public string ItemDescription { get; set; }
            public string DiecutNumber { get; set; }
            public string ItemNotes { get; set; }
            public string RequisitionNumber { get; set; }
            public string Priority { get; set; }
            public string Machine { get; set; }
            public string AssignedTo { get; set; }
            public string PrintcardNumber { get; set; }
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
                        SELECT item_description,diecut_number,notes,requisition_number,date_created,
                        customer,machine,priority,assigned_to,printcardno
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
                                attributes.CustomerName = reader.IsDBNull(reader.GetOrdinal("customer")) ? null : reader.GetString(reader.GetOrdinal("customer"));
                                attributes.ItemDescription = reader.IsDBNull(reader.GetOrdinal("item_description")) ? null : reader.GetString(reader.GetOrdinal("item_description"));                                
                                attributes.RequisitionNumber = reader.IsDBNull(reader.GetOrdinal("requisition_number")) ? null : reader.GetString(reader.GetOrdinal("requisition_number"));
                                attributes.ItemNotes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes"));
                                attributes.Priority = reader.IsDBNull(reader.GetOrdinal("priority")) ? null : reader.GetString(reader.GetOrdinal("priority"));
                                attributes.Machine = reader.IsDBNull(reader.GetOrdinal("machine")) ? null : reader.GetString(reader.GetOrdinal("machine"));
                                attributes.AssignedTo = reader.IsDBNull(reader.GetOrdinal("assigned_to")) ? null : reader.GetString(reader.GetOrdinal("assigned_to"));
                                attributes.PrintcardNumber = reader.IsDBNull(reader.GetOrdinal("printcardno")) ? null : reader.GetString(reader.GetOrdinal("printcardno"));
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

        public static bool UpdateDiecutItem(int DiecutID, int DiecutNumber, int MachineID)
        {
            try
            {
                using (var conn = new NpgsqlConnection(Config.PostgreSQLConnection))
                {
                    conn.Open();
                    if (MachineID > 0)
                    {
                        // Update the machine ID if provided
                        string sql = @"
                            UPDATE diecut_tools 
                            SET diecut_number = @diecut_number, machine_id = @machineId, updated_by = @updatedBy, status_id = @statusActive
                            WHERE id = @DiecutID;";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@diecut_number", DiecutNumber);
                            cmd.Parameters.AddWithValue("@machineId", MachineID);
                            cmd.Parameters.AddWithValue("@updatedBy", AppSession.CurrentUserId);
                            cmd.Parameters.AddWithValue("@statusActive", statusActive);
                            cmd.Parameters.AddWithValue("@DiecutID", DiecutID);
                            cmd.ExecuteNonQuery();
                        }
                    }else
                    {
                        string sql = @"
                        UPDATE diecut_tools 
                        SET diecut_number = @diecut_number, updated_by = @updatedBy, status_id = @statusActive
                        WHERE id = @DiecutID;";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@diecut_number", DiecutNumber);
                            cmd.Parameters.AddWithValue("@updatedBy", AppSession.CurrentUserId);
                            cmd.Parameters.AddWithValue("@statusActive", statusActive);
                            cmd.Parameters.AddWithValue("@DiecutID", DiecutID);
                            cmd.ExecuteNonQuery();
                        }
                    }                       
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating maintenance status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async void LoadMachinesToWebView()
        {
            var machines = new List<Dictionary<string, object>>();
            
            string query = "SELECT id, TRIM(code) || ' - ' || TRIM(machine_name) as machine_name FROM flexo_machines WHERE is_active=true AND printer=true;";

            using (var conn = new NpgsqlConnection(Config.PostgreSQLConnection))
            {
                await conn.OpenAsync();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        machines.Add(new Dictionary<string, object>
                {
                    { "id", reader["id"] },
                    { "name", reader["machine_name"] }
                });
                    }
                }
            }

            // Send to JavaScript as JSON
            var json = JsonSerializer.Serialize(machines);
            await webViewDiecuts.CoreWebView2.ExecuteScriptAsync($"populateMachineDropdown({json})");
        }       
    }
}
