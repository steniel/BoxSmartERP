using Antlr4.Runtime.Misc;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json; // For JSON serialization
using System.Text.Json.Serialization;

namespace BoxSmart_ERP.Services
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)] // To allow COM visibility
    public class PostgreSQLServices
    {        
        public PostgreSQLServices(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));     
        }        

        public async Task<string> InsertNewItem(string jsonData )
        {
            string suffixRequest = "";
            try
            { //
                dynamic request = JsonConvert.DeserializeObject(jsonData);
                if (request == null)
                {
                    throw new ArgumentException("Invalid JSON data provided.");
                }
                if (request.customerId == null || request.quantity == null || request.unit == null ||
                    request.requisitionNumber == null || request.dueDate == null || request.requestedDate == null ||
                    request.userId == null || request.aprintcardDescription == null || request.requestPriority == null
                    || request.printcardId == null)
                {
                    throw new ArgumentException("Missing required fields in the JSON data.");
                }
                if (request.userId <= 0)
                {
                    throw new ArgumentException("SessionUserID must be greater than zero.");
                }
               
            
                int customerId = request.customerId;                                
                int? printcardId = string.IsNullOrWhiteSpace($"{request.printcardId}") ? (int?)null : int.Parse($"{request.printcardId}");
                int quantity = request.quantity;
                int unit = request.unit;
                string itemNotes = request.itemNotes ?? string.Empty; // Default to empty string if null
                bool rubberdie = request.rubberdie ?? false; // Default to false if not provided
                bool diecutMould = request.diecutMould ?? false; // Default to false if not provided
                bool negativeFilm = request.negativeFilm ?? false; // Default to false if not provided
                bool requestInternal = request.requestInternal ?? false;
                bool requestExternal = request.requestExternal ?? false;
                string requisitionNumber = request.requisitionNumber.ToString();
                DateTime itemdueDate = request.dueDate;
                DateTime itemrequestedDate = request.requestedDate;
                int userId = request.userId;
                string diecutType = request.diecutType?.ToString() ?? "Rotary"; // Optional field for diecut type
                string requestPriority = request.requestPriority.ToString();
                int numOuts = request.numberOfColors; 
                string itemDescription = request.aprintcardDescription.ToString(); // Description of the item being requested
                int numberOfColors = request.numberOfColors != null ? (int)request.numberOfColors : 0; // Default to 0 if not provided


                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    //Remove suffixes and add them based on the request properties
                    requisitionNumber = requisitionNumber.TrimEnd('R', 'D'); // Remove existing suffixes
                    if (rubberdie) { suffixRequest = "R"; }
                    if (diecutMould) { suffixRequest += "D"; }

                    using var transaction = conn.BeginTransaction();
                    try
                    {
                        using var cmd = new NpgsqlCommand(
                            "INSERT INTO public.tooling_requests (customer_id, printcard_id, quantity, uom_id," +
                            "design_notes, rubberdie, diecut, negative_film, _internal_, _external_, " +
                            "requisition_number, due_date, requested_date, status, assigned_to, user_id, item_description,priority,num_outs, updated_by) " +
                            "VALUES (@customerId, @printcardId, @quantity, @uom, @design_notes, @rubberdie, " +
                            "@diecutMould, @negativeFilm, @requestInternal, @requestExternal, @_requisitionNumber, " +
                            "@dueDate, @requestedDate, @status, @assigned_to, @SessionUserID, @ItemDescription, @priority, @num_outs,@updated_by) " +
                            "RETURNING request_id;",
                            conn, transaction);

                        cmd.Parameters.AddWithValue("customerId", customerId);                        
                        cmd.Parameters.AddWithValue("printcardId", (object?)printcardId ?? DBNull.Value).NpgsqlDbType = NpgsqlDbType.Integer;
                        cmd.Parameters.AddWithValue("quantity", quantity);
                        cmd.Parameters.AddWithValue("uom", unit);
                        cmd.Parameters.AddWithValue("design_notes", itemNotes);
                        cmd.Parameters.AddWithValue("rubberdie", rubberdie);
                        cmd.Parameters.AddWithValue("diecutMould", diecutMould);
                        cmd.Parameters.AddWithValue("negativeFilm", negativeFilm);
                        cmd.Parameters.AddWithValue("requestInternal", requestInternal);
                        cmd.Parameters.AddWithValue("requestExternal", requestExternal);
                        cmd.Parameters.AddWithValue("_requisitionNumber", requisitionNumber + suffixRequest);
                        cmd.Parameters.AddWithValue("dueDate", itemdueDate);
                        cmd.Parameters.AddWithValue("requestedDate", itemrequestedDate);
                        cmd.Parameters.AddWithValue("status", "Pending");
                        cmd.Parameters.AddWithValue("assigned_to", 10); //Since this is a new request, we assign it to the developer (10), which translate to Not Yet Assigned
                        cmd.Parameters.AddWithValue("SessionUserID", userId);
                        cmd.Parameters.AddWithValue("ItemDescription", itemDescription);
                        cmd.Parameters.AddWithValue("priority", requestPriority);
                        cmd.Parameters.AddWithValue("num_outs", numOuts);
                        cmd.Parameters.AddWithValue("updated_by", userId);

                        long toolingRequestId = (long)cmd.ExecuteScalar();

                        if (diecutMould)
                        {
                            using var diecutCmd = new NpgsqlCommand(
                                "INSERT INTO public.diecut_tools (type, notes, request_id) " +
                                "VALUES (@type::diecut_type, @diecutNotes, @toolingRequestId);",
                                conn, transaction); // Pass the transaction object                                                                                                
                            diecutCmd.Parameters.AddWithValue("type", diecutType);
                            diecutCmd.Parameters.AddWithValue("diecutNotes", itemNotes);
                            diecutCmd.Parameters.AddWithValue("toolingRequestId", toolingRequestId);
                            diecutCmd.ExecuteNonQuery();
                        }

                        if (rubberdie)
                        {
                            using var rubberdieCmd = new NpgsqlCommand(
                                "INSERT INTO public.rubberdie_plates (notes, request_id, num_colors) VALUES (@rubberdieNotes, @toolingRequestId, @numColors);",
                                conn, transaction); // Pass the transaction object
                            rubberdieCmd.Parameters.AddWithValue("rubberdieNotes", itemNotes);
                            rubberdieCmd.Parameters.AddWithValue("toolingRequestId", toolingRequestId);
                            rubberdieCmd.Parameters.AddWithValue("numColors", numberOfColors);
                            rubberdieCmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return "success";
                    }
                    catch (NpgsqlException ex)
                    {
                        transaction.Rollback();
                        LogErrorMessage(ex, "InsertNewItem");
                        MessageBox.Show("An error has occurred, see log file.: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }                    
                }                
            }
            catch (Exception ex)
            {
                OnMessageToWebView?.Invoke("InsertFailed");
                MessageBox.Show("Error deserializing JSON data: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogErrorMessage(ex, "InsertNewItem");
                return "error: " + ex.Message;
            }
        }
        public void InsertMaintenance(string jsonData)
        {
            try
            {
                var maintenance = JsonConvert.DeserializeObject<MaintenanceModel>(jsonData);
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    var maintenanceItems = new List<MaintenanceModel> { maintenance };
                    {
                        using var transaction = conn.BeginTransaction();
                        try
                        {
                            using var cmd = new NpgsqlCommand(
                                "INSERT INTO die_maintenance_log(diecut_id, maintenance_date, user_id, notes, RepairTypeID) VALUES(@p1, @p2, @p3, @p5, @p6);", conn, transaction);
                            cmd.Parameters.AddWithValue("p1", maintenance.DiecutId);
                            cmd.Parameters.AddWithValue("p2", DateTime.Parse(maintenance.MaintenanceDate));
                            cmd.Parameters.AddWithValue("p3", maintenance.UserId);                            
                            cmd.Parameters.AddWithValue("p5", maintenance.Notes);
                            cmd.Parameters.AddWithValue("p6", maintenance.ActionId);

                            transaction.Commit();
                        }
                        catch (NpgsqlException ex)
                        {
                            transaction.Rollback();
                            OnMessageToWebView?.Invoke("InsertMaintenanceFailed");
                            LogErrorMessage(ex, "InsertMaintenance");
                            MessageBox.Show("An error has occurred, see log file.: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }
                    OnMessageToWebView?.Invoke("InsertMaintenanceSuccess");
                }
            }
            catch (Exception ex)
            {
                OnMessageToWebView?.Invoke("InsertMaintenanceFailed");
                MessageBox.Show("Error deserializing JSON data: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogErrorMessage(ex, "InsertMaintenance");
                return;
            }
        }
        public static void LogErrorMessage(Exception ex, string callFunction)
        {
            string logDirectory = @"C:\Logs";
            string logFileName = "boxerp_errors.log";
            string logFilePath = Path.Combine(logDirectory, logFileName);
            long maxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB
            int maxArchiveFiles = 10; // Keep only the latest 10 logs
            int maxDaysOld = 30; // Delete logs older than 30 days
            string errorMessage = $"[{DateTime.Now}] Error in function {callFunction}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
            try
            {
                Directory.CreateDirectory(logDirectory);

                // Rotate log if size exceeded
                if (File.Exists(logFilePath))
                {
                    FileInfo fileInfo = new FileInfo(logFilePath);
                    if (fileInfo.Length >= maxFileSizeInBytes)
                    {
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string archiveName = $"boxerp_errors_{timestamp}.log";
                        string archivePath = Path.Combine(logDirectory, archiveName);
                        File.Move(logFilePath, archivePath);
                    }
                }

                // Append new error
                File.AppendAllText(logFilePath, errorMessage);

                // Clean up old archive logs
                var archivedLogs = Directory.GetFiles(logDirectory, "boxerp_errors_*.log")
                                            .Select(f => new FileInfo(f))
                                            .OrderByDescending(f => f.CreationTime)
                                            .ToList();

                // Delete logs older than maxDaysOld
                foreach (var file in archivedLogs)
                {
                    if ((DateTime.Now - file.CreationTime).TotalDays > maxDaysOld)
                    {
                        file.Delete();
                    }
                }
                // Keep only the latest X archives
                var excessFiles = archivedLogs.Skip(maxArchiveFiles);
                foreach (var file in excessFiles)
                {
                    file.Delete();
                }
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Failed to write to log file: {logEx.Message}");
            }

            Console.WriteLine(errorMessage);
        }

        //Method to update tooling_requests with the assigned person and status to In Development' when the maintenance is assigned to a user
        [ComVisible(true)]
        public void UpdateRequestStatus(string requisitionNumber, int assignedTo, string status)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE tooling_requests 
                        SET assigned_to = @assignedTo, status = @status 
                        WHERE requisition_number = @requisitionNumber;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@assignedTo", assignedTo);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@requisitionNumber", requisitionNumber);
                        cmd.ExecuteNonQuery();
                    }
                    //Update other related table

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating maintenance status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetRecentRequests()
        {
            var results = new List<Dictionary<string, object>>();
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"
                            SELECT requisition_number, 
                                CASE
                                    WHEN diecut IS NULL OR rubberdie IS NULL THEN 'Invalid'
                                    WHEN diecut = true AND rubberdie = true THEN 'Diecut/Rubberdie'
                                    WHEN diecut = false AND rubberdie = true THEN 'Rubberdie'
                                    ELSE 'Diecut'
                                END AS request_type,
                                co.organization_name as customer,
                                status,
                                due_date
                            FROM tooling_requests tr
                            LEFT JOIN customer cu ON tr.customer_id = cu.id
                            LEFT JOIN contact co ON co.id = cu.contact_id
                            WHERE requested_date >= CURRENT_DATE - INTERVAL '6 weeks'
                            ORDER BY due_date DESC
                            LIMIT 10;";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new Dictionary<string, object>
                            {
                                ["requisition_number"] = reader["requisition_number"]?.ToString(),
                                ["request_type"] = reader["request_type"]?.ToString(),
                                ["customer"] = reader["customer"]?.ToString(),
                                ["status"] = reader["status"]?.ToString(),
                                ["due_date"] = reader["due_date"] is DateTime dt ? dt.ToString("yyyy-MM-dd") : reader["due_date"]?.ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching recent maintenanceItems: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return System.Text.Json.JsonSerializer.Serialize(results);
        }
        public class RequestModel
        {
            public required int CustomerId { get; set; }
            public int? PrintcardId { get; set; }
            public required int ItemQuantity { get; set; }
            public required int ItemColors { get; set; }
            public required int ItemUnit { get; set; }
            public string ItemRemarks { get; set; }
            public bool Rubberdie { get; set; }
            public bool DiecutMould { get; set; }
            public string DiecutType { get; set; } // Optional field for diecut type (Rotary or Flatbed)
            public bool NegativeFilm { get; set; }
            public bool RequestInternal { get; set; }
            public bool RequestExternal { get; set; }
            public required string UserName { get; set; }
            public required string RequisitionNumber { get; set; }
            public required string DueDate { get; set; }
            public required string RequestedDate { get; set; }
            public string RequestStatus { get; set; } = "Pending"; // Default status
            public int AssignedTo { get; set; } = 10; // Default to developer equals unassigned.
            public required int UserId { get; set; } // User ID of the person making the maintenance
            public required string ItemDescription { get; set; } // Description of the item being requested
            public required string RequestPriority { get; set; } // Priority of the maintenance (e.g., High, Normal, Low)
            public required int NumOuts { get; set; } // Number of outs for the maintenance            
            public string PdfUrl { get; set; }
        }
        public class MaintenanceModel
        {
            public int DiecutId { get; set; }
            public string MaintenanceDate { get; set; } // Date of maintenance
            public int UserId { get; set; } // ID of the user performing the maintenance
            public int EstimatedDowntime { get; set; } // Estimated downtime in hours
            public string Notes { get; set; } // Additional notes or comments
            public int ActionId { get; set; } // Action ID related to the maintenance
        }
        public class Printcard
        {
            public int id { get; set; }
            public string box_description { get; set; }
        }
        public class User
        {
            public int id { get; set; }
            public string fullname { get; set; }
        }
        public async Task<string> SearchPrintcards(string searchTerm, int customerID)
        {
            var printcards = new List<Printcard>();
            string jsonResult = "[]";
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
            {
                return jsonResult;
            }
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var sql = "SELECT printcardid, box_description FROM public.tsdprintcardbrowser WHERE box_description ILIKE @searchTerm AND id = @customerID AND status='Active' LIMIT 10;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("searchTerm", $"%{searchTerm}%");
                        cmd.Parameters.AddWithValue("customerID", customerID);
                        using var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            printcards.Add(new Printcard
                            {
                                id = reader.GetInt32(reader.GetOrdinal("printcardid")),
                                box_description = reader.GetString(reader.GetOrdinal("box_description"))
                            });
                        }
                    }
                }
                jsonResult = System.Text.Json.JsonSerializer.Serialize(printcards, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "SearchPrintcard");
                Console.WriteLine($"Error in SearchPrintcard: {ex.Message}");                
                jsonResult = "[]"; 
            }
            return jsonResult;
        }

        public class Diecut
        {
            public int id { get; set; }
            public string item_description { get; set; }
            public string diecut_type { get; set; } // Rotary or Flatbed
            public string status { get; set; } // Active, Inactive, etc.
        }

        public async Task<string> SearchDiecuts(string searchTerm)
        {
            var diecuts = new List<Diecut>();
            string jsonResult = "[]";
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
            {
                return jsonResult;
            }
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var sql = "SELECT diecutid, item_description FROM public.diecutview WHERE item_description ILIKE @searchTerm AND status='Active' LIMIT 10;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("searchTerm", $"%{searchTerm}%");
                        using var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            diecuts.Add(new Diecut
                            {
                                id = reader.GetInt32(reader.GetOrdinal("diecutid")),
                                item_description = reader.GetString(reader.GetOrdinal("item_description"))
                            });
                        }
                    }
                }
                jsonResult = System.Text.Json.JsonSerializer.Serialize(diecuts, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchPrintcard: {ex.Message}");                
                jsonResult = "[]"; // Return empty array on error
                LogErrorMessage(ex, "SearchDiecuts");
            }
            return jsonResult;
        }
        public async Task<string> SearchUsers(string searchTerm)
        {
            var users = new List<User>();
            string jsonResult = "[]";

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var sql = "SELECT id,fullname FROM systemusers WHERE fullname ILIKE @searchTerm AND department_id=70 AND activeuser=true;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("searchTerm", $"%{searchTerm}%");
                        using var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User
                            {
                                id = reader.GetInt32(reader.GetOrdinal("id")),
                                fullname = reader.GetString(reader.GetOrdinal("fullname"))
                            });
                        }
                    }
                }
                jsonResult = System.Text.Json.JsonSerializer.Serialize(users, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchUsers: {ex.Message}");
                LogErrorMessage(ex, "SearchUsers");
                jsonResult = "[]"; 
            }
            return jsonResult;
        }

        public class Customer
        {
            public int id { get; set; }
            public required string organization_name { get; set; }
        }

        public class UserFullname
        {
            public required string Fullname { get; set; }
            public int UserId { get; set; }
        }

        // DTO for monthly counts (will primarily be used for Printcards initially)
        public class MonthlyPrintcardCounts
        {
            [JsonPropertyName("month")]
            public int Month { get; set; } // 1 (Jan) to 12 (Dec)
            [JsonPropertyName("count")]
            public double Count { get; set; }
        }

        // DTO for the overall TSD metrics
        public class TSDMetrics
        {
            [JsonPropertyName("totalPrintcards")]
            public int TotalPrintcards { get; set; }
            [JsonPropertyName("addedThisWeek")]
            public int AddedThisWeek { get; set; }
            [JsonPropertyName("addedThisMonth")]
            public int AddedThisMonth { get; set; } // Optional, if you want to track this too
            [JsonPropertyName("totalRequests")]
            public int TotalRequests { get; set; } // Total number of maintenanceItems for the past 1 year

            //Next field will be used for the next control number of maintenanceItems (S - 2025 / NNN) Where NNN is the next number
            [JsonPropertyName("nextControlNumber")]
            public string NextControlNumber { get; set; } // Next control number for maintenanceItems

            [JsonPropertyName("pendingRequests")]
            public int PendingRequests { get; set; } // Number of pending maintenanceItems

            [JsonPropertyName("highPriorityRequests")]
            public int HighPriorityRequests { get; set; } // Number of high priority maintenanceItems

            [JsonPropertyName("inDevelopmentRequests")]
            public int InDevelopmentRequests { get; set; } // Number of maintenanceItems in development

            [JsonPropertyName("thisWeekCompletedRequests")]
            public int ThisWeekCompletedRequests { get; set; } // Number of maintenanceItems completed this week

            [JsonPropertyName("completedRequests")]
            public int CompletedRequests { get; set; } // Number of completed maintenanceItems
            [JsonPropertyName("rejectedRequests")]
            public int RejectedRequests { get; set; } // Number of rejected maintenanceItems
            [JsonPropertyName("archivedRequests")]
            public int ArchivedRequests { get; set; } // Number of archived maintenanceItems
            [JsonPropertyName("totalActiveDiecuts")]
            public int TotalActiveDiecuts { get; set; } // Total number of active diecuts
            [JsonPropertyName("diecutUsageLimit")]
            public int DiecutUsageLimit { get; set; } // Usage limit for diecuts
            [JsonPropertyName("rubberdieUsageLimit")]
            public int RubberdieUsageLimit { get; set; } // Usage limit for rubberdie
            [JsonPropertyName("totalDiecutInMaintenance")]
            public int TotalDiecutInMaintenance { get; set; } // Total number of diecuts in maintenance
            [JsonProperty("totalDisposed")]
            public int totalDiecutDisposed { get; set; } // Total number of diecuts disposed

            [JsonPropertyName("totalActiveRubberdie")]
            public int TotalActiveRubberdie { get; set; } // Total number of active diecuts

            [JsonPropertyName("printcardCounts")]
            public List<MonthlyPrintcardCounts> PrintcardCounts { get; set; } = new List<MonthlyPrintcardCounts>(); // Monthly counts for Printcards)

        }

        public class RequestedItems
        {
            public int requestId { get; set; }
            public string customerName { get; set; } // Name of the customer associated with the maintenance           
            public int quantity { get; set; } // Quantity of the item requested
            public string uomId { get; set; }
            public int numOuts { get; set; }
            public string itemDescription { get; set; }
            public string itemRemarks { get; set; } // Remarks or notes about the item
            public string itemPriority { get; set; } // Priority of the item (e.g., High, Normal, Low)

        }

        public string GetRequestsDataTable(string requisitionNumber)
        {
            List<RequestedItems> requestedItems = new List<RequestedItems>();

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"
                SELECT request_id, requisition_number, customer_id, co.organization_name as customer_name, printcard_id, quantity, u.uom_code as unit, 
                       design_notes as item_remarks, priority, rubberdie, diecut, negative_film, 
                       _internal_, _external_, assigned_to,num_outs as outs, priority, requested_date, due_date, status, tr.user_id, item_description
                FROM tooling_requests tr
                LEFT JOIN uom u ON u.id = tr.uom_id
                LEFT JOIN customer cu ON tr.customer_id = cu.id
                LEFT JOIN contact co ON cu.contact_id = co.id
                WHERE requisition_number = @requisitionNumber;";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@requisitionNumber", requisitionNumber);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                requestedItems.Add(new RequestedItems
                                {
                                    requestId = reader.GetInt32(reader.GetOrdinal("request_id")),
                                    customerName = reader.IsDBNull(reader.GetOrdinal("customer_name")) ? null : reader.GetString(reader.GetOrdinal("customer_name")),
                                    quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                    uomId = reader.GetString(reader.GetOrdinal("unit")),
                                    numOuts = reader.GetInt32(reader.GetOrdinal("outs")),
                                    itemDescription = reader.GetString(reader.GetOrdinal("item_description")),
                                    itemRemarks = reader.IsDBNull(reader.GetOrdinal("item_remarks")) ? null : reader.GetString(reader.GetOrdinal("item_remarks")),
                                    itemPriority = reader.GetString(reader.GetOrdinal("priority"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching maintenanceItems data: {ex.Message}");
                return System.Text.Json.JsonSerializer.Serialize(new { data = new List<RequestedItems>() }); // empty data array
            }

            return System.Text.Json.JsonSerializer.Serialize(
                new { data = requestedItems },
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
            );
        }

        // DTO getting attributes from specific control number / requisition number
        public class RequestAttributes
        {
            [JsonPropertyName("customerId")]
            public int CustomerId { get; set; }
            [JsonPropertyName("customerName")]
            public string CustomerName { get; set; } // Name of the customer associated with the maintenance

            [JsonPropertyName("printcardId")]
            public int? PrintcardId { get; set; } // Nullable if not applicable
            [JsonPropertyName("itemQuantity")]
            public int ItemQuantity { get; set; }
            [JsonPropertyName("itemUnit")]
            public string ItemUnit { get; set; }
            [JsonPropertyName("itemRemarks")]
            public string ItemRemarks { get; set; }
            [JsonPropertyName("rubberdie")]
            public bool Rubberdie { get; set; }
            [JsonPropertyName("diecutMould")]
            public bool DiecutMould { get; set; }
            [JsonPropertyName("negativeFilm")]
            public bool NegativeFilm { get; set; }
            [JsonPropertyName("requestInternal")]
            public bool RequestInternal { get; set; }
            [JsonPropertyName("requestExternal")]
            public bool RequestExternal { get; set; }
            [JsonPropertyName("assignedTo")]
            public int AssignedTo { get; set; } // User ID of the person assigned to the maintenance
            [JsonPropertyName("numOuts")]
            public int NumOuts { get; set; } // Number of outs for the maintenance
            [JsonPropertyName("priority")]
            public string RequestPriority { get; set; } // Priority of the maintenance (e.g., High, Normal, Low)
            [JsonPropertyName("dueDate")]
            public string DueDate { get; set; } // Due date for the maintenance
            [JsonPropertyName("requestedDate")]
            public string RequestedDate { get; set; } // Date when the maintenance was made
        }

        // DTO to combine all dashboard data to send to JavaScript
        public class DashboardDataResponse
        {
            [JsonPropertyName("action")]
            public string Action { get; set; } = "populateDashboard"; // Action to tell JS what kind of data it is
            [JsonPropertyName("printcardMetrics")]
            public TSDMetrics PrintcardMetrics { get; set; }
            [JsonPropertyName("monthlyPrintcardData")]
            public List<MonthlyPrintcardCounts> MonthlyPrintcardData { get; set; }
            // Add other sections here as you implement them (e.g., MonthlyRubberdieData, MonthlyDiecutData)


        }

        // Class to hold both data and total count for pagination
        public class PaginatedPrintcardData
        {
            [JsonPropertyName("data")]
            public List<PrintcardData> Data { get; set; }

            [JsonPropertyName("totalCount")]
            public int TotalCount { get; set; }
        }

        // Class to hold your Printcard data
        public class PrintcardData
        {
            [JsonPropertyName("printcardid")]
            public string Printcardid { get; set; }

            [JsonPropertyName("organization_name")]
            public string OrganizationName { get; set; }

            [JsonPropertyName("box_description")]
            public string BoxDescription { get; set; }

            [JsonPropertyName("printcardno")]
            public string Printcardno { get; set; }

            [JsonPropertyName("boardsize")]
            public string Boardsize { get; set; }

            [JsonPropertyName("insidedimension")]
            public string Insidedimension { get; set; }

            [JsonPropertyName("filename")]
            public string Filename { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; } // Ensure this column exists in your DB and query

            [JsonPropertyName("date_created")]
            public DateTime DateCreated { get; set; }
        }
        private readonly string _connectionString;

        // Implement SearchCustomers
        public async Task<string> SearchCustomers(string searchTerm)
        {
            var customers = new List<Customer>();
            string jsonResult = "[]";

            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
            {
                return jsonResult;
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var sql = "SELECT id, organization_name FROM public.customer_list WHERE organization_name ILIKE @searchTerm LIMIT 10;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("searchTerm", $"%{searchTerm}%");
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                customers.Add(new Customer
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    organization_name = reader.GetString(reader.GetOrdinal("organization_name"))
                                });
                            }
                        }
                    }
                }
                jsonResult = System.Text.Json.JsonSerializer.Serialize(customers, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchCustomers: {ex.Message}");
                LogErrorMessage(ex, "SearchCustomers"); 
                jsonResult = "[]"; 
            }

            return jsonResult;
        }

        public class PrintcardItem
        {
            public int PrintcardId { get; set; }
            public string OrganizationName { get; set; }
            public string BoxDescription { get; set; }
            public string PrintcardNo { get; set; }
            public string BoardSize { get; set; }
            public string InsideDimension { get; set; }
            public string Filename { get; set; }
            public string Status { get; set; }
            public string FileType { get; set; }
            public DateTime DateCreated { get; set; }
        }

        // This class is specifically for DataTables server-side response format
        public class DataTableResponse
        {
            public int Draw { get; set; }
            public int RecordsTotal { get; set; }
            public int RecordsFiltered { get; set; }
            public List<PrintcardItem> Data { get; set; }
        }
        public string GetPrintcardsForDataTable(int draw, int start, int length, string searchValue, string dateYear, string dateMonth)
        {
            List<PrintcardItem> printcardItems = new List<PrintcardItem>();
            int recordsTotal = 0;    // Total records in DB without any filters
            int recordsFiltered = 0; // Total records after applying date/search filters

            string baseWhereClause = " WHERE status = 'Active' ";
            string dateFilterClause = "";
            string searchFilterClause = "";

            // Conditionally build date filter clause
            bool hasDateFilter = !string.IsNullOrEmpty(dateYear);
            if (hasDateFilter)
            {
                string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                dateFilterClause = $" AND to_char(date_created, '{paramFormat}') = @yearMonth ";
            }

            // Build search filter clause if search value is provided
            bool hasSearch = !string.IsNullOrEmpty(searchValue);
            if (hasSearch)
            {
                // IMPORTANT: Adjust these columns based on what you want to be searchable
                searchFilterClause = $@"
                AND (
                    organization_name ILIKE @searchValue OR
                    box_description ILIKE @searchValue OR
                    printcardno ILIKE @searchValue OR
                    boardsize ILIKE @searchValue OR
                    rubber_location ILIKE @searchValue OR
                    insidedimension ILIKE @searchValue OR
                    filename ILIKE @searchValue
                    -- Add more columns as needed
                )
            ";
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Get total records (without any filters)
                    string countTotalQuery = $"SELECT COUNT(*) FROM browseprintcard;";
                    using (var cmd = new NpgsqlCommand(countTotalQuery, conn))
                    {
                        recordsTotal = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. Get filtered records count (with date filter, and/or search filter if present)
                    string countFilteredQuery = $"SELECT COUNT(*) FROM browseprintcard {baseWhereClause} {dateFilterClause} {searchFilterClause};";
                    using (var cmd = new NpgsqlCommand(countFilteredQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        recordsFiltered = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Get the actual paged and filtered data
                    string dataQuery = $@"
                    SELECT printcardid, organization_name, box_description, printcardno, boardsize, 
                           insidedimension, filename, status, filetype, date_created
                    FROM browseprintcard
                    {baseWhereClause} {dateFilterClause} {searchFilterClause}
                    ORDER BY date_created DESC
                    LIMIT @length OFFSET @start;";

                    using (var cmd = new NpgsqlCommand(dataQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        cmd.Parameters.AddWithValue("@length", length);
                        cmd.Parameters.AddWithValue("@start", start);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                printcardItems.Add(new PrintcardItem
                                {
                                    PrintcardId = reader.GetInt32(reader.GetOrdinal("printcardid")),
                                    OrganizationName = reader.GetString(reader.GetOrdinal("organization_name")),
                                    BoxDescription = reader.IsDBNull(reader.GetOrdinal("box_description")) ? null : reader.GetString(reader.GetOrdinal("box_description")),
                                    PrintcardNo = reader.IsDBNull(reader.GetOrdinal("printcardno")) ? null : reader.GetString(reader.GetOrdinal("printcardno")),
                                    BoardSize = reader.IsDBNull(reader.GetOrdinal("boardsize")) ? null : reader.GetString(reader.GetOrdinal("boardsize")),
                                    InsideDimension = reader.IsDBNull(reader.GetOrdinal("insidedimension")) ? null : reader.GetString(reader.GetOrdinal("insidedimension")),
                                    Filename = reader.IsDBNull(reader.GetOrdinal("filename")) ? null : reader.GetString(reader.GetOrdinal("filename")),
                                    Status = reader.GetString(reader.GetOrdinal("status")),
                                    FileType = reader.IsDBNull(reader.GetOrdinal("filetype")) ? null : reader.GetString(reader.GetOrdinal("filetype")),
                                    DateCreated = reader.GetDateTime(reader.GetOrdinal("date_created"))
                                });
                            }
                        }
                    }
                }

                var response = new DataTableResponse
                {
                    Draw = draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = printcardItems
                };

                return System.Text.Json.JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                return System.Text.Json.JsonSerializer.Serialize(new { error = "Error loading data: " + ex.Message, draw = draw }); // Include draw for DataTables
            }
        }
        /// <summary>
        /// Download Printcard
        /// </summary>
        /// <param name="id">Printcard ID</param>
        /// <param name="sFileName">Filename</param>
        /// <param name="sFileExtension">File extension</param>
        public void DownloadViewFile(long id, string sFileName, string sFileExtension)
        {
            int fileId = (int)id; // Ensure id is an integer if necessary   
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    string sql = "SELECT filename_id FROM printcard WHERE id = @id;"; //id is the Printcard ID
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("File not found.", "Download Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            fileId = Convert.ToInt32(result);
                        }
                    }


                    sql = "SELECT fileloaded FROM graphicfiles WHERE id = @id;";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", fileId);
                        byte[] fileData = cmd.ExecuteScalar() as byte[];
                        string fullTempFileName = Path.Combine(Path.GetTempPath(), sFileName + "." + sFileExtension);

                        if (fileData != null && fileData.Length > 0)
                        {
                            using (FileStream fs = new FileStream(fullTempFileName, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(fileData, 0, fileData.Length);
                            }
                            Process.Start(new ProcessStartInfo(fullTempFileName) { UseShellExecute = true });

                        }
                        else
                        {
                            MessageBox.Show("No file data found for the specified ID.", "Download Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error downloading file: " + ex.Message, "File Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task OpenExportedFile(string filePath)
        {
            // IMPORTANT: Sanitize and validate the file path to prevent security vulnerabilities
            // Ensure the path is within an expected and controlled directory.
            // For simplicity, we'll assume the filePath returned by ExportPrintcardsToExcel
            // is already in a safe location. In a production app, you'd add checks.

            // Get the full, canonical path to ensure it's valid
            string fullPath = Path.GetFullPath(filePath);

            if (File.Exists(fullPath))
            {
                try
                {
                    // This will open the file using its default associated application (e.g., Excel)
                    // UseShellExecute = true is important for this to work correctly for document types.
                    Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });

                    // You could also show a message box here if you want to confirm to the user
                    // that the file was opened/downloaded. This is usually not necessary
                    // if Process.Start makes the application appear.

                }
                catch (Exception ex)
                {
                    // Log the error for debugging
                    Console.WriteLine($"Error opening exported Excel file '{fullPath}': {ex.Message}");
                    // Re-throw to inform JavaScript about the failure if needed
                    throw new Exception($"Failed to open exported Excel file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Exported Excel file not found: {fullPath}");
                throw new FileNotFoundException($"Exported Excel file not found at: {fullPath}");
            }
        }

        [ComVisible(true)]
        public string ExportPrintcardsToExcel(string year, string month)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string whereClause = " WHERE status = 'Active' ";

                    // Add date filter if provided
                    if (!string.IsNullOrEmpty(year))
                    {
                        string dateFormat = string.IsNullOrEmpty(month) ? "YYYY" : "YYYYMM";
                        string yearMonth = year + (string.IsNullOrEmpty(month) ? "" : month);
                        whereClause += $" AND to_char(date_created, '{dateFormat}') = '{yearMonth}' ";
                    }

                    string query = $@"
                SELECT 
                    printcardid as ""ID"",
                    organization_name as ""Organization"",
                    box_description as ""Description"",
                    printcardno as ""Printcard No"",
                    boardsize as ""Board Size"",
                    insidedimension as ""Inside Dimension"",
                    filename as ""Filename"",
                    status as ""Status"",
                    filetype as ""File Type"",
                    date_created as ""Date Created""
                FROM browseprintcard
                {whereClause}
                ORDER BY date_created DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Create Excel package
                        using (var package = new OfficeOpenXml.ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Printcards");

                            // Load data into worksheet
                            using (var dt = new DataTable())
                            {
                                dt.Load(reader);
                                worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                                // Auto-fit columns
                                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                            }

                            // Save to temporary file
                            string tempPath = Path.Combine(Path.GetTempPath(), $"Printcards_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                            package.SaveAs(new FileInfo(tempPath));

                            return tempPath; // Return the file path
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return System.Text.Json.JsonSerializer.Serialize(new { error = ex.Message });
            }
        }

        public RequestAttributes GetRequestAttributes(string requisitionNumber)
        {
            var attributes = new RequestAttributes();
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT request_id, customer_id, co.organization_name as customer_name, printcard_id, quantity, u.uom_code as unit, 
                               design_notes as item_remarks, rubberdie, diecut, negative_film, 
                               _internal_, _external_, assigned_to,num_outs as outs, priority,due_date,requested_date
                        FROM tooling_requests tr
                        LEFT JOIN
                            uom u ON u.id=tr.uom_id
                        LEFT JOIN
                            customer cu ON tr.customer_id = cu.id
                        LEFT JOIN
                           contact co ON cu.contact_id = co.id
                        WHERE requisition_number = @requisitionNumber;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@requisitionNumber", requisitionNumber);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                attributes.CustomerId = reader.GetInt32(reader.GetOrdinal("customer_id"));
                                attributes.CustomerName = reader.IsDBNull(reader.GetOrdinal("customer_name")) ? null : reader.GetString(reader.GetOrdinal("customer_name"));
                                attributes.PrintcardId = reader.IsDBNull(reader.GetOrdinal("printcard_id")) ? null : reader.GetInt32(reader.GetOrdinal("printcard_id"));
                                attributes.ItemQuantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                                attributes.ItemUnit = reader.GetString(reader.GetOrdinal("unit"));
                                attributes.ItemRemarks = reader.IsDBNull(reader.GetOrdinal("item_remarks")) ? null : reader.GetString(reader.GetOrdinal("item_remarks"));
                                attributes.Rubberdie = reader.GetBoolean(reader.GetOrdinal("rubberdie"));
                                attributes.DiecutMould = reader.GetBoolean(reader.GetOrdinal("diecut"));
                                attributes.NegativeFilm = reader.GetBoolean(reader.GetOrdinal("negative_film"));
                                attributes.RequestInternal = reader.GetBoolean(reader.GetOrdinal("_internal_"));
                                attributes.RequestExternal = reader.GetBoolean(reader.GetOrdinal("_external_"));
                                attributes.AssignedTo = reader.GetInt32(reader.GetOrdinal("assigned_to"));
                                attributes.NumOuts = reader.GetInt32(reader.GetOrdinal("outs"));
                                attributes.RequestPriority = reader.IsDBNull(reader.GetOrdinal("priority")) ? "Normal" : reader.GetString(reader.GetOrdinal("priority")); // Default to "Normal" if null
                                attributes.DueDate = reader.IsDBNull(reader.GetOrdinal("due_date")) ? null : reader.GetDateTime(reader.GetOrdinal("due_date")).ToString("MM-dd-yyyy"); // Format as needed  
                                attributes.RequestedDate = reader.IsDBNull(reader.GetOrdinal("requested_date")) ? null : reader.GetDateTime(reader.GetOrdinal("requested_date")).ToString("MM-dd-yyyy"); // Format as needed
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching maintenance attributes: {ex.Message}");
                // Log the error or handle it as needed
            }
            return attributes; // Ensure a value is always returned
        }

        // Renamed and slightly adjusted the existing method for clarity
        public TSDMetrics GetPrintcardSummaryMetrics(string yearMonth = "") // yearMonth is for filtering TotalPrintcards
        {
            SpecimenCycles cyclesData = GetSpecimenCycles();
            var metrics = new TSDMetrics();
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                conn.Open();

                // --- Query for Total Printcards (filtered by yearMonth if provided) ---
                string totalSql;
                if (string.IsNullOrEmpty(yearMonth))
                {
                    totalSql = @"
                        SELECT COUNT(*)
                        FROM tsdprintcardbrowser
                        WHERE status = 'Active';";
                }
                else
                {
                    totalSql = @"
                        SELECT COUNT(*)
                        FROM tsdprintcardbrowser
                        WHERE TO_CHAR(date_created::date, 'YYYYMM') = @YearMonth
                        AND status = 'Active';";
                }

                using (NpgsqlCommand cmdTotal = new(totalSql, conn))
                {
                    if (!string.IsNullOrEmpty(yearMonth))
                    {
                        cmdTotal.Parameters.AddWithValue("@YearMonth", yearMonth);
                    }
                    metrics.TotalPrintcards = Convert.ToInt32(cmdTotal.ExecuteScalar() ?? 0);
                }

                // --- Query for Added This Week ---
                string addedThisWeekSql = @"
                SELECT COUNT(*)
                FROM tsdprintcardbrowser
                WHERE date_created >= date_trunc('week', CURRENT_DATE)
                  AND status = 'Active';";

                using (NpgsqlCommand cmdAddedThisWeek = new(addedThisWeekSql, conn))
                {
                    metrics.AddedThisWeek = Convert.ToInt32(cmdAddedThisWeek.ExecuteScalar() ?? 0);
                }

                // --- Query for Added This Month ---               
                string addedThisMonthSql = @"
                SELECT COUNT(*)
                FROM tsdprintcardbrowser
                WHERE date_created >= date_trunc('month', CURRENT_DATE)
                AND date_created < (date_trunc('month', CURRENT_DATE) + INTERVAL '1 month')
                  AND status = 'Active';";

                using (NpgsqlCommand cmdAddedThisMonth = new(addedThisMonthSql, conn))
                {
                    metrics.AddedThisMonth = Convert.ToInt32(cmdAddedThisMonth.ExecuteScalar() ?? 0);
                }

                // --- Query for Total Requests ---
                string totalRequestsSql = @"
                    SELECT COUNT(*)
                    FROM public.tooling_requests
                    WHERE requested_date >= NOW() - INTERVAL '1 year';";

                using (NpgsqlCommand cmdTotal = new(totalRequestsSql, conn))
                {
                    metrics.TotalRequests = Convert.ToInt32(cmdTotal.ExecuteScalar() ?? 0);
                }

                // --- Query for Pending Requests ---
                string pendingRequestsSql = @"
                    SELECT COUNT(*)
                    FROM public.tooling_requests
                    WHERE status = 'Pending';";
                using (NpgsqlCommand cmdPending = new(pendingRequestsSql, conn))
                {
                    metrics.PendingRequests = Convert.ToInt32(cmdPending.ExecuteScalar() ?? 0);
                }

                // --- Query for High Priority Requests ---
                string highPriorityRequestsSql = @"
                    SELECT COUNT(*)
                    FROM public.tooling_requests
                    WHERE priority = 'Urgent' OR priority = 'High' AND status NOT IN ('Archived','Completed','Rejected');"; // Assuming 'Archived' is the status for removed maintenanceItems
                using (NpgsqlCommand cmdHighPriority = new(highPriorityRequestsSql, conn))
                {
                    metrics.HighPriorityRequests = Convert.ToInt32(cmdHighPriority.ExecuteScalar() ?? 0);
                }

                // --- Query for In Development Requests ---
                string inDevelopmentRequestsSql = @"
                    SELECT COUNT(*)
                    FROM public.tooling_requests
                    WHERE status = 'In Development';";
                using (NpgsqlCommand cmdDevelopment = new(inDevelopmentRequestsSql, conn))
                {
                    metrics.InDevelopmentRequests = Convert.ToInt32(cmdDevelopment.ExecuteScalar() ?? 0);
                }

                // --- Query for Completed Requests ---
                string completedRequestsSql = @"
                    SELECT COUNT(*)
                    FROM public.tooling_requests
                    WHERE status = 'Completed';";
                using (NpgsqlCommand cmdCompleted = new(completedRequestsSql, conn))
                {
                    metrics.CompletedRequests = Convert.ToInt32(cmdCompleted.ExecuteScalar() ?? 0);
                }

                string thisWeekCompletedRequestsSql = @"
                    SELECT COUNT(*)
                    FROM public.tooling_requests
                    WHERE status = 'Completed' AND completion_date >= date_trunc('week', CURRENT_DATE);";
                using (NpgsqlCommand cmdThisWeekCompleted = new(thisWeekCompletedRequestsSql, conn))
                {
                    metrics.ThisWeekCompletedRequests = Convert.ToInt32(cmdThisWeekCompleted.ExecuteScalar() ?? 0);
                }
                // -- Get current year
                int currentYear = DateTime.Now.Year;

                // --- Query for the next control number ---
                string nextControlNumberSql = @"
                    SELECT 
                            COALESCE(
                                MAX(
                                    CAST(
                                        SUBSTRING(
                                            requisition_number 
                                            FROM 'S - \d{4} / (\d+)'
                                        ) AS INTEGER
                                    )
                                ),
                                0
                            ) + 1 AS next_number
                        FROM tooling_requests
                        WHERE requisition_number LIKE 'S - ' || EXTRACT(YEAR FROM CURRENT_DATE) || ' / %';"; // Assuming control numbers start with 'S-'
                using (NpgsqlCommand cmdNextControl = new(nextControlNumberSql, conn))
                {
                    metrics.NextControlNumber = cmdNextControl.ExecuteScalar()?.ToString() ?? $@"S - {currentYear} - 001"; // Default to a format if no maintenanceItems exist
                }

                string totalActiveDiecutsSql = @"
                    SELECT COUNT(*)
                        FROM public.diecut_tools
                        WHERE status_id = 15;";
                using (NpgsqlCommand cmdTotalDiecuts = new(totalActiveDiecutsSql, conn))
                {
                    metrics.TotalActiveDiecuts = Convert.ToInt32(cmdTotalDiecuts.ExecuteScalar() ?? 0);
                }

                string totalActiveRubberdiesSql = @"
                    SELECT COUNT(*)
                        FROM public.rubberdie_plates
                        WHERE status_id = 186;";
                using (NpgsqlCommand cmdTotalRubberdies = new(totalActiveRubberdiesSql, conn))
                {
                    metrics.TotalActiveRubberdie = Convert.ToInt32(cmdTotalRubberdies.ExecuteScalar() ?? 0);
                }
                string diecutUsageLimitSql = @$"
                    SELECT 
                       count(*)
                    FROM 
                        public.diecut_tools
                    WHERE 
                        current_usage >= {cyclesData.diecut_life_cycle} * 0.8 OR 
                        date_created <= NOW() - INTERVAL '{cyclesData.diecut_date_cycle} years';";
                using (NpgsqlCommand cmdDiecutUsageLimit = new(diecutUsageLimitSql, conn))
                {
                    metrics.DiecutUsageLimit = Convert.ToInt32(cmdDiecutUsageLimit.ExecuteScalar() ?? 0);
                }

                string rubberdieUsageLimitSql = @$"
                    SELECT 
                       count(*)
                    FROM 
                        public.rubberdie_plates
                    WHERE 
                        current_usage >= {cyclesData.rubberdie_life_cycle} * 0.8 OR 
                        date_created <= NOW() - INTERVAL '{cyclesData.rubberdie_date_cycle} years';";
                using (NpgsqlCommand cmdRubberdieUsageLimit = new(rubberdieUsageLimitSql, conn))
                {
                    metrics.RubberdieUsageLimit = Convert.ToInt32(cmdRubberdieUsageLimit.ExecuteScalar() ?? 0);
                }

                string totalDiecutInMaintenanceSql = "SELECT COUNT(*) FROM die_maintenance_log WHERE diecut_id IN (SELECT id FROM diecut_tools WHERE status_id=16);";
                using (NpgsqlCommand cmdDiecutMaintenance = new(totalDiecutInMaintenanceSql, conn))
                {
                    metrics.TotalDiecutInMaintenance = Convert.ToInt32(cmdDiecutMaintenance.ExecuteScalar() ?? 0);
                }      
                
                string totalDiecutDisposedSql = $"SELECT COUNT(*) FROM diecut_tools WHERE status_id={Config.DiecutDisposeStatus};";
                using (NpgsqlCommand cmdDiecutDisposed = new(totalDiecutDisposedSql, conn))
                {
                    metrics.totalDiecutDisposed = Convert.ToInt32(cmdDiecutDisposed.ExecuteScalar() ?? 0);
                }   

                return metrics;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching printcard summary metrics: {ex.Message}");
                // In a real application, you might log this error more formally.
                return new TSDMetrics { TotalPrintcards = 0, AddedThisWeek = 0, AddedThisMonth = 0 };
            }
        }

        public async Task<List<MonthlyPrintcardCounts>> GetMonthlyPrintcardCounts(int year)
        {
            var monthlyData = new List<MonthlyPrintcardCounts>();

            // Initialize with all 12 months with a count of 0
            for (int i = 1; i <= 12; i++)
            {
                monthlyData.Add(new MonthlyPrintcardCounts { Month = i, Count = 0 });
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    // This SQL query assumes a 'browseprintcard' table with a 'date_created' column.
                    // Adjust table and column names as per your actual database schema.
                    // It counts printcards created per month for a given year.
                    string sql = @"
                        SELECT
                            EXTRACT(MONTH FROM date_created) AS month,
                            COUNT(*) AS count
                        FROM
                            tsdprintcardbrowser -- Assuming 'browseprintcard' contains printcard data and 'date_created'
                        WHERE
                            EXTRACT(YEAR FROM date_created) = @year AND status = 'Active'
                        GROUP BY
                            month
                        ORDER BY
                            month;";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("year", year);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                double month = reader.GetDouble(reader.GetOrdinal("month"));
                                double count = reader.GetDouble(reader.GetOrdinal("count"));

                                // Update the count for the corresponding month
                                var existingMonth = monthlyData.FirstOrDefault(m => m.Month == month);
                                if (existingMonth != null)
                                {
                                    existingMonth.Count = count;
                                }
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                LogErrorMessage(ex, "GetMonthlyPrintcardCounts");
                Console.WriteLine($"PostgreSQL Error in GetMonthlyPrintcardCounts: {ex.Message} - {ex.ErrorCode}");
                throw new Exception("A database error occurred while fetching monthly printcard data.", ex);                
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "GetMonthlyPrintcardCounts");
                Console.WriteLine($"An unexpected error occurred in GetMonthlyPrintcardCounts: {ex.Message}");
                throw new Exception("An unexpected error occurred while fetching monthly printcard data.", ex);
            }

            return monthlyData;
        }


        // Placeholder for GetPrintcardDataAsync with filters/pagination from previous printcard.html
        // Ensure this method remains as it's used by printcard.html
        public async Task<PaginatedPrintcardData> GetPrintcardDataAsync(
            int? year = null,
            int? month = null,
            int limit = 25,
            int offset = 0,
            string searchTerm = ""
        )
        {
            List<PrintcardData> printcards = [];
            int totalCount = 0;
            var conditions = new List<string> { "status='Active' " }; // Always filter by active status, not deleted, and has printcardno
            var parameters = new List<NpgsqlParameter>();

            if (year.HasValue)
            {
                conditions.Add("EXTRACT(YEAR FROM date_created) = @year");
                parameters.Add(new NpgsqlParameter("year", year.Value));
            }

            if (month.HasValue)
            {
                conditions.Add("EXTRACT(MONTH FROM date_created) = @month");
                parameters.Add(new NpgsqlParameter("month", month.Value));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                conditions.Add("(box_description ILIKE @searchTerm OR printcardno ILIKE @searchTerm)");
                parameters.Add(new NpgsqlParameter("searchTerm", $"%{searchTerm}%"));
            }

            string whereClause = conditions.Any() ? " WHERE " + string.Join(" AND ", conditions) : "";

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Get total count
                    string countSql = $"SELECT COUNT(*) FROM browseprintcard {whereClause};";
                    using (var cmd = new NpgsqlCommand(countSql, conn))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                        totalCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    }

                    // Get paginated data
                    string dataSql = $@"
                        SELECT 
                            printcardid, organization_name, box_description, printcardno, boardsize,
                            insidedimension, filename, status, date_created
                        FROM browseprintcard
                        {whereClause}
                        ORDER BY date_created DESC
                        LIMIT @limit OFFSET @offset;";

                    using (var cmd = new NpgsqlCommand(dataSql, conn))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                        cmd.Parameters.AddWithValue("limit", limit);
                        cmd.Parameters.AddWithValue("offset", offset);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                printcards.Add(new PrintcardData
                                {
                                    Printcardid = reader.IsDBNull(reader.GetOrdinal("printcardid")) ? null : reader["printcardid"].ToString(),
                                    OrganizationName = reader.IsDBNull(reader.GetOrdinal("organization_name")) ? null : reader["organization_name"].ToString(),
                                    BoxDescription = reader.IsDBNull(reader.GetOrdinal("box_description")) ? null : reader["box_description"].ToString(),
                                    Printcardno = reader.IsDBNull(reader.GetOrdinal("printcardno")) ? null : reader["printcardno"].ToString(),
                                    Boardsize = reader.IsDBNull(reader.GetOrdinal("boardsize")) ? null : reader["boardsize"].ToString(),
                                    Insidedimension = reader.IsDBNull(reader.GetOrdinal("insidedimension")) ? null : reader["insidedimension"].ToString(),
                                    Filename = reader.IsDBNull(reader.GetOrdinal("filename")) ? null : reader["filename"].ToString(),
                                    Status = reader.IsDBNull(reader.GetOrdinal("status")) ? "Unknown" : reader["status"].ToString(),
                                    DateCreated = reader.IsDBNull(reader.GetOrdinal("date_created")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("date_created"))
                                });
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in GetPrintcardDataAsync: {ex.Message}");
                // Log the exception details
                throw new Exception("A database error occurred while fetching printcard data.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred in GetPrintcardDataAsync: {ex.Message}");
                // Log the exception details
                throw new Exception("An unexpected error occurred while fetching printcard data.", ex);
            }

            return new PaginatedPrintcardData { Data = printcards, TotalCount = totalCount };
        }


        // Helper class for DataTables maintenance parameters
        public class DataTableRequest
        {
            public int draw { get; set; }
            public int start { get; set; }
            public int length { get; set; }

            [JsonPropertyName("orderColumn")]
            public string orderColumn { get; set; }

            [JsonPropertyName("orderDir")]
            public string orderDir { get; set; }

            [JsonPropertyName("searchValue")]
            public string searchValue { get; set; }

            // Custom filters
            public string year { get; set; }
            public string month { get; set; }
        }


        /// <summary>
        /// Fetches tooling maintenanceItems data for DataTables server-side processing.
        /// </summary>
        /// <param name="dtRequestJson">JSON string containing DataTables maintenance parameters.</param>
        /// <returns>JSON string in DataTables compatible format.</returns>
        public string GetRequestsForDataTable(string dtRequestJson)
        {
            var dtRequest = System.Text.Json.JsonSerializer.Deserialize<DataTableRequest>(dtRequestJson, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (dtRequest == null)
            {
                return System.Text.Json.JsonSerializer.Serialize(new { error = "Invalid DataTable maintenance format." });
            }

            long recordsTotal = 0;
            long recordsFiltered = 0;

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Get total records (before any filters)
                    using (var cmdTotal = new NpgsqlCommand("SELECT COUNT(*) FROM public.tooling_requests", conn))
                    {
                        recordsTotal = (long)cmdTotal.ExecuteScalar();
                    }

                    // Define the common CTE and FROM/JOIN clauses
                    string cteDefinition = @"
                    WITH grouped_requisitions AS (
                        SELECT tr.*, COUNT(*) OVER (PARTITION BY tr.requisition_number) AS requisition_count
                        FROM public.tooling_requests tr
                    ),first_pcb AS (
                        SELECT DISTINCT ON (printcardid) *
                        FROM public.tsdprintcardbrowser
                        ORDER BY printcardid, id -- adjust ORDER BY if needed for most recent or specific row
                    )";

                    string baseFromAndJoins = @"
                    FROM grouped_requisitions gr
                    LEFT JOIN public.printcard pc ON gr.printcard_id = pc.id
                    LEFT JOIN public.uom u ON gr.uom_id = u.id
                    LEFT JOIN public.systemusers su ON gr.assigned_to = su.id 
                    LEFT JOIN first_pcb pcb ON gr.printcard_id = pcb.printcardid
                    LEFT JOIN public.customer cu ON gr.customer_id = cu.id
                    LEFT JOIN public.contact co ON cu.contact_id = co.id";

                    // Define the SELECT list with aliases that match frontend DataTable 'data' properties
                    string selectColumnsWithAliases = @"
                        gr.request_id,
                        gr.requisition_number,
                        gr.status,
                        CASE
                            WHEN gr.printcard_id IS NOT NULL THEN COALESCE(pcb.printcardno, 'None')
                            ELSE 'None'
                        END AS printcardno,
                        gr.requested_date,
                        gr.due_date,
                        CASE
                            WHEN gr.assigned_to = 10 THEN 'Not Assigned'
                            ELSE su.fullname
                        END AS assigned_user,
                        CASE
                            WHEN gr.approval_date IS NULL THEN 'N/A'
                            ELSE TO_CHAR(gr.approval_date, 'YYYY-MM-DD')
                        END AS approval_date,
                        CASE
                            WHEN gr.completion_date IS NULL THEN 'N/A'
                            ELSE TO_CHAR(gr.completion_date, 'YYYY-MM-DD')
                        END AS completion_date,
                        co.organization_name AS customer, 
                        gr.diecut,
                        gr.rubberdie,
                        gr.quantity,
                        CASE
                            WHEN gr.user_id = 10 THEN 'DBImport'
                            ELSE (SELECT fullname FROM systemusers WHERE id=gr.user_id)
                        END AS createdby,
                        gr.num_outs AS outs,
                        u.uom_code,
                        COALESCE(pc.box_description, gr.item_description) AS description,
                        gr.design_notes AS remarks,
                        CASE
                            WHEN gr._internal_ = True THEN 'Internal'
                            ELSE 'External'
                        END AS origin,priority";

                    // Build the WHERE clause for filtering
                    var commonParameters = new List<NpgsqlParameter>();

                    var whereClauses = new List<string>();

                    // Global search filter
                    if (!string.IsNullOrWhiteSpace(dtRequest.searchValue))
                    {
                        string searchTerm = $"%{dtRequest.searchValue.ToLower()}%";
                        whereClauses.Add($@"(LOWER(gr.requisition_number) LIKE @search OR
                                      LOWER(gr.status) LIKE @search OR
                                      LOWER(CASE WHEN gr.printcard_id IS NOT NULL THEN COALESCE(pcb.printcardno, 'None') ELSE 'None' END) LIKE @search OR
                                      LOWER(CASE WHEN gr.assigned_to = 10 THEN 'Not Assigned' ELSE su.fullname END) LIKE @search OR
                                      LOWER(co.organization_name) LIKE @search OR
                                      LOWER(COALESCE(pc.box_description, gr.item_description)) LIKE @search OR
                                      LOWER(CASE WHEN gr.user_id = 10 THEN 'DevTester' ELSE su.fullname END) LIKE @search OR
                                      LOWER(CASE WHEN gr.user_id = 10 THEN 'DevTester' ELSE (SELECT fullname FROM systemusers WHERE id=gr.user_id) END) LIKE @search OR
                                      LOWER(gr.design_notes) LIKE @search OR
                                      LOWER(CASE WHEN gr._internal_ = True THEN 'Internal' ELSE 'External' END) LIKE @search OR
                                      LOWER(gr.priority) LIKE @search)");
                        commonParameters.Add(new NpgsqlParameter("search", searchTerm));
                    }

                    // Year filter
                    if (!string.IsNullOrWhiteSpace(dtRequest.year))
                    {
                        whereClauses.Add($"EXTRACT(YEAR FROM gr.requested_date) = @year");
                        commonParameters.Add(new NpgsqlParameter("year", int.Parse(dtRequest.year)));
                    }

                    // Month filter
                    if (!string.IsNullOrWhiteSpace(dtRequest.month))
                    {
                        whereClauses.Add($"EXTRACT(MONTH FROM gr.requested_date) = @month");
                        commonParameters.Add(new NpgsqlParameter("month", int.Parse(dtRequest.month)));
                    }

                    string filterCondition = whereClauses.Any() ? " WHERE " + string.Join(" AND ", whereClauses) : "";

                    // 2. Get filtered records count
                    string filteredCountQuery = $@"
                    {cteDefinition}
                    SELECT COUNT(*)
                    FROM (
                        SELECT {selectColumnsWithAliases}
                        {baseFromAndJoins}
                        {filterCondition}
                    ) AS filtered_data_subquery";

                    using (var cmdFiltered = new NpgsqlCommand(filteredCountQuery, conn))
                    {
                        foreach (var p in commonParameters)
                        {
                            cmdFiltered.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));
                        }
                        recordsFiltered = (long)cmdFiltered.ExecuteScalar();
                    }

                    // Build the main query for data retrieval
                    StringBuilder queryBuilder = new StringBuilder($@"
                    {cteDefinition}
                    SELECT
                        {selectColumnsWithAliases}
                    {baseFromAndJoins}
                    {filterCondition} ");

                    //Clipboard.SetText(queryBuilder.ToString());
                    Debug.WriteLine($"Query: {queryBuilder}");

                    //Add ORDER BY clause
                    string orderByColumn = dtRequest.orderColumn;
                    string orderDir = (dtRequest.orderDir?.ToLower() == "asc") ? "ASC" : "DESC";

                    string[] validColumns = {
                        "id", "requisition_number", "status", "printcardno", "requested_date", "due_date",
                        "assigned_to", "approval_date", "completion_date", "customer_name", "diecut",
                        "rubberdie", "quantity", "created_by", "outs", "uom", "description", "remarks", "origin","priority"
                    };

                    if (!Array.Exists(validColumns, col => col.Equals(orderByColumn, StringComparison.OrdinalIgnoreCase)))
                    {
                        orderByColumn = "requested_date"; // Default if invalid or not found
                    }

                    queryBuilder.Append($" ORDER BY {orderByColumn} {orderDir}");
                    queryBuilder.Append($" LIMIT @length OFFSET @start ");

                    commonParameters.Add(new NpgsqlParameter("length", dtRequest.length));
                    commonParameters.Add(new NpgsqlParameter("start", dtRequest.start));

                    string finalQuery = queryBuilder.ToString();

                    var dataList = new List<Dictionary<string, object>>();
                    using (var cmdData = new NpgsqlCommand(finalQuery, conn))
                    {

                        foreach (var p in commonParameters)
                        {
                            cmdData.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));
                        }
                        using (var reader = cmdData.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string colName = reader.GetName(i);
                                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);

                                    if (value is DateTime dtValue)
                                    {
                                        value = dtValue.ToString("yyyy-MM-dd");
                                    }

                                    row[colName] = value;
                                }
                                dataList.Add(row);
                            }
                        }
                    }

                    var response = new
                    {
                        draw = dtRequest.draw,
                        recordsTotal = recordsTotal,
                        recordsFiltered = recordsFiltered,
                        data = dataList
                    };

                    return System.Text.Json.JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRequestsForDataTable: {ex.Message}\n{ex.StackTrace}");
                LogErrorMessage(ex, "GetRequestsForDataTable"); // Assuming you have a method to log errors
                return System.Text.Json.JsonSerializer.Serialize(new { draw = dtRequest?.draw ?? 0, recordsTotal = 0, recordsFiltered = 0, data = new List<object>(), error = ex.Message });
            }
        }


        //* Below are the methods for user management and authentication *//
        /// <summary>
        /// Get the name of the requester based on SystemUsername.
        /// </summary>
        /// <param name="SystemUserID"></param>
        /// <returns></returns>
        public string GetSystemUserName(string username, int SystemUserID)
        {
            string parResult = "";
            NpgsqlDataReader dr;
            string sql;
            NpgsqlCommand cmd;
            NpgsqlConnection conn = new(_connectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sql = @"SELECT fullname FROM systemusers WHERE id=@SystemUsername;";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SystemUsername", SystemUserID);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    parResult = dr.GetValue(0)?.ToString() ?? string.Empty; // Safely handle possible null values
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return parResult;
        }
        /// <summary>
        /// Return the UserID based on the logged-in username.
        /// </summary>
        /// <param name="LoggedUsername"></param>
        /// <returns></returns>
        public int GetUserID(string LoggedUsername)
        {
            int UserID = 0;
            NpgsqlDataReader dr;
            string sql;
            NpgsqlCommand cmd;
            NpgsqlConnection conn = new(_connectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sql = @"SELECT id FROM systemusers WHERE username=@username;";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", LoggedUsername);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    UserID = dr.IsDBNull(0) ? 0 : Convert.ToInt32(dr.GetValue(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return UserID;
        }

        /// <summary>
        /// Get the password of a user based on SystemUsername.
        /// </summary>
        /// <param name="SystemUsername"></param>
        /// <returns></returns>
        public string GetUserPassword(string SystemUsername)
        {
            string parResult = "";
            NpgsqlDataReader dr;
            string sql;
            NpgsqlCommand cmd;
            NpgsqlConnection conn = new(_connectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sql = "SELECT spassword FROM systemusers WHERE trim(username)=@SystemUsername;";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SystemUsername", SystemUsername);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    parResult = dr.GetValue(0)?.ToString() ?? string.Empty; // Safely handle possible null values
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return parResult;
        }
        public string UserInputPassword(string SystemUsername, string PasswordText)
        {
            string UserEnteredPassword = "";
            NpgsqlDataReader dr;
            string sql;
            NpgsqlCommand cmd;
            NpgsqlConnection conn = new(_connectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sql = "SELECT salt FROM systemusers WHERE trim(username)=@SystemUsername;";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SystemUsername", SystemUsername);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    string UserSalt = dr.GetValue(0)?.ToString() ?? string.Empty;
                    UserEnteredPassword = getSHA1Hash(PasswordText + UserSalt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return UserEnteredPassword;
        }

        /// <summary>
        /// Update the user's password in the database and in the PostgreSQL role.
        /// </summary>
        /// <param name="SystemUserID"></param>
        /// <param name="SystemUsername"></param>
        /// <param name="RequestedNewPassword"></param>
        /// <returns></returns>
        public bool UpdateUserPassword(int SystemUserID, string SystemUsername, string RequestedNewPassword)
        {
            string pass_salt = CreateSalt(30);
            string loc_PassSha1 = getSHA1Hash(RequestedNewPassword + pass_salt);

            bool parResult = false;
            string sql;

            using var conn = new NpgsqlConnection(_connectionString);
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (NpgsqlTransaction pgTransaction = conn.BeginTransaction())
                    {
                        try
                        {
                            //Update table user's password
                            sql = "UPDATE systemusers SET spassword=@spassword,salt=@salt WHERE id=@id;";
                            using (NpgsqlCommand SQL_UPDATE_PASSWORD = new(sql, conn, pgTransaction))
                            {
                                SQL_UPDATE_PASSWORD.Parameters.AddWithValue("@id", SystemUserID);
                                SQL_UPDATE_PASSWORD.Parameters.AddWithValue("@spassword", loc_PassSha1);
                                SQL_UPDATE_PASSWORD.Parameters.AddWithValue("@salt", pass_salt);
                                SQL_UPDATE_PASSWORD.ExecuteNonQuery();
                            }
                            //Update user role password in the database
                            if (SystemUsername.Contains(".") == true)
                            {
                                sql = "ALTER ROLE \"" + SystemUsername + "\" WITH PASSWORD '" + RequestedNewPassword + "'; ";
                            }
                            else
                            {
                                sql = "ALTER ROLE " + SystemUsername + " WITH PASSWORD '" + RequestedNewPassword + "'; ";
                            }
                            using (NpgsqlCommand SQL_UPDATE_ROLE_PASSWORD = new(sql, conn, pgTransaction))
                            {
                                SQL_UPDATE_ROLE_PASSWORD.ExecuteNonQuery();
                            }

                            pgTransaction.Commit();
                            pgTransaction.Dispose();
                            conn.Close();
                            parResult = true;
                        }
                        catch (Exception)
                        {
                            pgTransaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return parResult;
        }

        //* Utilities for User Management and Authentication *//
        private string getSHA1Hash(string strToHash)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create()) // Use the Create method on the base type
            {
                byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);
                byte[] hashBytes = sha1.ComputeHash(bytesToHash);

                // Convert hash bytes to a hexadecimal string
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }
        private static string CreateSalt(int size)
        {
            // Use RandomNumberGenerator static methods to generate a random number
            byte[] buff = new byte[size];
            RandomNumberGenerator.Fill(buff); // Replaces RNGCryptoServiceProvider
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        internal UserFullname GetSystemUserName(string username)
        {
            var User = new UserFullname
            {
                Fullname = string.Empty, // Initialize required member
                UserId = 0 // Initialize optional member
            };

            NpgsqlDataReader dr;
            string sql;
            NpgsqlCommand cmd;
            NpgsqlConnection conn = new(_connectionString);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sql = @"SELECT fullname,id FROM systemusers WHERE username=@SystemUsername;";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SystemUsername", username);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    User.Fullname = dr.GetValue(0)?.ToString() ?? string.Empty; // Safely handle possible null values
                    User.UserId = dr.IsDBNull(1) ? 0 : Convert.ToInt32(dr.GetValue(1)); // Safely handle possible null values
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return User;
        }

        public event Action<string> OnMessageToWebView;
        [ComVisible(true)]
        public async Task<bool> InsertRequests(string jsonData)
        {
            string thisPdfAddress = "";
            string suffixRequest = "";
            try
            {
                var requests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RequestModel>>(jsonData);

                await using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    foreach (var request in requests)
                    {
                        if (request.Rubberdie && !suffixRequest.Contains("R")) suffixRequest += "R";
                        if (request.DiecutMould && !suffixRequest.Contains("D")) suffixRequest += "D";
                        thisPdfAddress = request.PdfUrl;

                        await using var transaction = await conn.BeginTransactionAsync();
                        try
                        {
                            await using var cmd = new NpgsqlCommand(
                                @"INSERT INTO public.tooling_requests (customer_id, printcard_id, quantity, uom_id,
                          design_notes, rubberdie, diecut, negative_film, _internal_, _external_,
                          requisition_number, due_date, requested_date, status, assigned_to, user_id, item_description,priority,num_outs) 
                          VALUES (@customerId, @printcardId, @quantity, @uom, @design_notes, @rubberdie, 
                          @diecutMould, @negativeFilm, @requestInternal, @requestExternal, @_requisitionNumber, 
                          @dueDate, @requestedDate, @status, @assigned_to, @SessionUserID, @ItemDescription, @priority, @num_outs)
                          RETURNING request_id;",
                                conn, transaction);

                            cmd.Parameters.AddWithValue("customerId", request.CustomerId);
                            cmd.Parameters.AddWithValue("printcardId", (object)request.PrintcardId ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("quantity", request.ItemQuantity);
                            cmd.Parameters.AddWithValue("uom", request.ItemUnit);
                            cmd.Parameters.AddWithValue("design_notes", request.ItemRemarks);
                            cmd.Parameters.AddWithValue("rubberdie", request.Rubberdie);
                            cmd.Parameters.AddWithValue("diecutMould", request.DiecutMould);
                            cmd.Parameters.AddWithValue("negativeFilm", request.NegativeFilm);
                            cmd.Parameters.AddWithValue("requestInternal", request.RequestInternal);
                            cmd.Parameters.AddWithValue("requestExternal", request.RequestExternal);
                            cmd.Parameters.AddWithValue("_requisitionNumber", request.RequisitionNumber + suffixRequest);
                            cmd.Parameters.AddWithValue("dueDate", DateTime.Parse(request.DueDate));
                            cmd.Parameters.AddWithValue("requestedDate", DateTime.Parse(request.RequestedDate));
                            cmd.Parameters.AddWithValue("status", request.RequestStatus);
                            cmd.Parameters.AddWithValue("assigned_to", request.AssignedTo);
                            cmd.Parameters.AddWithValue("SessionUserID", request.UserId);
                            cmd.Parameters.AddWithValue("ItemDescription", request.ItemDescription);
                            cmd.Parameters.AddWithValue("priority", request.RequestPriority);
                            cmd.Parameters.AddWithValue("num_outs", request.NumOuts);

                            long toolingRequestId = (long)await cmd.ExecuteScalarAsync();

                            if (request.DiecutMould)
                            {
                                await using var diecutCmd = new NpgsqlCommand(
                                    "INSERT INTO public.diecut_tools (type, notes, request_id) VALUES (@type::diecut_type, @diecutNotes, @toolingRequestId);",
                                    conn, transaction);
                                diecutCmd.Parameters.AddWithValue("type", request.DiecutType);
                                diecutCmd.Parameters.AddWithValue("diecutNotes", (object)request.ItemRemarks ?? DBNull.Value);
                                diecutCmd.Parameters.AddWithValue("toolingRequestId", toolingRequestId);
                                await diecutCmd.ExecuteNonQueryAsync();
                            }

                            if (request.Rubberdie)
                            {
                                await using var rubberdieCmd = new NpgsqlCommand(
                                    "INSERT INTO public.rubberdie_plates (notes, request_id, num_colors) VALUES (@rubberdieNotes, @toolingRequestId, @numColors);",
                                    conn, transaction);
                                rubberdieCmd.Parameters.AddWithValue("rubberdieNotes", (object)request.ItemRemarks ?? DBNull.Value);
                                rubberdieCmd.Parameters.AddWithValue("toolingRequestId", toolingRequestId);
                                rubberdieCmd.Parameters.AddWithValue("numColors", request.ItemColors);
                                await rubberdieCmd.ExecuteNonQueryAsync();
                            }
                            await transaction.CommitAsync();
                        }
                        catch (NpgsqlException ex)
                        {
                            await transaction.RollbackAsync();
                            LogErrorMessage(ex, "InsertRequests");
                            MessageBox.Show("An error has occurred, see log file.: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw;
                        }
                    }                    
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deserializing JSON data: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogErrorMessage(ex, "InsertRequests");
                return false;
            }
        }

        private class RequisitionItemRow
        {
            public string CustomerName { get; set; }
            public string Quantity { get; set; }
            public string UomCode { get; set; }
            public string NumOuts { get; set; }
            public string ItemDescription { get; set; }
            public string Remarks { get; set; }
            public string PrintcardNo { get; set; }
        }
        public void OpenPDFReport(string httpaddress)
        {
            try
            {
                // Validate the URL
                if (string.IsNullOrWhiteSpace(httpaddress))
                {
                    MessageBox.Show("Please provide a valid PDF URL");
                    return;
                }

                // Use the system's default handler to open the PDF
                Process.Start(new ProcessStartInfo
                {
                    FileName = httpaddress,
                    UseShellExecute = true  // This lets Windows handle the file appropriately
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open PDF: {ex.Message}");
            }
        }

        public void OpenPDFReportInForm(string httpaddress)
        {
            try
            {
                // Validate the URL
                if (string.IsNullOrWhiteSpace(httpaddress))
                {
                    MessageBox.Show("Please provide a valid PDF URL");
                    return;
                }
                ViewReport ViewReportFrm= new(httpaddress);
                ViewReportFrm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open PDF: {ex.Message}");
            }
        }

        public string GetDiecutInventory(string year, string month)
        {
            var results = new List<Dictionary<string, object>>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            string dateCondition = string.IsNullOrEmpty(month)
                ? "to_char(date_created, 'YYYY') = @year"
                : "to_char(date_created, 'YYYYMM') = @yearmonth";

            string query = $@"
            SELECT diecutid as ""diecutId"", customer as ""organizationName"", item_description as ""itemDescription"", diecut_number as ""diecutNumber"", rack_num as ""rackNumber"",
           row_num as ""rowNumber"",type as ""diecutType"", machine as ""assignedMachine"", current_usage as ""currentUsage"", notes as ""itemNotes"", status as ""itemStatus""
            FROM diecutview
            WHERE {dateCondition}";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("year", year);
            if (!string.IsNullOrEmpty(month))
                cmd.Parameters.AddWithValue("yearmonth", year + month);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(row);
            }

            // Wrap in a DataTables-compatible object if needed
            var response = new
            {
                recordsTotal = results.Count,
                recordsFiltered = results.Count,
                data = results
            };


            return System.Text.Json.JsonSerializer.Serialize(response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public class DiecutDataResponse
        {
            public int Draw { get; set; }
            public int RecordsTotal { get; set; }
            public int RecordsFiltered { get; set; }
            public List<DiecutItem> Data { get; set; }
        }
        public class DiecutItem
        {
            public int DiecutId { get; set; }
            public int MaintenanceId { get; set; } //Maintenance table
            public string MaintenanceAction { get; set; } //Maintenance table  
            public string FormattedTimeInMaintenance { get; set; } //Maintenance table
            public string EstimatedDownTime { get; set; } //Maintenance table  
            public string OrganizationName { get; set; } // Assuming this is the organization name
            public string ItemDescription { get; set; }
            public string DiecutNumber { get; set; }
            public string RackNumber { get; set; }
            public string RowNumber { get; set; }
            public string DiecutType { get; set; }
            public string AssignedMachine { get; set; }
            public int CurrentUsage { get; set; }
            public string ItemNotes { get; set; }
            public string ItemStatus { get; set; }
            public DateTime DateCreated { get; set; }
            public string DateRestored { get; set; }
            public string RequisitionNumber { get; set; } 
        }
        public string GetDiecutTable(int draw, int start, int length, string searchValue, string dateYear, string dateMonth)
        {
            List<DiecutItem> diecutItems = new List<DiecutItem>();
            int recordsTotal = 0;    // Total records in DB without any filters
            int recordsFiltered = 0; // Total records after applying date/search filters

            string baseWhereClause = " WHERE  ";
            string dateFilterClause = "";
            string searchFilterClause = "";
            // Conditionally build date filter clause
            bool hasDateFilter = !string.IsNullOrEmpty(dateYear);
            if (hasDateFilter)
            {
                string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                dateFilterClause = $" to_char(date_created, '{paramFormat}') = @yearMonth ";
            }

            // Build search filter clause if search value is provided
            bool hasSearch = !string.IsNullOrEmpty(searchValue);
            if (hasSearch)
            {
                // IMPORTANT: Adjust these columns based on what you want to be searchable
                searchFilterClause = $@"
                AND (
                    customer ILIKE @searchValue OR
                    item_description ILIKE @searchValue OR
                    diecut_number ILIKE @searchValue OR
                    rack_num ILIKE @searchValue OR
                    row_num ILIKE @searchValue OR
                    machine ILIKE @searchValue OR
                    status ILIKE @searchValue  OR
                    requisition_number ILIKE @searchValue
                )
            ";
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Get total records (without any filters)
                    string countTotalQuery = $"SELECT COUNT(*) FROM diecut_tools;";
                    using (var cmd = new NpgsqlCommand(countTotalQuery, conn))
                    {
                        recordsTotal = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. Get filtered records count (with date filter, and/or search filter if present)
                    string countFilteredQuery = $"SELECT COUNT(*) FROM diecutview {baseWhereClause} {dateFilterClause} {searchFilterClause};";
                    using (var cmd = new NpgsqlCommand(countFilteredQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        recordsFiltered = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Get the actual paged and filtered data
                    string dataQuery = $@"
                    SELECT diecutid, customer, item_description, diecut_number, rack_num, row_num, type, 
                           machine, current_usage, notes, status, date_created,requisition_number
                    FROM diecutview                    
                    {baseWhereClause} {dateFilterClause} {searchFilterClause}
                    ORDER BY date_created DESC
                    LIMIT @length OFFSET @start;";

                    using (var cmd = new NpgsqlCommand(dataQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        cmd.Parameters.AddWithValue("@length", length);
                        cmd.Parameters.AddWithValue("@start", start);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                diecutItems.Add(new DiecutItem
                                {
                                    DiecutId = reader.GetInt32(reader.GetOrdinal("diecutid")),
                                    OrganizationName = reader.GetString(reader.GetOrdinal("customer")),
                                    ItemDescription = reader.GetString(reader.GetOrdinal("item_description")),
                                    DiecutNumber = reader.GetString(reader.GetOrdinal("diecut_number")),
                                    RackNumber = reader.GetString(reader.GetOrdinal("rack_num")),
                                    RowNumber = reader.GetString(reader.GetOrdinal("row_num")),
                                    DiecutType = reader.GetString(reader.GetOrdinal("type")),
                                    AssignedMachine = reader.GetString(reader.GetOrdinal("machine")),
                                    CurrentUsage = reader.GetInt32(reader.GetOrdinal("current_usage")),
                                    ItemNotes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes")),
                                    ItemStatus = reader.GetString(reader.GetOrdinal("status")),
                                    DateCreated = reader.GetDateTime(reader.GetOrdinal("date_created")),
                                    DateRestored = reader.IsDBNull(reader.GetOrdinal("date_restored")) ? "N/A" : reader.GetDateTime(reader.GetOrdinal("date_restored")).ToString("yyyy-MM-dd"),
                                    RequisitionNumber = reader.GetString(reader.GetOrdinal("requisition_number")),
                                });
                            }
                        }
                    }
                }

                var response = new DiecutDataResponse
                {
                    Draw = draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = diecutItems
                };

                return System.Text.Json.JsonSerializer.Serialize(response,
                    new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                LogErrorMessage(ex, "GetDiecutTable"); // Log the error message
                return System.Text.Json.JsonSerializer.Serialize(new { error = "Error loading data: " + ex.Message, draw = draw }); // Include draw for DataTables
            }
        }

        public string GetDiecutMaintenanceTable(int draw, int start, int length, string searchValue, string dateYear, string dateMonth)
        {
            List<DiecutItem> diecutItems = new List<DiecutItem>();
            int recordsTotal = 0;    // Total records in DB without any filters
            int recordsFiltered = 0; // Total records after applying date/search filters

            string baseWhereClause = " WHERE  ";
            string dateFilterClause = "";
            string searchFilterClause = "";
            // Conditionally build date filter clause
            bool hasDateFilter = !string.IsNullOrEmpty(dateYear);
            if (hasDateFilter)
            {
                string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                dateFilterClause = $" to_char(date_created, '{paramFormat}') = @yearMonth ";
            }

            // Build search filter clause if search value is provided
            bool hasSearch = !string.IsNullOrEmpty(searchValue);
            if (hasSearch)
            {
                // IMPORTANT: Adjust these columns based on what you want to be searchable
                searchFilterClause = $@"
                AND (
                    customer ILIKE @searchValue OR
                    item_description ILIKE @searchValue OR
                    diecut_number ILIKE @searchValue OR                    
                    machine ILIKE @searchValue OR
                    maintenance_action ILIKE @searchValue OR                               
                )
            ";
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Get total records (without any filters)
                    string countTotalQuery = $"SELECT COUNT(*) FROM diecut_maintenance_view;";
                    using (var cmd = new NpgsqlCommand(countTotalQuery, conn))
                    {
                        recordsTotal = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. Get filtered records count (with date filter, and/or search filter if present)
                    string countFilteredQuery = $"SELECT COUNT(*) FROM diecut_maintenance_view {baseWhereClause} {dateFilterClause} {searchFilterClause};";
                    using (var cmd = new NpgsqlCommand(countFilteredQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        recordsFiltered = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Get the actual paged and filtered data
                    string dataQuery = $@"
                    SELECT maintenance_id, customer, item_description, diecut_number, type, 
                           machine, maintenance_action, time_in_maintenance, current_usage, notes, date_created, date_restored
                    FROM diecut_maintenance_view                    
                    {baseWhereClause} {dateFilterClause} {searchFilterClause}
                    ORDER BY date_created DESC
                    LIMIT @length OFFSET @start;";

                    using (var cmd = new NpgsqlCommand(dataQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        cmd.Parameters.AddWithValue("@length", length);
                        cmd.Parameters.AddWithValue("@start", start);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                diecutItems.Add(new DiecutItem
                                {
                                    MaintenanceId = reader.GetInt32(reader.GetOrdinal("maintenance_id")),
                                    OrganizationName = reader.GetString(reader.GetOrdinal("customer")),
                                    ItemDescription = reader.GetString(reader.GetOrdinal("item_description")),
                                    DiecutNumber = reader.GetString(reader.GetOrdinal("diecut_number")),
                                    DiecutType = reader.GetString(reader.GetOrdinal("type")),
                                    AssignedMachine = reader.GetString(reader.GetOrdinal("machine")),
                                    MaintenanceAction = reader.GetString(reader.GetOrdinal("maintenance_action")),
                                    EstimatedDownTime = ToHumanReadableString(reader.GetTimeSpan(reader.GetOrdinal("time_in_maintenance"))),
                                    CurrentUsage = reader.GetInt32(reader.GetOrdinal("current_usage")),
                                    ItemNotes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes")),                                    
                                    DateCreated = reader.GetDateTime(reader.GetOrdinal("date_created"))
                                });
                            }
                        }
                    }
                }   

                var response = new DiecutDataResponse
                {
                    Draw = draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = diecutItems
                };

                return System.Text.Json.JsonSerializer.Serialize(response,
                    new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                LogErrorMessage(ex, "GetDiecutMaintenanceTable"); // Log the error message
                return System.Text.Json.JsonSerializer.Serialize(new { error = "Error loading data: " + ex.Message, draw = draw }); // Include draw for DataTables
            }
        }

        public static string ToHumanReadableString(TimeSpan timeSpan)
        {
            var parts = new List<string>();

            if (timeSpan.Days > 0)
            {
                parts.Add($"{timeSpan.Days} day{(timeSpan.Days > 1 ? "s" : "")}");
            }
            if (timeSpan.Hours > 0)
            {
                parts.Add($"{timeSpan.Hours} hour{(timeSpan.Hours > 1 ? "s" : "")}");
            }
            if (timeSpan.Minutes > 0)
            {
                parts.Add($"{timeSpan.Minutes} minute{(timeSpan.Minutes > 1 ? "s" : "")}");
            }
            if (timeSpan.TotalSeconds < 60 && timeSpan.TotalSeconds > 0)
            {
                parts.Add($"{timeSpan.Seconds} second{(timeSpan.Seconds > 1 ? "s" : "")}");
            }

            if (parts.Count == 0)
            {
                return "less than a minute";
            }

            return string.Join(", ", parts);
        }

        //Ruberdie plates datatable
        public class RubberdieDataResponse
        {
            public int Draw { get; set; }
            public int RecordsTotal { get; set; }
            public int RecordsFiltered { get; set; }
            public List<RubberdieItem> Data { get; set; }
        }
        public class RubberdieItem
        {
            public int RubberdieId { get; set; }
            public string OrganizationName { get; set; } // Assuming this is the organization name
            public string ItemDescription { get; set; }
            public string RackNumber { get; set; }
            public string RowNumber { get; set; }
            public string AssignedMachine { get; set; }
            public int CurrentUsage { get; set; }
            public int NumColors { get; set; } // Assuming this is the number of colors
            public string ItemNotes { get; set; }
            public string ItemStatus { get; set; }
            public DateTime DateCreated { get; set; }
        }
        public string GetRubberdieTable(int draw, int start, int length, string searchValue, string dateYear, string dateMonth)
        {
            List<RubberdieItem> rubberdieItems = new List<RubberdieItem>();
            int recordsTotal = 0;    // Total records in DB without any filters
            int recordsFiltered = 0; // Total records after applying date/search filters

            string baseWhereClause = " WHERE  ";
            string dateFilterClause = "";
            string searchFilterClause = "";
            // Conditionally build date filter clause
            bool hasDateFilter = !string.IsNullOrEmpty(dateYear);
            if (hasDateFilter)
            {
                string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                dateFilterClause = $" to_char(date_created, '{paramFormat}') = @yearMonth ";
            }

            // Build search filter clause if search value is provided
            bool hasSearch = !string.IsNullOrEmpty(searchValue);
            if (hasSearch)
            {
                // IMPORTANT: Adjust these columns based on what you want to be searchable
                searchFilterClause = $@"
                AND (
                    customer ILIKE @searchValue OR
                    item_description ILIKE @searchValue OR                    
                    machine ILIKE @searchValue OR
                    notes ILIKE @searchValue OR
                    status ILIKE @searchValue                   
                )
            ";
            }

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();

                    // 1. Get total records (without any filters)
                    string countTotalQuery = $"SELECT COUNT(*) FROM rubberdieview;";
                    using (var cmd = new NpgsqlCommand(countTotalQuery, conn))
                    {
                        recordsTotal = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. Get filtered records count (with date filter, and/or search filter if present)
                    string countFilteredQuery = $"SELECT COUNT(*) FROM rubberdieview {baseWhereClause} {dateFilterClause} {searchFilterClause};";
                    using (var cmd = new NpgsqlCommand(countFilteredQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        recordsFiltered = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Get the actual paged and filtered data
                    string dataQuery = $@"
                    SELECT id, customer, item_description, rack_num, row_num,  
                           machine, current_usage, num_colors, notes, status, date_created
                    FROM rubberdieview                    
                    {baseWhereClause} {dateFilterClause} {searchFilterClause}
                    ORDER BY date_created DESC
                    LIMIT @length OFFSET @start;";

                    using (var cmd = new NpgsqlCommand(dataQuery, conn))
                    {
                        if (hasDateFilter)
                        {
                            string paramFormat = string.IsNullOrEmpty(dateMonth) ? "YYYY" : "YYYYMM";
                            string yearAndMonth = dateYear + (string.IsNullOrEmpty(dateMonth) ? "" : dateMonth);
                            cmd.Parameters.AddWithValue("@yearMonth", yearAndMonth);
                        }
                        if (hasSearch)
                        {
                            cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");
                        }
                        cmd.Parameters.AddWithValue("@length", length);
                        cmd.Parameters.AddWithValue("@start", start);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rubberdieItems.Add(new RubberdieItem
                                {
                                    RubberdieId = reader.GetInt32(reader.GetOrdinal("id")),
                                    OrganizationName = reader.GetString(reader.GetOrdinal("customer")),
                                    ItemDescription = reader.GetString(reader.GetOrdinal("item_description")),
                                    RackNumber = reader.GetString(reader.GetOrdinal("rack_num")),
                                    RowNumber = reader.GetString(reader.GetOrdinal("row_num")),
                                    AssignedMachine = reader.GetString(reader.GetOrdinal("machine")),
                                    CurrentUsage = reader.GetInt32(reader.GetOrdinal("current_usage")),
                                    NumColors = reader.GetInt32(reader.GetOrdinal("num_colors")),
                                    ItemNotes = reader.IsDBNull(reader.GetOrdinal("notes")) ? null : reader.GetString(reader.GetOrdinal("notes")),
                                    ItemStatus = reader.GetString(reader.GetOrdinal("status")),
                                    DateCreated = reader.GetDateTime(reader.GetOrdinal("date_created"))
                                });
                            }
                        }
                    }
                }

                var response = new RubberdieDataResponse
                {
                    Draw = draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = rubberdieItems
                };

                return System.Text.Json.JsonSerializer.Serialize(response,
                    new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                LogErrorMessage(ex, "GetRubberdieTable"); // Log the error message
                return System.Text.Json.JsonSerializer.Serialize(new { error = "Error loading data: " + ex.Message, draw = draw }); // Include draw for DataTables
            }
        }

        //TODO: Include date condition - ageing of rubberdie plates and diecut tools
        public string GetRubberdieUsageLimitItems()
        {
            SpecimenCycles cyclesData = GetSpecimenCycles();

            var results = new List<object>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@$"
                SELECT rp.id, tr.item_description, co.organization_name, rp.current_usage ,tr.requested_date
                    FROM rubberdie_plates rp
                    JOIN tooling_requests tr ON tr.request_id = rp.request_id
                    JOIN customer cu ON tr.customer_id = cu.id
                    JOIN contact co ON cu.contact_id = co.id
                WHERE current_usage >= {cyclesData.rubberdie_life_cycle} * 0.8
                      OR dc.date_created <= NOW() - INTERVAL '{cyclesData.rubberdie_date_cycle} years';", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new
                        {
                            rubber_id = reader.GetValue(0).ToString(),
                            description = reader.GetString(1),
                            customer = reader.GetString(2),
                            current_usage = reader.GetInt32(3).ToString("N0"),
                            requested_date = reader.GetDateTime(4).ToString("yyyy-MM-dd")
                        });
                    }
                }
            }

            return System.Text.Json.JsonSerializer.Serialize(results);
        }
        public string GetDiecutUsageLimitItems()
        {
            SpecimenCycles cyclesData = GetSpecimenCycles();

            var results = new List<object>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(@$"
                SELECT dc.id,tr.item_description,co.organization_name,dc.current_usage,fm.code || '-' || fm.machine_name as machine,tr.requested_date
	            FROM diecut_tools dc
	                JOIN tooling_requests tr ON tr.request_id=dc.request_id
	                JOIN customer cu ON tr.customer_id = cu.id
	                JOIN contact co ON cu.contact_id = co.id
	                JOIN flexo_machines fm ON fm.id=dc.machine_id
	            WHERE  current_usage >= {cyclesData.diecut_life_cycle} * 0.8 
                       OR dc.date_created <= NOW() - INTERVAL '{cyclesData.diecut_date_cycle} years'
                LIMIT 10;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new
                        {
                            diecut_id = reader.GetValue(0).ToString(),
                            description = reader.GetString(1),
                            customer = reader.GetString(2),
                            current_usage = reader.GetInt32(3).ToString("N0"),
                            machine = reader.GetString(4),
                            requested_date = reader.GetDateTime(5).ToString("yyyy-MM-dd")
                        });
                    }
                }
            }

            return System.Text.Json.JsonSerializer.Serialize(results);
        }

        //Delete item from tooling_requests table by request_id
        //Change the function as boolean
        public bool DeleteToolingRequest(int requestId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("DELETE FROM tooling_requests WHERE request_id = @requestId", conn))
                    {
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; 
                    }
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "DeleteToolingRequest");
                MessageBox.Show("An error occurred while deleting the request: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
        }

        internal int GetRequestRowCountByRequisitionNumber(string requisitionNumber)
        {
            int recordsCount = 0;
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "SELECT count(*) FROM tooling_requests WHERE requisition_number = @requisition_number;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@requisition_number", requisitionNumber);
                        recordsCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }                    
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "GetRequestRowCountByRequisitionNumber");
                MessageBox.Show("An error occurred while deleting the request: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            return recordsCount; 
        }
        /// <summary>
        /// Cancel request by requisition number Cancelled
        /// </summary>
        /// <param name="controlSequence"></param>
        /// <returns></returns>
        internal bool CancelRequest(string controlSequence)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE tooling_requests SET status = 'Cancelled' WHERE requisition_number = @controlSequence;";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@controlSequence", controlSequence);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Return true if at least one row was updated
                    }
                }
                catch (Exception ex)
                {
                    LogErrorMessage(ex, "CancelRequest");
                    MessageBox.Show("An error occurred while cancelling the request: " + ex.Message, Config.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Return false if an error occurred
                }
            }
        }

        internal async Task DisposeDiecutItem(int diecutId, int SessionUserID, string remarks)
        {
            try
            {
                var connection = new NpgsqlConnection(_connectionString);     
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    // Set session user_id
                    using (var command = new NpgsqlCommand($"SET SESSION my.current_user_id = {SessionUserID};", connection))
                    {                        
                        command.ExecuteNonQuery();
                    }

                    // Perform update
                    string sql = "UPDATE diecut_tools SET status_id = @status_id,  " +
                                  "date_disposed = @date_disposed, disposed_by = @disposed_by, dispose_remarks=@dispose_remarks " +
                                  "WHERE id = @diecutId;";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@status_id", Config.DiecutDisposeStatus);
                        cmd.Parameters.AddWithValue("@date_disposed", DateTime.Now);
                        cmd.Parameters.AddWithValue("@disposed_by", SessionUserID);
                        cmd.Parameters.AddWithValue("@diecutId", diecutId);
                        cmd.Parameters.AddWithValue("@dispose_remarks", remarks);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }                   
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "DisposeDiecutItem");
                Console.WriteLine($"Error updating diecut_tools: {ex.Message}");
                throw;
            }
        }

        internal async Task MaintainanceDiecutItem(int diecutId, int SessionUserID, string remarks, int RepairTypeID)
        {
            try
            {
                // Ensure RepaireTypeID is a valid integer / value
                int validRepairTypeID = RepairTypeID > 0 ? RepairTypeID : Config.DefaultRepairTypeID;   
                var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    // Set session user_id
                    using (var command = new NpgsqlCommand($"SET SESSION my.current_user_id = {SessionUserID};", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // Update diecut to maintenance status (16)
                    string sql = "UPDATE diecut_tools SET status_id = @status_id " +                                 
                                  "WHERE id = @diecutId;";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@status_id", Config.DiecutMaintenanceStatus);
                        cmd.Parameters.AddWithValue("@diecutId", diecutId);                        
                        cmd.ExecuteNonQuery();
                    }

                    // Get description from diecut_tools table
                    string diecutItemDescription = "";
                    sql = "SELECT tr.item_description FROM tooling_requests tr " +
                           "JOIN diecut_tools dt ON dt.request_id=tr.request_id " +
                           "WHERE dt.id=@diecutId;";    
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@diecutId", diecutId);
                        diecutItemDescription = cmd.ExecuteScalar() as string;
                    }   

                    // Insert into die_maintenance_log table
                    sql = "INSERT INTO die_maintenance_log (diecut_id, maintenance_date, description, user_id, notes, action_id) " +
                          "VALUES ( @diecutId, @maintenance_date, @description, @user_id, @user_notes, @RepairTypeID );";  
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@diecutId", diecutId);                        
                        cmd.Parameters.AddWithValue("@maintenance_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@description", diecutItemDescription);
                        cmd.Parameters.AddWithValue("@user_id", SessionUserID);
                        cmd.Parameters.AddWithValue("@user_notes", remarks);
                        cmd.Parameters.AddWithValue("@RepairTypeID", validRepairTypeID);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                LogErrorMessage(ex, "DisposeDiecutItem");
                Console.WriteLine($"Error updating diecut_tools: {ex.Message}");
                throw;
            }
        }

        public class SpecimenCycles
        {
            public int row_limit { get; set; }
            public int rubberdie_life_cycle { get; set; }
            public short rubberdie_date_cycle { get; set; } // Changed to short for smallint
            public int diecut_life_cycle { get; set; }
            public short diecut_date_cycle { get; set; }     // Changed to short for smallint
            public bool isvalid { get; set; } // Assuming you want to keep track of validity
            public bool enable_checks { get; set; } // Assuming you want to keep track of whether checks are enabled


            // Optional: Add a default constructor or a constructor with parameters
            public SpecimenCycles()
            {
                // Default values if no valid settings are found
                row_limit = 0; // Or a sensible default
                rubberdie_life_cycle = 0;
                rubberdie_date_cycle = 0;
                diecut_life_cycle = 0;
                diecut_date_cycle = 0;
                isvalid = true;
                enable_checks = true;
            }
        }

        public SpecimenCycles GetSpecimenCycles()
        {
            SpecimenCycles cycles = null; // Initialize to null, will be populated if data is found

            string query = "SELECT rowlimit, rubberdie_life_cycle, rubberdie_date_cycle, diecut_life_cycle, diecut_date_cycle, enable_checks " +
                           "FROM settings WHERE isvalid=true;";
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cycles = new SpecimenCycles
                                {
                                    row_limit = reader.GetInt32(reader.GetOrdinal("rowlimit")),
                                    rubberdie_life_cycle = reader.GetInt32(reader.GetOrdinal("rubberdie_life_cycle")),
                                    rubberdie_date_cycle = reader.GetInt16(reader.GetOrdinal("rubberdie_date_cycle")),
                                    diecut_life_cycle = reader.GetInt32(reader.GetOrdinal("diecut_life_cycle")),
                                    diecut_date_cycle = reader.GetInt16(reader.GetOrdinal("diecut_date_cycle")),
                                    enable_checks = reader.GetBoolean(reader.GetOrdinal("enable_checks"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Config.LogErrorMessage(ex, "GetSpecimenCycles");
                MessageBox.Show($"Error fetching specimen cycles: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cycles;
        }
    }
}
