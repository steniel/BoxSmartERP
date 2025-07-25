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
        public static string DecryptPassword(string encryptedBase64)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
            byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.LocalMachine);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
