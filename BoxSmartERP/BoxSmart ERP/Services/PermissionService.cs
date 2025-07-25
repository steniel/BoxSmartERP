using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxSmart_ERP.Services
{
    internal class PermissionService
    {
        private readonly string _connectionString;

        public PermissionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {
            var permissions = new List<string>();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var query = @"
            SELECT DISTINCT p.permission_name
            FROM systemusers u
            JOIN role_permissions rp ON u.role_id = rp.role_id
            JOIN permissions p ON rp.permission_id = p.id
            WHERE u.id = @userId AND u.activeuser = true
            UNION
            SELECT p.permission_name
            FROM user_permissions up
            JOIN permissions p ON up.permission_id = p.id
            WHERE up.user_id = @userId";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("userId", userId);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                permissions.Add(reader.GetString(0));
            }

            if (permissions.Count == 0)
            {
                Console.WriteLine($"No permissions found for user ID {userId}");
            }

            return permissions;
        }

        public async Task<bool> HasPermissionAsync(int userId, string permissionName)
        {
            var permissions = await GetUserPermissionsAsync(userId);
            return permissions.Contains(permissionName);
        }

        // New method to add a permission to a specific user
        public async Task AddUserPermissionAsync(int userId, string permissionName)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var query = @"
            INSERT INTO user_permissions (user_id, permission_id)
            SELECT @userId, id FROM permissions WHERE permission_name = @permissionName
            ON CONFLICT DO NOTHING";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("permissionName", permissionName);
            await cmd.ExecuteNonQueryAsync();
        }

        // New method to remove a permission from a specific user
        public async Task RemoveUserPermissionAsync(int userId, string permissionName)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            var query = @"
            DELETE FROM user_permissions
            WHERE user_id = @userId
            AND permission_id = (SELECT id FROM permissions WHERE permission_name = @permissionName)";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("permissionName", permissionName);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
