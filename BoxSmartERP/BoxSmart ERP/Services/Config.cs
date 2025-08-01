using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BoxSmart_ERP.Services
{
    internal class Config
    {
        public static string SQLStringConnection;
        public static string PostgreSQLConnection;      
        public static string PostgreSQLUsername;
        public static SecureString PostgreSQLPassword; // Store encrypted password
        public static string ApplicationName = "BoxSmart";  
        
        public static int DiecutDevelopment = 14;
        public static int DiecutActiveStatus = 15;
        public static int DiecutMaintenanceStatus = 16;
        public static int DiecutArchivedStatus = 17;
        public static int DiecutDisposeStatus = 18;
        public static int DiecutOnHoldStatus = 19;
        public static int DiecutCancelledStatus = 20;
        public static int DiecutPendingStatus = 21;
        public static int DefaultRepairTypeID = 1; // Default Repair Type ID for new diecuts
        public static string DecryptPassword(string encryptedBase64)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
            byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.LocalMachine);
            return Encoding.UTF8.GetString(decryptedBytes);
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
    }
}
