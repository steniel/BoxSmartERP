using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static BoxSmart_ERP.Services.PostgreSQLServices;

namespace BoxSmart_ERP.Services
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)] // To allow COM visibility
    public class Maintenance
    {
        private readonly string _connectionString;
        public Maintenance(string connectionString) {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        /* 
         * INSERT INTO public.die_maintenance_log(
	        id, diecut_id, maintenance_date, description, user_id, cost, estimated_downtime, notes, date_created, action_id)
	        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?);
         */
        public bool LogMaintenance(int diecutId, DateTime maintenanceDate, string description, int userId, decimal cost, TimeSpan estimatedDowntime, string notes, DateTime dateCreated, int actionId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(Config.PostgreSQLConnection))
                {
                    conn.Open();
                    string sql = @"
                            INSERT INTO public.die_maintenance_log(diecut_id, maintenance_date, description, user_id, cost, estimated_downtime, notes, date_created, action_id)
                            VALUES(@diecut_id, @maintenance_date, @description, @user_id, @cost, @estimated_downtime, @notes, @date_created, @action_id)";                            
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@diecut_id", diecutId);

                        cmd.ExecuteNonQuery();
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
    }
}
