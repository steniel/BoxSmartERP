using Antlr4.Runtime.Misc;
using BoxSmart_ERP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using PowerArgs;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.Json;
using System.Web;
using System.Windows.Forms;
using static BoxSmart_ERP.Services.PostgreSQLServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BoxSmart_ERP
{
    public partial class MainForm : Form
    {
        // P/Invoke declarations for moving the window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private PostgreSQLServices _dbService;
        private InfoTech _machineService;
        private readonly IConfiguration _config;

        // NEW: Flag to indicate if dashboard data needs to be loaded after navigation
        private bool _loadDashboardDataAfterNavigation = false;
        //Background background = new Background(); // Initialize the background form
        private readonly string pgsqlHost;
        private readonly string pgsqlPort;
        private readonly string pgsqlDatabase;
        private readonly string pgsqlErrorDetails;
        private static string connectionString;
        private CancelConfirmationDialog confirmationDialog;
        private PermissionService _permissionService;
        public MainForm(IConfiguration config)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true); // For smoother resizing

            _config = config;
            string sqlServer = _config["ConnectionStrings:SQLServer"];
            string sqlDbName = _config["ConnectionStrings:SQLDbName"];
            Config.SQLStringConnection = $"Data Source={sqlServer},1433;" +
                                         $"Initial Catalog={sqlDbName};" +
                                         "TrustServerCertificate=True;" +
                                         "Integrated Security=True;";

            pgsqlHost = _config["ConnectionStrings:PostgreSQLHost"];
            pgsqlPort = _config["ConnectionStrings:PostgreSQLPort"];
            pgsqlDatabase = _config["ConnectionStrings:PostgreSQLDatabase"];
            pgsqlErrorDetails = _config["ConnectionStrings:ErrorDetails"];      

            splitContainer1.BackColor = ColorTranslator.FromHtml("#282A2D");
            this.Text = "BoxSmart ERP"; // Set the title of the main form
            //background.Show(); // Show the background form           

        }

        public class RequestMessage
        {
            public string Command { get; set; } = "";           // default to empty string
            public string ControlSequence { get; set; } = "";   // default to empty string
            public string PdfURL { get; set; } = "";
        }

        private async void RequestsMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {            
            var data = JsonSerializer.Deserialize<RequestMessage>(e.WebMessageAsJson);
            if (data != null)
            {
                //string commandReceived = data.ContainsKey("Command") ? data["Command"] : "";
                //string controlSequence = data.ContainsKey("ControlSequence") ? data["ControlSequence"] : "";
                string commandReceived = data?.Command ?? "";
                string controlSequence = data?.ControlSequence ?? "";
                string _PdfUrl = data?.PdfURL ?? "";
                Console.WriteLine($"Action: {commandReceived}, ID: {controlSequence}");
                if (commandReceived == "EditRequest")
                {
                    //Check if user has permission to edit
                    bool hasPermission = await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "diecut_create") || await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "diecut_delete") || await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "diecut_update") || await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "rubberdie_create") || await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "rubberdie_delete") || await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "rubberdie_update");
                    if (!hasPermission)
                    {
                        MessageBox.Show("You do not have permission to edit requests.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (IsRequisitionPending(controlSequence))
                    {
                        _loadDashboardDataAfterNavigation = false;
                        EditRecentRequest editRequest = new(_config, controlSequence);
                        editRequest.Show(); // Show the EditRecentRequest form
                    }
                    else
                    {
                        MessageBox.Show("You can only edit requests with pending status.", "Pending Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (commandReceived == "UpdateRequest")
                {
                    bool hasPermission = await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "assign_specimen");
                    if (!hasPermission)
                    {
                        MessageBox.Show("Only TSD officers can assign job requests.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (IsRequisitionPending(controlSequence))
                    {
                        _loadDashboardDataAfterNavigation = false;
                        MoreActions moreActions = new(_config, controlSequence);
                        moreActions.Show(); // Show the MoreActions form 
                    }
                    else
                    {
                        MessageBox.Show("You can only update requests with pending status.", "Update Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }                    
                }
                else if (commandReceived == "CancelRequest")
                {
                    bool hasPermission = await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "cancel_request");
                    if (!hasPermission)
                    {
                        MessageBox.Show("Only Sales personnel can cancel requested items.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (IsRequisitionPending(controlSequence))
                    {
                        DialogResult result = MessageBox.Show($"Are you sure you want to cancel all the items on this requisition #: {controlSequence}?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            _loadDashboardDataAfterNavigation = false;

                            using ConfirmationDialogForm dialogForm = new();
                            dialogForm.Question = $"This action will permanently cancel the items in requisition # {controlSequence}. Type 'Yes cancel this request' to confirm.";

                            if (dialogForm.ShowDialog(this) == DialogResult.Yes)
                            {
                                this.Refresh(); // Refresh the form to ensure UI updates
                                if (_dbService.CancelRequest(controlSequence))
                                {
                                    MessageBox.Show($"Requisition #: {controlSequence} was canceled successfully.", "Canceled Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    webViewApps.CoreWebView2.Reload(); // Reload the page to reflect changes                                    
                                }                                
                            }
                        }
                    } else {
                        MessageBox.Show("You can only delete requests with pending status.", "Delete Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (commandReceived == "view")
                {
                    // Check permission
                    bool canView = await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "view_reports");
                    if (!canView)
                    {
                        //await webViewApps.CoreWebView2.ExecuteScriptAsync("alert('You lack permission to view reports.');");
                        await webViewApps.CoreWebView2.ExecuteScriptAsync("toastr.error(\"Permission required to view reports!\", null, { positionClass: \"toast-top-full-width\" });");
                        return;
                    }
                    else
                    {                                    
                        ViewReport viewReportForm = new ViewReport(_PdfUrl);
                        viewReportForm.Show();
                    }                        
                }
            }
        }
    
        private async void InitializeAsync()
        {
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(Config.PostgreSQLPassword);
            string plainTextPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            Config.PostgreSQLConnection = $"Host={pgsqlHost};Port={pgsqlPort};Username={Config.PostgreSQLUsername};Password={plainTextPassword};Database={pgsqlDatabase};Include Error Detail={pgsqlErrorDetails}";
            connectionString = Config.PostgreSQLConnection;
            _dbService = new PostgreSQLServices(connectionString);
            _machineService = new InfoTech(Config.SQLStringConnection); //Microsoft SQL Server

            _permissionService = new PermissionService(connectionString); // PostgreSQL connection string for permissions

            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/titlebar.html");
            webView21.Source = new Uri(htmlFilePath);

            await webViewSideBar.EnsureCoreWebView2Async(null);
            webViewSideBar.CoreWebView2.WebMessageReceived += SideBar_WebMessageReceived;
            string sideBarFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Sidebar.html");
            webViewSideBar.Source = new Uri(sideBarFilePath);           

            await webViewApps.EnsureCoreWebView2Async(null);
   
            webViewApps.CoreWebView2.NavigationCompleted += WebViewApps_NavigationCompleted;
            string mainDashboard = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Dashboard.html");

            _loadDashboardDataAfterNavigation = true;

            webViewApps.Source = new Uri(mainDashboard); 
            this.Visible = true;

            await UpdateActivityData();
            SetupTimer();
            webViewApps.CoreWebView2.WebMessageReceived += RequestsMessageReceived;
        }

        private async void WebViewApps_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // Check if navigation was successful and if we need to load dashboard data
            if (e.IsSuccess && _loadDashboardDataAfterNavigation)
            {
                // Reset the flag immediately to prevent multiple calls
                _loadDashboardDataAfterNavigation = false;

                // Check if the current source is indeed the Dashboard.html
                // This is a safety check, important if other pages could trigger this.
                string currentHtml = webViewApps.Source.LocalPath;
                string dashboardPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Dashboard.html");

                if (currentHtml.EndsWith(Path.GetFileName(dashboardPath), StringComparison.OrdinalIgnoreCase))
                {

                    await webViewApps.EnsureCoreWebView2Async(null);
                    webViewApps.CoreWebView2.AddHostObjectToScript("dashboardApi", _dbService);

                }
                else if (currentHtml.EndsWith("Printcard.html", StringComparison.OrdinalIgnoreCase))
                {
                    await SetupPrintcardHostObject();
                }
                else if (currentHtml.EndsWith("Diecut.html", StringComparison.OrdinalIgnoreCase))
                {
                    await webViewApps.EnsureCoreWebView2Async(null); 
                    webViewApps.CoreWebView2.AddHostObjectToScript("printcardApi", _dbService);
                }
                else if (currentHtml.EndsWith("Rubberdie.html", StringComparison.OrdinalIgnoreCase))
                {
                    await webViewApps.EnsureCoreWebView2Async(null); 
                    webViewApps.CoreWebView2.AddHostObjectToScript("printcardApi", _dbService);
                }
                else if (currentHtml.EndsWith("Requests.html", StringComparison.OrdinalIgnoreCase))
                {
                    await webViewApps.EnsureCoreWebView2Async(null); 
                    webViewApps.CoreWebView2.AddHostObjectToScript("requestsApi", _dbService);
                }
                else if (currentHtml.EndsWith("NewRequest.html", StringComparison.OrdinalIgnoreCase))
                {
                    await webViewApps.EnsureCoreWebView2Async(null); 
                    webViewApps.CoreWebView2.AddHostObjectToScript("requestsApi", _dbService);
                }
                else if (currentHtml.EndsWith("Maintenance.html", StringComparison.OrdinalIgnoreCase))
                {
                    await webViewApps.EnsureCoreWebView2Async(null); 
                    webViewApps.CoreWebView2.AddHostObjectToScript("printcardApi", _dbService);
                }
                else if (currentHtml.EndsWith("MachineSchedule.html", StringComparison.OrdinalIgnoreCase))
                {
                    await SetupMachineHostObject(); 
                }
            }
        }
      
        private async void SideBar_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string action = e.WebMessageAsJson.Replace("\"", "");
            string contentHtmlPath = "";

            switch (action)
            {
                case "dashboard":
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Dashboard.html");
                    _loadDashboardDataAfterNavigation = true; // Set flag to load data after dashboard loads
                    break;
                case "printcard":
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Printcard.html");
                    _loadDashboardDataAfterNavigation = true; // No dashboard data needed for printcard
                    break;
                case "rubberdie":
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Rubberdie.html");
                    _loadDashboardDataAfterNavigation = true;
                    break;
                case "diecut":
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Diecut.html");
                    _loadDashboardDataAfterNavigation = true;
                    break;
                case "mac_schedule":
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/MachineSchedule.html");
                    _loadDashboardDataAfterNavigation = true;
                    break;
                case "maintenance":
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Maintenance.html");
                    _loadDashboardDataAfterNavigation = true;
                    break;
                case "requests":
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Requests.html");
                    _loadDashboardDataAfterNavigation = true;
                    break;               
                case "new_request":
                    _loadDashboardDataAfterNavigation = true;
                    bool hasPermission = await CheckPermissionNewRequestAsync();
                    if (!hasPermission)
                    {
                        //contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/PermissionDenied.html");
                        //MessageBox.Show("You do not have permission to create new requests.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);                        
                        await webViewApps.CoreWebView2.ExecuteScriptAsync("toastr.error(\"Permission required to create requests!\", null, { positionClass: \"toast-top-full-width\" });");
                        return;
                    }
                    else
                    {
                        contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/NewRequest.html");
                        _loadDashboardDataAfterNavigation = true;
                    }                    
                    break;
                case "logout":
                    if (MessageBox.Show("Are you sure you want to log out? ", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return; // User chose not to log out
                    }
                    AppSession.EndSession();
                    Application.Exit();
                    break;
                default:
                    contentHtmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.html");
                    _loadDashboardDataAfterNavigation = false;
                    break;
            }

            if (File.Exists(contentHtmlPath))
            {
                webViewApps.Source = new Uri(contentHtmlPath);
            }
            else
            {
                webViewApps.CoreWebView2.NavigateToString("<html><body><h1>Content Not Found</h1><p>The requested page for '" + action + "' could not be loaded.</p></body></html>");
            }
        }

        private async Task<bool> CheckPermissionNewRequestAsync() {
            return await _permissionService.HasPermissionAsync(AppSession.CurrentUserId, "create_request");               
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
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

        private async System.Threading.Tasks.Task SetupPrintcardHostObject()
        {            
            await webViewApps.EnsureCoreWebView2Async(null); // Ensure initialized if not already            
            webViewApps.CoreWebView2.AddHostObjectToScript("printcardApi", _dbService);            
            // You might want to call JavaScript functions to load data here if Printcard.html has its own data needs.
        }

        private async System.Threading.Tasks.Task SetupMachineHostObject()
        {
            await webViewApps.EnsureCoreWebView2Async(null); // Ensure initialized if not already
            webViewApps.CoreWebView2.AddHostObjectToScript("machineApi", _machineService);
            
            // You might want to call JavaScript functions to load data here if MachineSchedule.html has its own data needs.
        }

        private async void LoadRubberdieUsageAlert()
        {
            var pg = new PostgreSQLServices(connectionString);
            string jsonData = pg.GetRubberdieUsageLimitItems();
            // Count the number of items
            using var doc = JsonDocument.Parse(jsonData);
            int count = doc.RootElement.GetArrayLength();
            // Escape the JSON for injection into JS
            string escapedJson = jsonData.Replace("\\", "\\\\").Replace("\"", "\\\"");
            string jsCode = $"updateRubberdieUsageLimit('rubberdiesUsageLimitReached', {count}, JSON.parse(\"{escapedJson}\"));";
            //MessageBox.Show(jsCode);
            await webViewApps.CoreWebView2.ExecuteScriptAsync(jsCode);
        }
        private async void LoadDiecutUsageAlert()
        {
            var pg = new PostgreSQLServices(connectionString);
            string jsonData = pg.GetDiecutUsageLimitItems();            
            using var doc = JsonDocument.Parse(jsonData);
            int count = doc.RootElement.GetArrayLength();            
            string escapedJson = jsonData.Replace("\\", "\\\\").Replace("\"", "\\\"");
            string jsCode = $"updateDiecutUsageLimit('diecutsUsageLimitReached', {count}, JSON.parse(\"{escapedJson}\"));";            
            await webViewApps.CoreWebView2.ExecuteScriptAsync(jsCode);
        }

        private async System.Threading.Tasks.Task SendPrintcardsMetricDataToWebView()
        {
            // Ensure CoreWebView2 is ready
            if (webViewApps.CoreWebView2 == null) return;

            TSDMetrics dashboardMetrics = new(); // Initialize DTO             
            try
            {
                await webViewApps.EnsureCoreWebView2Async(null);
                webViewApps.CoreWebView2.AddHostObjectToScript("dashboardApi", _dbService);

                int year = DateTime.Now.Year; // Get the current year
                var chartData = await FetchInventoryData(year);

                string jsonData = JsonConvert.SerializeObject(chartData, Formatting.Indented);
                System.Diagnostics.Debug.WriteLine($"Data sent to WebView2: {jsonData}");

                string escapedJson = HttpUtility.JavaScriptStringEncode(jsonData);

                string script = $"try {{ updateInventoryChart(JSON.parse(\"{escapedJson}\"), {year}); }} catch (e) {{ logDebug('Script error: ' + e.message); }}";
                System.Diagnostics.Debug.WriteLine($"Executing script: {script}");

                //await Task.Delay(1000);

                await webViewApps.ExecuteScriptAsync(script);

                // Call the updated GetPrintcardData method 
                dashboardMetrics = _dbService.GetPrintcardSummaryMetrics();
                    await webViewApps.CoreWebView2.ExecuteScriptAsync(
                   $"updateMetricValue('totalPrintcardsValue', {dashboardMetrics.TotalPrintcards});" +
                   $"updateMetricBadgeThisWeek('totalPrintcardsBadgeWeek', {dashboardMetrics.AddedThisWeek});" +
                   $"updateMetricBadgeThisMonth('totalPrintcardsBadgeMonth', {dashboardMetrics.AddedThisMonth});" +
                   $"updateMetricValue('totalRequests', {dashboardMetrics.TotalRequests});" +
                   $"updateMetricPendingRequests('totalPendingRequests', {dashboardMetrics.PendingRequests});" +
                   $"updateHighPriorityRequests('totalHighPriorityRequests', {dashboardMetrics.HighPriorityRequests});" +
                   $"updateInDevelopmentRequests('totalInDevelopmentRequests', {dashboardMetrics.InDevelopmentRequests});" +
                   $"updateCompletedRequests('totalInCompletedRequests', {dashboardMetrics.CompletedRequests});" +
                   $"updateCompletedThisWeek('thisWeekCompleted', {dashboardMetrics.ThisWeekCompletedRequests});" +
                   $"updateMetricValue('totalDiecutMould', {dashboardMetrics.TotalActiveDiecuts});" +
                   $"updateMetricValue('totalRubberdies', {dashboardMetrics.TotalActiveRubberdie});" +
                   $"updateDiecutInMaintenance('totalDiecutInMaintenance', {dashboardMetrics.TotalDiecutInMaintenance});" +
                   $"tryLoadRecentRequests();"
                );
                await LoadDiecutPieAsync(); //piechart for Diecut

                await LoadRubberdieAsync(); //piechart for Rubberdie

                await LoadPrintcardAsync(); //piechart for Printcard

                if (dashboardMetrics.DiecutUsageLimit > 0)
                {
                    LoadDiecutUsageAlert();
                }
                else
                {
                    // If no diecut usage limit, ensure the alert is cleared
                    await webViewApps.CoreWebView2.ExecuteScriptAsync("updateDiecutUsageLimit('diecutsUsageLimitReached', 0, []);");
                }
                if (dashboardMetrics.RubberdieUsageLimit > 0)
                {
                    LoadRubberdieUsageAlert();
                }
                else
                {
                    // If no rubberdie usage limit, ensure the alert is cleared
                    await webViewApps.CoreWebView2.ExecuteScriptAsync("updateRubberdieUsageLimit('rubberdiesUsageLimitReached', 0, []);");
                }

                    int currentYear = DateTime.Now.Year;

                List<MonthlyPrintcardCounts> monthlyPrintcardData = await _dbService.GetMonthlyPrintcardCounts(currentYear);
                var dashboardResponse = new DashboardDataResponse
                {
                    Action = "populateDashboard", // This action tells JS how to handle the data                
                    MonthlyPrintcardData = monthlyPrintcardData
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching Printcard data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                   
        }
        
        private async void webViewApps_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string currentHtml = webViewApps.Source.LocalPath;
            string dashboardPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Dashboard.html");
            string newRequestPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/NewRequest.html");
            string MaintenanceHTML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Maintenance.html");            
            try
            {             
                
                string action = e.WebMessageAsJson.Replace("\"", "");
                //Fetch inventory data if year changed
                if (action.Contains("fetchYearData"))
                {
                    string message = e.WebMessageAsJson;
                    var json = JsonConvert.DeserializeObject<dynamic>(message);
                    int year = (int)json.year;
                    var chartData = await FetchInventoryData(year);
                    string jsonData = JsonConvert.SerializeObject(chartData, Formatting.Indented);
                    System.Diagnostics.Debug.WriteLine($"Data sent to WebView2: {jsonData}");
                    string escapedJson = HttpUtility.JavaScriptStringEncode(jsonData);
                    string script = $"try {{ updateInventoryChart(JSON.parse(\"{escapedJson}\"), {year}); }} catch (e) {{ logDebug('Script error: ' + e.message); }}";
                    System.Diagnostics.Debug.WriteLine($"Executing script: {script}");
                    await webViewApps.ExecuteScriptAsync(script);
                }

                if (action.Contains("InsertSuccess"))
                {                                        
                    await NewRequestsObjects();
                }
                //For Maintenance tasks                

                if (currentHtml.EndsWith(Path.GetFileName(dashboardPath), StringComparison.OrdinalIgnoreCase))
                {
                    var messagePayload = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(e.WebMessageAsJson);
                    if (messagePayload.TryGetValue("action", out JsonElement actionElement) && actionElement.GetString() == "fetchDashboardData")
                    {
                        int currentYear = DateTime.Now.Year;
                        TSDMetrics printcardMetrics = _dbService.GetPrintcardSummaryMetrics();                    

                        List<MonthlyPrintcardCounts> monthlyPrintcardData = await _dbService.GetMonthlyPrintcardCounts(currentYear);
                        var dashboardResponse = new DashboardDataResponse
                        {
                            Action = "populateDashboard", // This action tells JS how to handle the data
                            PrintcardMetrics = printcardMetrics,
                            MonthlyPrintcardData = monthlyPrintcardData
                        };
                        string jsonResponse = JsonSerializer.Serialize(dashboardResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        await webViewApps.CoreWebView2.ExecuteScriptAsync($"window.chrome.webview.postMessage({jsonResponse});");
                        
                        string message = e.WebMessageAsJson;
                        System.Diagnostics.Debug.WriteLine($"Received message from JavaScript: {message}");                        
                    }
                    else if (messagePayload.TryGetValue("action", out actionElement) && actionElement.GetString() == "fetchPrintcards")
                    {
                        JsonElement filtersElement = messagePayload["filters"];

                        int? year = null;
                        if (filtersElement.TryGetProperty("year", out JsonElement yearProperty) && yearProperty.ValueKind == JsonValueKind.String && !string.IsNullOrEmpty(yearProperty.GetString()))
                        {
                            if (int.TryParse(yearProperty.GetString(), out int parsedYear))
                            {
                                year = parsedYear;
                            }
                        }

                        int? month = null;
                        if (filtersElement.TryGetProperty("month", out JsonElement monthProperty) && monthProperty.ValueKind == JsonValueKind.String && !string.IsNullOrEmpty(monthProperty.GetString()))
                        {
                            if (int.TryParse(monthProperty.GetString(), out int parsedMonth))
                            {
                                month = parsedMonth;
                            }
                        }

                        int limit = 25;
                        if (filtersElement.TryGetProperty("limit", out JsonElement limitProperty) && limitProperty.ValueKind == JsonValueKind.Number)
                        {
                            limit = limitProperty.GetInt32();
                        }

                        int offset = 0;
                        if (filtersElement.TryGetProperty("offset", out JsonElement offsetProperty) && offsetProperty.ValueKind == JsonValueKind.Number)
                        {
                            offset = offsetProperty.GetInt32();
                        }

                        string searchTerm = "";
                        if (filtersElement.TryGetProperty("search", out JsonElement searchProperty) && searchProperty.ValueKind == JsonValueKind.String)
                        {
                            searchTerm = searchProperty.GetString();
                        }

                        PaginatedPrintcardData paginatedData = await _dbService.GetPrintcardDataAsync(year, month, limit, offset, searchTerm);

                        var responseToJs = new
                        {
                            action = "populatePrintcards",
                            data = paginatedData.Data,
                            totalCount = paginatedData.TotalCount
                        };

                        string jsonResponse = JsonSerializer.Serialize(responseToJs, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                        await webViewApps.CoreWebView2.ExecuteScriptAsync($"window.chrome.webview.postMessage({jsonResponse});");
                    }
                    else if (messagePayload.TryGetValue("action", out actionElement) && actionElement.GetString() == "PrintcardPageReady")
                    {
                        Console.WriteLine("Printcard page reported ready.");
                    }
                }              
            }
            catch (System.Text.Json.JsonException jsonEx)
            {
                string errorMessage = $"Error parsing message from JavaScript: {jsonEx.Message}. Raw message: {e.WebMessageAsJson}"; // Changed to e.WebMessageAsJson
                var errorResponse = new { action = "error", message = errorMessage };
                string jsonError = JsonSerializer.Serialize(errorResponse);
                await webViewApps.CoreWebView2.ExecuteScriptAsync($"window.chrome.webview.postMessage({jsonError});");
                MessageBox.Show(errorMessage, "Communication Error (JSON Parsing)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error processing request: {ex.Message}. Please check database connection and query.";
                var errorResponse = new { action = "error", message = errorMessage };
                string jsonError = JsonSerializer.Serialize(errorResponse);
                await webViewApps.CoreWebView2.ExecuteScriptAsync($"window.chrome.webview.postMessage({jsonError});");
                MessageBox.Show(errorMessage, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private async void webViewApps_NavigationCompleted_1(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
            {
                MessageBox.Show($"Navigation failed: {e.WebErrorStatus}", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string currentHtml = webViewApps.Source.LocalPath; // Get the local path of the loaded HTML

            string dashboardPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Dashboard.html");
            string printcardPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Printcard.html");
            string machineSchedulePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/MachineSchedule.html");
            string listRequests = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Requests.html");
            string listNewRequest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/NewRequest.html");
     
            if (currentHtml.EndsWith(Path.GetFileName(dashboardPath), StringComparison.OrdinalIgnoreCase))
            {                
                if (_loadDashboardDataAfterNavigation) // Check the flag only for dashboard
                {
                    _loadDashboardDataAfterNavigation = false;
                    await SendPrintcardsMetricDataToWebView(); // Populates metrics                                       

                    //await SendChartDataToWebView();             // Populates chart
                }
            }
            else if (currentHtml.EndsWith(Path.GetFileName(printcardPath), StringComparison.OrdinalIgnoreCase))
            {
                await SetupPrintcardHostObject();
            }
            else if (currentHtml.EndsWith(Path.GetFileName(machineSchedulePath), StringComparison.OrdinalIgnoreCase))
            {
                await SetupMachineHostObject();
            }
            else if (currentHtml.EndsWith(Path.GetFileName(listRequests), StringComparison.OrdinalIgnoreCase))
            {
                await SetupRequestsHostObject();
            }
            else if (currentHtml.EndsWith(Path.GetFileName(listNewRequest), StringComparison.OrdinalIgnoreCase))
            {
                await NewRequestsObjects();
            }

        }

        private async Task NewRequestsObjects()
        {
            string currentUsername = AppSession.CurrentUsername;
            string currentFullName = AppSession.CurrentFullName;
            int currentUserId = AppSession.CurrentUserId;
            string escapedUsername = currentUsername?.Replace("'", "\\'").Replace("\\", "\\\\") ?? "";
            string script = $"setLoggedUsername('{escapedUsername}');";
            await webViewApps.CoreWebView2.ExecuteScriptAsync(script);

            script = $"setLoggedUserId('{currentUserId}');";
            await webViewApps.CoreWebView2.ExecuteScriptAsync(script);

            await webViewApps.EnsureCoreWebView2Async(null); // Ensure initialized if not already
            webViewApps.CoreWebView2.AddHostObjectToScript("requestsApi", _dbService);
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
            await webViewApps.CoreWebView2.ExecuteScriptAsync($"document.getElementById('requisition_number').value = '{nextControlNumber}';");           
        }

        private async Task SetupRequestsHostObject()
        {            
            string currentUsername = AppSession.CurrentUsername;
            int currentUserId = AppSession.CurrentUserId;
            string escapedUsername = currentUsername?.Replace("'", "\\'").Replace("\\", "\\\\") ?? "";
            string script = $"setLoggedUsername('{escapedUsername}');";
            await webViewApps.CoreWebView2.ExecuteScriptAsync(script);

            //await webViewSideBar.EnsureCoreWebView2Async(null);        
            //webViewApps.CoreWebView2.AddHostObjectToScript("appBridge", new AppBridge(this, webViewSideBar));

            // Optional: If you want to initially disable the sidebar or set its state
            // You could call AppBridge's method here too, or just set webViewSideBar.Enabled directly.
            // webViewSideBar.Enabled = true; // Ensure it's enabled by default

            await webViewApps.EnsureCoreWebView2Async(null); // Ensure initialized if not already
            webViewApps.CoreWebView2.AddHostObjectToScript("requestsApi", _dbService);     
        }    
       
        private void webViewApps_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            string currentHtml = webViewApps.Source.LocalPath;
            string dashboardPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebResources/Dashboard.html");
            if (currentHtml.EndsWith(Path.GetFileName(dashboardPath), StringComparison.OrdinalIgnoreCase))
            {
                webViewApps.CoreWebView2.WebMessageReceived += webViewApps_WebMessageReceived;
            }

        }

        private void MainForm_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ShowLoginForm();
        }
        private void ShowLoginForm()
        {
            this.Visible = false;
            DialogResult result = new Login(_config).ShowDialog(); // Show the login form as a dialog  
            if (result != DialogResult.OK)
            {
                // If the user did not log in successfully, close the main form                               
                this.Close();
            }
            else
            {
                InitializeAsync();
                //background.Close(); // Close the background form once the main form is initialized
                this.Visible = true;
                this.Activate(); // Bring the main form to the front
            }
        }

        private async void webViewSideBar_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // Make sure AppSession and its properties are accessible here.
            string currentFullName = AppSession.CurrentFullName;
            string currentUsername = AppSession.CurrentUsername;

            // Sanitize inputs to prevent JavaScript injection issues, especially if they come from user input.
            // Replace single quotes with escaped quotes, backslashes, etc.
            string escapedFullName = currentFullName?.Replace("'", "\\'").Replace("\\", "\\\\") ?? "";
            string escapedUsername = currentUsername?.Replace("'", "\\'").Replace("\\", "\\\\") ?? "";

            // Construct the JavaScript call
            string script = $"updateUserInfo('{escapedFullName}', '{escapedUsername}');";

            // Execute the JavaScript
            await webViewSideBar.CoreWebView2.ExecuteScriptAsync(script);           

        }

        private System.Timers.Timer _refreshTimer;

        void SetupTimer()
        {
            _refreshTimer = new System.Timers.Timer(30000); // 30 seconds
            _refreshTimer.Elapsed += async (s, e) =>
            {
                // Marshal the call to the UI thread
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(async () => await UpdateActivityData()));
                }
                else
                {
                    await UpdateActivityData();
                }
            };
            _refreshTimer.Start();
        }

        public async Task UpdateActivityData()
        {
            var activities = await GetRecentActivitiesAsync();
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(async () =>
                {
                    await webViewSideBar.ExecuteScriptAsync($"updateActivities({JsonConvert.SerializeObject(activities)})");
                }));
            }
            else
            {
                await webViewSideBar.ExecuteScriptAsync($"updateActivities({JsonConvert.SerializeObject(activities)})");
            }
        }

        private async Task<List<ActivityItem>> GetRecentActivitiesAsync()
        {
            var activities = new List<ActivityItem>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = @"
                           SELECT *
                                 FROM (
                                     SELECT DISTINCT ON (activity_title)
                                         to_char(action_tstamp_tx,'MM/DD/YYYY AM') AS ""Date"",   
                                         table_name AS table_name,
                                         CASE 
                                             WHEN table_name = 'diecut_tools' THEN 
                                                 'Diecut #' || (row_data -> 'diecut_number') || ' ' || 
                                                 CASE WHEN action = 'I' THEN 'created' 
                                                      WHEN action = 'U' THEN 'updated' 
                                                      WHEN action = 'D' THEN 'deleted' 
                                                 END
                                             WHEN table_name = 'tooling_requests' THEN
                                                 'Requisition #:' || (row_data -> 'requisition_number') || ' ' ||
                                                 CASE WHEN action = 'I' THEN 'created'
                                                      WHEN action = 'U' THEN 'updated'
                                                      WHEN action = 'D' THEN 'deleted'
                                                 END
                                             WHEN table_name = 'rubberdie_plates' THEN 
                                                 'Rubberdie plate #' || (row_data -> 'id') || ' ' || 
                                                 CASE WHEN action = 'I' THEN 'created' 
                                                      WHEN action = 'U' THEN 'updated' 
                                                      WHEN action = 'D' THEN 'deleted' 
                                                 END
                                             ELSE table_name || ' record modified'
                                         END AS activity_title,

                                         CONCAT_WS(', ',
                                             CASE 
                                                 WHEN FLOOR(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)) / 86400)::int > 1 
                                                 THEN FLOOR(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)) / 86400)::int || ' days'
                                             END,
                                             CASE 
                                                 WHEN FLOOR(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)) / 86400)::int = 1 
                                                 THEN '1 day'
                                             END,
                                             CASE
				                                    WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 86400) / 3600)::int > 1
				                                    THEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 86400) / 3600)::int || ' hours'
				                                END,
                                             CASE 
                                                 WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 86400) / 3600)::int = 1 
                                                 THEN '1 hour'
                                             END,
                                             CASE 
                                                 WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 3600) / 60)::int > 1 
                                                 THEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 3600) / 60)::int || ' minutes ago'
                                             END,
                                             CASE 
                                                 WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 3600) / 60)::int = 1 
                                                 THEN '1 minute ago'
                                             END,
                                             CASE 
                                                 WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 60))::int < 60 AND 
                                                      FLOOR(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint / 60)::int = 0
                                                 THEN MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx))::bigint, 60)::int || ' seconds ago'
                                             END
                                         ) AS relative_time,
                                         action_tstamp_tx
                                     FROM audit.logged_actions
                                     ORDER BY activity_title, action_tstamp_tx DESC
                                 ) AS sub
                                 ORDER BY action_tstamp_tx DESC
                                 LIMIT 10;";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            activities.Add(new ActivityItem
                            {
                                Title = reader["activity_title"].ToString(),
                                Time = reader["relative_time"].ToString()
                            });
                        }
                    }
                }
            }

            return activities;
        }

        public class ActivityItem
        {
            public string Title { get; set; }
            public string Time { get; set; }
        }

        //Diecut distribution chart data (piechart)
        private async Task LoadDiecutPieAsync()
        {
            var labels = new List<string>();
            var counts = new List<int>();

            const string sql = @"
                SELECT ide.content AS status, COUNT(*) AS count
                FROM diecut_tools dc
                JOIN generic_status gs ON gs.id = dc.status_id
                JOIN international_description ide
                     ON ide.foreign_id = gs.status_value AND ide.table_id = 102
                GROUP BY ide.content
                ORDER BY ide.content;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    labels.Add(reader.GetString(0));
                    counts.Add(reader.GetInt32(1));
                }
            }

            /* Build a small JS snippet */
            string labelsJson = JsonSerializer.Serialize(labels);
            string countsJson = JsonSerializer.Serialize(counts);

            string js = $@"
        renderDiecutPie({labelsJson}, {countsJson});";

            await webViewApps.CoreWebView2.ExecuteScriptAsync(js);
        }

        private async Task LoadRubberdieAsync()
        {
            var labels = new List<string>();
            var counts = new List<int>();

            const string sql = @"
                SELECT ide.content AS status, COUNT(*) AS count
                    FROM rubberdie_plates rp
                    JOIN generic_status gs ON gs.id = rp.status_id
                    JOIN international_description ide
                         ON ide.foreign_id = gs.status_value AND ide.table_id = 103
                    GROUP BY ide.content
                    ORDER BY ide.content;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    labels.Add(reader.GetString(0));
                    counts.Add(reader.GetInt32(1));
                }
            }

            /* Build a small JS snippet */
            string labelsJson = JsonSerializer.Serialize(labels);
            string countsJson = JsonSerializer.Serialize(counts);

            string js = $@"
        renderRubberdiePie({labelsJson}, {countsJson});";

            await webViewApps.CoreWebView2.ExecuteScriptAsync(js);
        }

        private async Task LoadPrintcardAsync()
        {
            var labels = new List<string>();
            var counts = new List<int>();

            const string sql = @"
                SELECT ide.content AS status, COUNT(*) AS count
                    FROM printcard p
                    JOIN generic_status gs ON gs.id = p.printcard_status
                    JOIN international_description ide
                         ON ide.foreign_id = gs.status_value AND ide.table_id = 56
                    GROUP BY ide.content
                    ORDER BY ide.content;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    labels.Add(reader.GetString(0));
                    counts.Add(reader.GetInt32(1));
                }
            }

            /* Build a small JS snippet */
            string labelsJson = JsonSerializer.Serialize(labels);
            string countsJson = JsonSerializer.Serialize(counts);

            string js = $@"
        renderPrintcardPie({labelsJson}, {countsJson});";

            await webViewApps.CoreWebView2.ExecuteScriptAsync(js);
        }
        public async Task PushInventoryChartAsync(int year = 0)
        {
            var s = await GetInventorySeriesAsync(year);

            // Escape the JSON to ensure safe JS injection
            var months = JsonSerializer.Serialize(s.Months);
            var rubberdies = JsonSerializer.Serialize(s.Rubberdies);
            var diecuts = JsonSerializer.Serialize(s.Diecuts);
            var printcards = JsonSerializer.Serialize(s.Printcards);

            // Construct single-line JS call
            var js = $"renderInventoryBarChart({months}, {rubberdies}, {diecuts}, {printcards});";

            // Optional: Log to debug
            Console.WriteLine("Injected JS: " + js);

            // Inject into WebView2
            await webViewApps.CoreWebView2.ExecuteScriptAsync(js);
        }

        public record InventorySeries(
            List<string> Months,
            List<int> Rubberdies,
            List<int> Diecuts,
            List<int> Printcards);

        /// <summary>
        /// Gets monthly counts for rubberdie_plates, diecut_tools and printcard tables
        /// for the specified calendar year (default: current year).
        /// </summary>
        public async Task<InventorySeries> GetInventorySeriesAsync(int year = 0)
        {
            if (year == 0) year = DateTime.UtcNow.Year;

            string sql = $@"
            WITH all_months AS (
                        SELECT generate_series(1, 12) AS month_number
                    ),
                    rubberdie_counts AS (
                        SELECT EXTRACT(MONTH FROM date_created)::int AS month_num, COUNT(*) AS count
                        FROM rubberdie_plates
                        WHERE EXTRACT(YEAR FROM date_created) = @yr
                        GROUP BY month_num
                    ),
                    diecut_counts AS (
                        SELECT EXTRACT(MONTH FROM date_created)::int AS month_num, COUNT(*) AS count
                        FROM diecut_tools
                        WHERE EXTRACT(YEAR FROM date_created) = @yr
                        GROUP BY month_num
                    ),
                    printcard_counts AS (
                        SELECT EXTRACT(MONTH FROM date_created)::int AS month_num, COUNT(*) AS count
                        FROM printcard
                        WHERE EXTRACT(YEAR FROM date_created) = @yr
                        GROUP BY month_num
                    )
                    SELECT 
                        TO_CHAR(TO_DATE(am.month_number::text, 'MM'), 'Mon') AS month,
                        COALESCE(rd.count, 0) AS rubberdie,
                        COALESCE(dc.count, 0) AS diecut,
                        COALESCE(pc.count, 0) AS printcard
                    FROM 
                        all_months am
                    LEFT JOIN rubberdie_counts rd ON am.month_number = rd.month_num
                    LEFT JOIN diecut_counts dc ON am.month_number = dc.month_num
                    LEFT JOIN printcard_counts pc ON am.month_number = pc.month_num
                    ORDER BY am.month_number;";

            var months = new List<string>();
            var rubberdies = new List<int>();
            var diecuts = new List<int>();
            var printcards = new List<int>();

            await using var conn = new NpgsqlConnection(connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("yr", year);

            await using var rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                months.Add(rdr.GetString(0).Trim());     // "Jan", "Feb"…
                rubberdies.Add(rdr.GetInt32(1));
                diecuts.Add(rdr.GetInt32(2));
                printcards.Add(rdr.GetInt32(3));
            }

            return new InventorySeries(months, rubberdies, diecuts, printcards);
        }

        private async Task<object> FetchInventoryData(int year)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"
                        WITH all_months AS (
                            SELECT generate_series(1, 12) AS month_number
                        ),
                        rubberdie_counts AS (
                            SELECT EXTRACT(MONTH FROM date_created)::int AS month_num, COUNT(*) AS count
                            FROM rubberdie_plates
                            WHERE EXTRACT(YEAR FROM date_created) = @p1
                            GROUP BY month_num
                        ),
                        diecut_counts AS (
                            SELECT EXTRACT(MONTH FROM date_created)::int AS month_num, COUNT(*) AS count
                            FROM diecut_tools
                            WHERE EXTRACT(YEAR FROM date_created) = @p1
                            GROUP BY month_num
                        ),
                        printcard_counts AS (
                            SELECT EXTRACT(MONTH FROM date_created)::int AS month_num, COUNT(*) AS count
                            FROM printcard
                            WHERE EXTRACT(YEAR FROM date_created) = @p1
                            GROUP BY month_num
                        )
                        SELECT 
                            TO_CHAR(TO_DATE(am.month_number::text, 'MM'), 'Mon') AS month,
                            COALESCE(rd.count, 0) AS rubberdie_count,
                            COALESCE(dc.count, 0) AS diecut_count,
                            COALESCE(pc.count, 0) AS printcard_count
                        FROM 
                            all_months am
                        LEFT JOIN rubberdie_counts rd ON am.month_number = rd.month_num
                        LEFT JOIN diecut_counts dc ON am.month_number = dc.month_num
                        LEFT JOIN printcard_counts pc ON am.month_number = pc.month_num
                        ORDER BY am.month_number;";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("p1", year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var months = new List<string>();
                            var rubberdieCounts = new List<long>();
                            var diecutCounts = new List<long>();
                            var printcardCounts = new List<long>();

                            while (await reader.ReadAsync())
                            {
                                months.Add(reader.GetString(0)); // month
                                rubberdieCounts.Add(reader.GetInt64(1)); // rubberdie_count
                                diecutCounts.Add(reader.GetInt64(2)); // diecut_count
                                printcardCounts.Add(reader.GetInt64(3)); // printcard_count
                            }

                            // Log the raw data for debugging
                            System.Diagnostics.Debug.WriteLine($"Query returned {months.Count} rows");
                            for (int i = 0; i < months.Count; i++)
                            {
                                System.Diagnostics.Debug.WriteLine($"Month: {months[i]}, Rubberdie: {rubberdieCounts[i]}, Diecut: {diecutCounts[i]}, Printcard: {printcardCounts[i]}");
                            }

                            return new
                            {
                                labels = months,
                                datasets = new[]
                                {
                                    new { label = "Rubberdie Plates", data = rubberdieCounts, backgroundColor = "rgba(75, 192, 192, 0.6)", borderColor = "rgba(75, 192, 192, 1)", borderWidth = 1 },
                                    new { label = "Diecut Tools", data = diecutCounts, backgroundColor = "rgba(255, 159, 64, 0.6)", borderColor = "rgba(255, 159, 64, 1)", borderWidth = 1 },
                                    new { label = "Printcards", data = printcardCounts, backgroundColor = "rgba(153, 102, 255, 0.6)", borderColor = "rgba(153, 102, 255, 1)", borderWidth = 1 }
                                }
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
            }

            // Fallback data
            return new
            {
                labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
                datasets = new[]
                {
                    new { label = "Rubberdie Plates", data = new long[12], backgroundColor = "rgba(75, 192, 192, 0.6)", borderColor = "rgba(75, 192, 192, 1)", borderWidth = 1 },
                    new { label = "Diecut Tools", data = new long[12], backgroundColor = "rgba(255, 159, 64, 0.6)", borderColor = "rgba(255, 159, 64, 1)", borderWidth = 1 },
                    new { label = "Printcards", data = new long[12], backgroundColor = "rgba(153, 102, 255, 0.6)", borderColor = "rgba(153, 102, 255, 1)", borderWidth = 1 }
                }
            };
        }
     
        //Determine if a requisition number's status is pending
        private bool IsRequisitionPending(string requisitionNumber)
        {
            if (string.IsNullOrEmpty(requisitionNumber))
            {
                return false; // Invalid requisition number
            }
            // Check if the requisition number exists in the database and has a pending status
            string query = "SELECT COUNT(*) FROM tooling_requests WHERE requisition_number = @requisitionNumber AND status = 'Pending'";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("requisitionNumber", requisitionNumber);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Return true if there is at least one pending requisition
                }
            }
        }

        internal void RefreshRequestsList()
        {
            webViewApps.CoreWebView2.Reload();
        }
    }
}