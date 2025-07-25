using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json; // For JSON serialization
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BoxSmart_ERP.Services.PostgreSQLServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BoxSmart_ERP.Services;
using Dapper;

namespace BoxSmart_ERP.Services
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)] // To allow COM visibility
    public class PostgreSQLServices
    {        
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
                            WHERE requested_date >= CURRENT_DATE - INTERVAL '7 days'
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
                MessageBox.Show($"Error fetching recent requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return System.Text.Json.JsonSerializer.Serialize(results);
        }
        public class RequestModel
        {
            public required int CustomerId { get; set; }
            public int? PrintcardId { get; set; }
            public required int ItemQuantity { get; set; }
            public required int ItemUnit { get; set; }            
            public string ItemRemarks { get; set; }
            public bool Rubberdie { get; set; }
            public bool DiecutMould { get; set; }
            public bool NegativeFilm { get; set; }
            public bool RequestInternal { get; set; }
            public bool RequestExternal { get; set; }
            public required string UserName { get; set; }
            public required string RequisitionNumber { get; set; }
            public required string DueDate { get; set; }
            public required string RequestedDate { get; set; }
            public string RequestStatus { get; set; } = "Pending"; // Default status
            public int AssignedTo { get; set; } = 10; // Default to developer equals unassigned.
            public required int UserId { get; set; } // User ID of the person making the request
            public required string ItemDescription { get; set; } // Description of the item being requested
            public required string RequestPriority { get; set; } // Priority of the request (e.g., High, Medium, Low)
            public required int NumOuts { get; set; } // Number of outs for the request
        }

        public class Printcard
        {
            public int id { get; set; }
            public string box_description { get; set; }
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
                jsonResult = JsonSerializer.Serialize(printcards, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchPrintcard: {ex.Message}");
                // In a real app, log this properly.
                jsonResult = "[]"; // Return empty array on error
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
            public int Count { get; set; }
        }

        // DTO for the overall Printcard metrics (Total, Added This Week)
        public class TSDMetrics
        {
            [JsonPropertyName("totalPrintcards")]
            public int TotalPrintcards { get; set; }
            [JsonPropertyName("addedThisWeek")]
            public int AddedThisWeek { get; set; }
            [JsonPropertyName("addedThisMonth")]
            public int AddedThisMonth { get; set; } // Optional, if you want to track this too
            [JsonPropertyName("totalRequests")]

            public int TotalRequests { get; set; } // Total number of requests
            [JsonPropertyName("pendingRequests")]
            public int PendingRequests { get; set; } // Number of pending requests

            [JsonPropertyName("highPriorityRequests")]
            public int HighPriorityRequests { get; set; } // Number of high priority requests

            [JsonPropertyName("inDevelopmentRequests")]            
            public int InDevelopmentRequests { get; set; } // Number of requests in development

            [JsonPropertyName("thisWeekCompletedRequests")]
            public int ThisWeekCompletedRequests { get; set; } // Number of requests completed this week

            [JsonPropertyName("completedRequests")]
            public int CompletedRequests { get; set; } // Number of completed requests
            [JsonPropertyName("rejectedRequests")]
            public int RejectedRequests { get; set; } // Number of rejected requests
            [JsonPropertyName("archivedRequests")]
            public int ArchivedRequests { get; set; } // Number of archived requests

            [JsonPropertyName("printcardCounts")]
            public List<MonthlyPrintcardCounts> PrintcardCounts { get; set; } = new List<MonthlyPrintcardCounts>(); // Monthly counts for Printcards
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
                jsonResult = JsonSerializer.Serialize(customers, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchCustomers: {ex.Message}");
                // In a real app, log this properly.
                jsonResult = "[]"; // Return empty array on error
            }

            return jsonResult;
        }

        public PostgreSQLServices(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            // You might want to add a check here to ensure the connection string is not empty.
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
        public string GetPrintcardsForDataTable(
            int draw,       // DataTables draw counter
            int start,      // DataTables offset (skip)
            int length,     // DataTables limit (take)
            string searchValue, // Global search value from DataTables
            string dateYear,    // Your year filter
            string dateMonth)   // Your optional month filter
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

                return JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                return JsonSerializer.Serialize(new { error = "Error loading data: " + ex.Message, draw = draw }); // Include draw for DataTables
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
                return JsonSerializer.Serialize(new { error = ex.Message });
            }
        }

        //Modify the following function to filter an empty yearMonth parameter

        /// <summary>
        /// Get printcard count based on yearMonth, if empty, returns total count of active printcards.
        /// </summary>
        /// <param name="yearMonth">Optional</param>
        /// <returns></returns>
        //public TSDMetrics GetPrintcardData(string yearMonth = "") // Changed return type
        //{
        //    var metrics = new TSDMetrics(); // Initialize the DTO
        //    try
        //    {
        //        using var conn = new NpgsqlConnection(_connectionString);
        //        conn.Open();

        //        // --- Query for Total Printcards --- //
        //        string totalSql;
        //        if (string.IsNullOrEmpty(yearMonth))
        //        {
        //            totalSql = @"
        //            SELECT COUNT(*)
        //            FROM browseprintcard
        //            WHERE status = 'Active';";
        //        }
        //        else
        //        // --- If yearMonth is provided, filter by it --- //
        //        {
        //            totalSql = @"
        //            SELECT COUNT(*)
        //            FROM browseprintcard
        //            WHERE TO_CHAR(date_created::date, 'YYYYMM') = @YearMonth
        //            AND status = 'Active';";
        //        }

        //        using (NpgsqlCommand cmdTotal = new(totalSql, conn))
        //        {
        //            if (!string.IsNullOrEmpty(yearMonth))
        //            {
        //                cmdTotal.Parameters.AddWithValue("@YearMonth", yearMonth);
        //            }
        //            metrics.TotalPrintcards = Convert.ToInt32(cmdTotal.ExecuteScalar() ?? 0);
        //        }

        //        // --- Query for Added This Week ---
        //        // PostgreSQL's date_trunc('week', CURRENT_DATE) returns the beginning of the current ISO week (Monday).
        //        // We want records created from this Monday onwards.
        //        string addedThisWeekSql = @"
        //        SELECT COUNT(*)
        //        FROM browseprintcard
        //        WHERE date_created >= date_trunc('week', CURRENT_DATE)
        //          AND status = 'Active';"; // Added AND to ensure consistency

        //        using (NpgsqlCommand cmdAddedThisWeek = new(addedThisWeekSql, conn))
        //        {
        //            metrics.AddedThisWeek = Convert.ToInt32(cmdAddedThisWeek.ExecuteScalar() ?? 0);
        //        }

        //        return metrics; // Return the populated DTO
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching printcard metrics: {ex.Message}");
        //        // Return default values in case of error
        //        return new TSDMetrics { TotalPrintcards = 0, AddedThisWeek = 0 };
        //    }
        //}


        // Renamed and slightly adjusted the existing method for clarity
        public TSDMetrics GetPrintcardSummaryMetrics(string yearMonth = "") // yearMonth is for filtering TotalPrintcards
        {
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
                    WHERE status NOT IN ('Archived','Completed','Rejected');"; // Assuming 'Archived' is the status for removed requests

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
                    WHERE priority = 'Urgent' OR priority = 'High' AND status NOT IN ('Archived','Completed','Rejected');"; // Assuming 'Archived' is the status for removed requests
                using (NpgsqlCommand cmdHighPriority = new(highPriorityRequestsSql, conn))
                {
                    metrics.HighPriorityRequests = Convert.ToInt32(cmdHighPriority.ExecuteScalar() ?? 0);
                }

                // --- Query for In Development Requests ---
                string inDevelopmentRequestsSql = @"
                    SELECT COUNT(*)
                    FROM public.tooling_requests
                    WHERE development_status = 'In Development';";  
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
                return metrics;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching printcard summary metrics: {ex.Message}");
                // In a real application, you might log this error more formally.
                return new TSDMetrics { TotalPrintcards = 0, AddedThisWeek = 0, AddedThisMonth = 0 };
            }
        }

        //public RequestsMetrics GetRequestSummaryMetrics()
        //{
        //    var metrics = new RequestsMetrics();
        //    try
        //    {
        //        using var conn = new NpgsqlConnection(_connectionString);
        //        conn.Open();
        //        // --- Query for Total Requests ---
        //        string totalRequestsSql = @"
        //            SELECT COUNT(*)
        //            FROM public.tooling_requests
        //            WHERE status != 'Archived';"; // Assuming 'Deleted' is the status for removed requests
        //        using (NpgsqlCommand cmdTotal = new(totalRequestsSql, conn))
        //        {
        //            metrics.TotalRequests = Convert.ToInt32(cmdTotal.ExecuteScalar() ?? 0);
        //        }
        //        // --- Query for Pending Requests ---
        //        string pendingRequestsSql = @"
        //            SELECT COUNT(*)
        //            FROM public.tooling_requests
        //            WHERE status = 'Pending';";
        //        using (NpgsqlCommand cmdPending = new(pendingRequestsSql, conn))
        //        {
        //            metrics.PendingRequests = Convert.ToInt32(cmdPending.ExecuteScalar() ?? 0);
        //        }
        //        // -- Query for In Development Requests ---
        //        string inDevelopmentRequests = @"
        //            SELECT COUNT(*)
        //            FROM public.tooling_requests
        //            WHERE status = 'In Development';";
        //        using (NpgsqlCommand cmdDevelopment = new(inDevelopmentRequests, conn))
        //        {
        //            metrics.InDevelopmentRequests = Convert.ToInt32(cmdDevelopment.ExecuteScalar() ?? 0);
        //        }
        //        // --- Query for Completed Requests ---
        //        string completedRequestsSql = @"
        //            SELECT COUNT(*)
        //            FROM public.tooling_requests
        //            WHERE status = 'Completed';";
        //        using (NpgsqlCommand cmdCompleted = new(completedRequestsSql, conn))
        //        {
        //            metrics.CompletedRequests = Convert.ToInt32(cmdCompleted.ExecuteScalar() ?? 0);
        //        }
        //        // --- Query for Rejected Requests ---
        //        string rejectedRequestsSql = @"
        //            SELECT COUNT(*)
        //            FROM public.tooling_requests
        //            WHERE status = 'Rejected';";
        //        using (NpgsqlCommand cmdRejected = new(rejectedRequestsSql, conn))
        //        {
        //            metrics.RejectedRequests = Convert.ToInt32(cmdRejected.ExecuteScalar() ?? 0);
        //        }

        //        // --- Query for Rejected Requests ---
        //        string archivedRequestsSql = @"
        //            SELECT COUNT(*)
        //            FROM public.tooling_requests
        //            WHERE status = 'Archived';";
        //        using (NpgsqlCommand cmdArchived = new(archivedRequestsSql, conn))
        //        {
        //            metrics.ArchivedRequests = Convert.ToInt32(cmdArchived.ExecuteScalar() ?? 0);
        //        }

        //        return metrics;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching request summary metrics: {ex.Message}");
        //        // In a real application, you might log this error more formally.
        //        return new RequestsMetrics { TotalRequests = 0, PendingRequests = 0, CompletedRequests = 0, RejectedRequests = 0 };
        //    }
        //}
        // New method to get monthly counts for Printcards for the bar chart
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
                            browseprintcard -- Assuming 'browseprintcard' contains printcard data and 'date_created'
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
                                int month = reader.GetInt32(reader.GetOrdinal("month"));
                                int count = reader.GetInt32(reader.GetOrdinal("count"));

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
                Console.WriteLine($"PostgreSQL Error in GetMonthlyPrintcardCounts: {ex.Message} - {ex.ErrorCode}");
                throw new Exception("A database error occurred while fetching monthly printcard data.", ex);
            }
            catch (Exception ex)
            {
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


        // Helper class for DataTables request parameters
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

        ///  public void GetRequestsForDataTable(int draw,       // DataTables draw counter
        //int start,      // DataTables offset (skip)
        //    int length,     // DataTables limit (take)
        //    string searchValue, // Global search value from DataTables
        //    string dateYear,    // Your year filter
        //    string dateMonth)
        //{

        /// <summary>
        /// Fetches tooling requests data for DataTables server-side processing.
        /// </summary>
        /// <param name="dtRequestJson">JSON string containing DataTables request parameters.</param>
        /// <returns>JSON string in DataTables compatible format.</returns>
        public string GetRequestsForDataTable(string dtRequestJson)
        {
            var dtRequest = JsonSerializer.Deserialize<DataTableRequest>(dtRequestJson, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (dtRequest == null)
            {
                return JsonSerializer.Serialize(new { error = "Invalid DataTable request format." });
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
                    )";

                    string baseFromAndJoins = @"
                    FROM grouped_requisitions gr
                    LEFT JOIN public.printcard pc ON gr.printcard_id = pc.id
                    LEFT JOIN public.uom u ON gr.uom_id = u.id
                    LEFT JOIN public.systemusers su ON gr.user_id = su.id AND gr.user_id != 10
                    LEFT JOIN public.tsdprintcardbrowser pcb ON gr.printcard_id = pcb.printcardid
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
                            WHEN gr.user_id = 10 THEN 'DevTester'
                            ELSE su.fullname
                        END AS createdby,
                        gr.num_outs AS outs,
                        u.uom_code,
                        COALESCE(pc.box_description, gr.item_description) AS description,
                        gr.design_notes AS remarks,
                        CASE
                            WHEN gr._internal_ = True THEN 'Internal'
                            ELSE 'External'
                        END AS origin";

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
                                      LOWER(gr.design_notes) LIKE @search OR
                                      LOWER(CASE WHEN gr._internal_ = True THEN 'Internal' ELSE 'External' END) LIKE @search)");
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
                        // Add parameters by creating new instances for this command
                        foreach (var p in commonParameters)
                        {
                            cmdFiltered.Parameters.Add(new NpgsqlParameter(p.ParameterName, p.Value));
                        }
                        recordsFiltered = (long)cmdFiltered.ExecuteScalar();
                    }

                    // 3. Build the main query for data retrieval
                    StringBuilder queryBuilder = new StringBuilder($@"
                    {cteDefinition}
                    SELECT
                        {selectColumnsWithAliases}
                    {baseFromAndJoins}
                    {filterCondition}");

                    // 4. Add ORDER BY clause
                    string orderByColumn = dtRequest.orderColumn;
                    string orderDir = (dtRequest.orderDir?.ToLower() == "asc") ? "ASC" : "DESC";

                    string[] validColumns = {
                    "id", "requisition_number", "status", "printcardno", "requested_date", "due_date",
                    "assigned_to", "approval_date", "completion_date", "customer_name", "diecut",
                    "rubberdie", "quantity", "created_by", "outs", "uom", "description", "remarks", "origin"
                };

                    if (!Array.Exists(validColumns, col => col.Equals(orderByColumn, StringComparison.OrdinalIgnoreCase)))
                    {
                        orderByColumn = "requested_date"; // Default if invalid or not found
                    }

                    queryBuilder.Append($" ORDER BY {orderByColumn} {orderDir}");

                    // 5. Add LIMIT and OFFSET for pagination parameters
                    // These are specific to the data retrieval query, so add them here
                    commonParameters.Add(new NpgsqlParameter("length", dtRequest.length));
                    commonParameters.Add(new NpgsqlParameter("start", dtRequest.start));

                    string finalQuery = queryBuilder.ToString();

                    // 6. Execute the main query and retrieve data
                    var dataList = new List<Dictionary<string, object>>();
                    using (var cmdData = new NpgsqlCommand(finalQuery, conn))
                    {
                        // Add parameters by creating new instances for this command
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

                    // 7. Prepare the DataTables response object
                    var response = new
                    {
                        draw = dtRequest.draw,
                        recordsTotal = recordsTotal,
                        recordsFiltered = recordsFiltered,
                        data = dataList
                    };

                    return JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRequestsForDataTable: {ex.Message}\n{ex.StackTrace}");
                return JsonSerializer.Serialize(new { draw = dtRequest?.draw ?? 0, recordsTotal = 0, recordsFiltered = 0, data = new List<object>(), error = ex.Message });
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
                MessageBox.Show("Error: " + ex.Message, "BoxSmart ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error: " + ex.Message, "BoxSmart ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error: " + ex.Message, "BoxSmart ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                sql = "SELECT salt FROM systemusers WHERE trim(username)=@SystemUsername;";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SystemUsername", SystemUsername);
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read()) {
                    string UserSalt = dr.GetValue(0)?.ToString() ?? string.Empty;
                    UserEnteredPassword = getSHA1Hash(PasswordText + UserSalt);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error: " + ex.Message, "BoxSmart ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
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
        public bool UpdateUserPassword(int SystemUserID, string SystemUsername, string RequestedNewPassword )
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
                    MessageBox.Show(ex.Message, "BoxSmart ERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Error: " + ex.Message, "BoxSmart ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return User;
        }

        public event Action<string> OnMessageToWebView;
        public void InsertRequests(string jsonData)
        {
            try
            {
                var requests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RequestModel>>(jsonData);

                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    foreach (var request in requests)
                    {

                        using var cmd = new
                            NpgsqlCommand("INSERT INTO public.tooling_requests (customer_id, printcard_id, quantity, uom_id," +
                            "design_notes, rubberdie, diecut, negative_film, _internal_, _external_, " +
                            "requisition_number, due_date, requested_date, status, assigned_to, user_id, item_description,priority,num_outs) " +
                            "VALUES (@customerId, @printcardId, @quantity, @uom, @design_notes, @rubberdie, " +
                            "@diecutMould, @negativeFilm, @requestInternal, @requestExternal, @requisitionNumber, " +
                            "@dueDate, @requestedDate, @status, @assigned_to, @UserId, @ItemDescription, @priority, @num_outs)", conn);
                        cmd.Parameters.AddWithValue("customerId", request.CustomerId);
                        cmd.Parameters.AddWithValue("printcardId", (object)request.PrintcardId ?? DBNull.Value); //If printcard is optional, use DBNull.Value is granted                        
                        cmd.Parameters.AddWithValue("quantity", request.ItemQuantity);
                        cmd.Parameters.AddWithValue("uom", request.ItemUnit);
                        cmd.Parameters.AddWithValue("design_notes", request.ItemRemarks);
                        cmd.Parameters.AddWithValue("rubberdie", request.Rubberdie);
                        cmd.Parameters.AddWithValue("diecutMould", request.DiecutMould);
                        cmd.Parameters.AddWithValue("negativeFilm", request.NegativeFilm);
                        cmd.Parameters.AddWithValue("requestInternal", request.RequestInternal);
                        cmd.Parameters.AddWithValue("requestExternal", request.RequestExternal);
                        cmd.Parameters.AddWithValue("requisitionNumber", request.RequisitionNumber);
                        cmd.Parameters.AddWithValue("dueDate", DateTime.Parse(request.DueDate));
                        cmd.Parameters.AddWithValue("requestedDate", DateTime.Parse(request.RequestedDate));
                        cmd.Parameters.AddWithValue("status", request.RequestStatus);
                        cmd.Parameters.AddWithValue("assigned_to", request.AssignedTo);
                        cmd.Parameters.AddWithValue("UserId", request.UserId);
                        cmd.Parameters.AddWithValue("ItemDescription", request.ItemDescription);
                        cmd.Parameters.AddWithValue("priority", request.RequestPriority);
                        cmd.Parameters.AddWithValue("num_outs", request.NumOuts == 0 ? DBNull.Value : (object)request.NumOuts); // Handle nullable NumOuts
                        cmd.ExecuteNonQuery();                        
                    }
                    OnMessageToWebView?.Invoke("InsertSuccess");
                }

            }
            catch (Exception ex)
            {
                OnMessageToWebView?.Invoke("InsertFailed");
                File.AppendAllText("error.log", DateTime.Now + " - " + ex.ToString() + Environment.NewLine);
                MessageBox.Show("Error deserializing JSON data: " + ex.Message, "BoxSmart ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                          
        }

        //public void GeneratePdfFromDatabase(string requisitionNumber, string outputPath)
        //{
        //    // Get data from database (using Dapper in this example)
        //    using (var connection = new NpgsqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        // Get main request data
        //        var requestData = connection.QuerySingleOrDefault<ToolingRequestData>(
        //            @"SELECT requisition_number as RequisitionNumber,
        //             due_date as DueDate,
        //             rubberdie as RubberDie,
        //             diecut as DieCut,
        //             negative_film as NegativeFilm
        //      FROM public.tooling_requests
        //      WHERE requisition_number = @requisitionNumber LIMIT 1",
        //            new { requisitionNumber });

        //        // Get items
        //        var items = connection.Query<ToolingRequestItem>(
        //            @"SELECT 
        //          co.organization_name as CustomerName,
        //          quantity as Quantity,
        //          u.uom_code as UomCode,
        //          item_description as ItemDescription,
        //          tr.num_outs::text as NumOuts,
        //          CASE
        //              WHEN tr.printcard_id IS NOT NULL THEN COALESCE(pcb.printcardno, 'None')
        //              ELSE 'None'
        //          END AS PrintCardNo,
        //          design_notes as DesignNotes
        //      FROM public.tooling_requests tr
        //      LEFT JOIN public.customer cu ON tr.customer_id = cu.id
        //      LEFT JOIN public.contact co ON co.id=cu.contact_id
        //      LEFT JOIN public.uom u ON u.id=tr.uom_id
        //      LEFT JOIN public.tsdprintcardbrowser pcb ON tr.printcard_id = pcb.printcardid
        //      WHERE requisition_number = @requisitionNumber",
        //            new { requisitionNumber }).ToList();

        //        // Generate PDF
        //        var generator = new ToolingRequestPdfGenerator();
        //        generator.GeneratePdf(outputPath, requestData, items);
        //    }
        //}

        //public void GenerateRequisitionPDFReport(string selectedRequisitionNumber)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(selectedRequisitionNumber))
        //        {
        //            MessageBox.Show("Please select a requisition number to generate PDF.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //        string outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{selectedRequisitionNumber.Replace("/", "-")}_RequisitionForm.pdf");
        //        //concatenate the requisition number with the timestamp to avoid overwriting
        //        outputFile = Path.Combine(Path.GetDirectoryName(outputFile), $"{Path.GetFileNameWithoutExtension(outputFile)}_{timeStamp}{Path.GetExtension(outputFile)}");

        //        GeneratePdfFromDatabase(selectedRequisitionNumber, outputFile);
        //        MessageBox.Show($"PDF generated successfully at: {outputFile}", "PDF Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outputFile) { UseShellExecute = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error");
        //    }


        //}
        private RequisitionData GetRequisitionData(string requisitionNumber)
        {
            RequisitionData requisitionData = null;
            List<RequisitionItemRow> rawItems = new List<RequisitionItemRow>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using var cmd = new NpgsqlCommand(@"
                SELECT requisition_number,
                        due_date,
                        diecut,
                        rubberdie,
                        negative_film,
                        item_description,
                        CASE
                            WHEN tr.printcard_id IS NOT NULL THEN COALESCE(pcb.printcardno, 'None')
                            ELSE 'None'
                        END AS printcardno,
                        quantity,
                        u.uom_code,
                        tr.num_outs::text || ' Outs' as num_outs,
                        co.organization_name as customer_name, -- Renamed for clarity in C# model
                        design_notes
                FROM public.tooling_requests tr
                LEFT JOIN
                    public.customer cu ON tr.customer_id = cu.id
                LEFT JOIN
                    public.contact co ON co.id=cu.contact_id
                LEFT JOIN
                    public.uom u ON u.id=tr.uom_id
                LEFT JOIN
                    public.tsdprintcardbrowser pcb ON tr.printcard_id = pcb.printcardid
                WHERE
                    requisition_number = @requisitionNumber;", conn);
                cmd.Parameters.AddWithValue("requisitionNumber", requisitionNumber);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (requisitionData == null)
                        {
                            // Initialize RequisitionData from the first row
                            requisitionData = new RequisitionData
                            {
                                ControlNumber = reader["requisition_number"].ToString(),
                                RequisitionDate = reader.GetDateTime("due_date"),
                                IsDieCutMould = reader.GetBoolean("diecut"),
                                IsRubberDie = reader.GetBoolean("rubberdie"),
                                IsNegativeFilm = reader.GetBoolean("negative_film"),
                                RequisitioningDepartment = "SALES / MARKETING DEPARTMENT", // Hardcoded from PDF
                                DocumentCode = "FM-SMPC 02-175", // Hardcoded from PDF
                                DocumentRevision = "Rev. 03/01", // Hardcoded from PDF
                                DocumentRevisionDate = new DateTime(2024, 4, 1), // Hardcoded from PDF
                                ShowNothingFollows = false // Based on your sample PDF
                            };

                            // Placeholder for signature persons if they are not in the DB query
                            requisitionData.PreparedBy = new BoxSmart_ERP.Services.RequisitionData.SignaturePerson { Name = "SALES & MARKETING STAFF", Title = "" };
                            requisitionData.ReviewedBy = new BoxSmart_ERP.Services.RequisitionData.SignaturePerson { Name = "SALES & MARKETING MANAGER/\n SALES ASSIST. MANAGER", Title = "" };
                            requisitionData.RecommendingApproval = new BoxSmart_ERP.Services.RequisitionData.SignaturePerson { Name = "", Title = "" };
                            requisitionData.ReviewedByProcurement = new BoxSmart_ERP.Services.RequisitionData.SignaturePerson { Name = "SR. PROCUREMENT & ADMIN MANAGER", Title = "" };
                            requisitionData.ReceivedBy = new BoxSmart_ERP.Services.RequisitionData.SignaturePerson { Name = "", Title = "" };
                            requisitionData.AssistantGeneralManager = new BoxSmart_ERP.Services.RequisitionData.SignaturePerson { Name = "ASSISTANT GENERAL MANAGER", Title = "" };
                        }

                        // Collect all item rows
                        rawItems.Add(new RequisitionItemRow
                        {
                            CustomerName = reader["customer_name"].ToString(),
                            Quantity = reader["quantity"].ToString(),
                            UomCode = reader["uom_code"].ToString(),
                            NumOuts = reader["num_outs"] == DBNull.Value ? "" : reader["num_outs"].ToString(),
                            ItemDescription = reader["item_description"].ToString(),
                            Remarks = reader["design_notes"] == DBNull.Value ? "" : reader["design_notes"].ToString(),
                            PrintcardNo = reader["printcardno"].ToString()
                        });
                    }
                }
            }

            if (requisitionData != null)
            {
                // Group items by customer and populate the Customers list
                //using static BoxSmart_ERP.Services.RequisitionData;
                var groupedByCustomer = rawItems.GroupBy(r => r.CustomerName);
                foreach (var group in groupedByCustomer)
                {
                    var customer = new BoxSmart_ERP.Services.RequisitionData.Customer { Name = group.Key };
                    foreach (var rawItem in group)
                    {
                        customer.Items.Add(new BoxSmart_ERP.Services.RequisitionData.RequisitionItem
                        {
                            Quantity = rawItem.Quantity,
                            Unit = rawItem.UomCode,
                            NumOuts = rawItem.NumOuts,
                            Description = rawItem.ItemDescription,
                            Remarks = rawItem.Remarks,
                            PrintcardNo = rawItem.PrintcardNo
                        });
                    }
                    requisitionData.Customers.Add(customer);
                }
            }

            return requisitionData;
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
  
        public void GenerateRequisitionPDFReport(string selectedRequisitionNumber)
        {
            // 1. Get the selected requisition number from your DataGrid
            //    This assumes your DataGrid has a column or property holding the requisition number            
            if (string.IsNullOrEmpty(selectedRequisitionNumber))
            {
                MessageBox.Show("Please select a requisition number to generate PDF.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Fetch the data
            RequisitionData data = GetRequisitionData(selectedRequisitionNumber);

            if (data != null)
            {
                //  Instantiate the generator
                RequisitionDocumentGenerator generator = new();

                // 4. Create the MigraDoc document
                MigraDoc.DocumentObjectModel.Document document = generator.CreateDocument(data);

                // 5. Save the PDF to a desired location
                // get time of generation to avoid overwriting
                string timeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{selectedRequisitionNumber.Replace("/", "-")}_RequisitionForm.pdf");
                //concatenate the requisition number with the timestamp to avoid overwriting
                filePath = Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}_{timeStamp}{Path.GetExtension(filePath)}");

                generator.SavePdf(document, filePath);

                MessageBox.Show($"PDF generated successfully at: {filePath}", "PDF Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optional: Open the PDF after generation
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show($"No data found for requisition number: {selectedRequisitionNumber}", "Data Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
