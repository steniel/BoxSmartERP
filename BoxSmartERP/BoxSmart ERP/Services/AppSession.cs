using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxSmart_ERP.Services
{
    internal class AppSession
    {
        public static string CurrentUsername { get; private set; }
        public static string CurrentFullName { get; private set; }
        public static int CurrentUserId { get; private set; }
        public static bool IsAuthenticated => !string.IsNullOrEmpty(CurrentUsername);
        public static List<string> CurrentUserPermissions { get; set; } = new List<string>();

        public static bool HasPermission(string permissionName)
        {
            return CurrentUserPermissions.Contains(permissionName);
        }

        public static void StartSession(string username, string fullName, int userId)
        {
            CurrentUsername = username;
            CurrentFullName = fullName;
            CurrentUserId = userId;
        }

        public static void EndSession()
        {
            CurrentUsername = null;
            CurrentFullName = null;
            CurrentUserId = 0;
            CurrentUserPermissions.Clear();
        }
    }
}
