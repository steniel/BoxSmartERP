using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxSmart_ERP.Services
{
    internal class Settings
    {
        public string ApplicationTitle = "BoxSmart ERP";
        private NpgsqlConnection conn; // Initialize this somewhere, e.g., in the constructor
        public Settings(string connectionString)
        {
            conn = new NpgsqlConnection(connectionString);
        }

        // A more modern and robust approach using 'using' statements for proper resource disposal  
        // and async/await for better performance in UI applications.  
        public async Task<int> AGetSQLRowLimit(int ActiveRow)
        {
            int iRes = 0;
            string sql = "SELECT rowlimit FROM settings WHERE id = @ActiveRow;";

            try
            {
                // 'using' statement ensures the connection is closed and disposed even if an error occurs.  
                using (var conn = new NpgsqlConnection((this.conn.ConnectionString))) // Re-create connection for using block  
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", ActiveRow);

                        // CommandBehavior.CloseConnection is implicitly handled by the 'using' block for the reader.  
                        using (var dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                iRes = dr.GetInt32(0);
                            }
                        }
                    }
                }
                Debug.WriteLine($"Executing SQL: {sql} with CustomerID: {ActiveRow}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return iRes;
        }

        public int GetSQLRowLimit(int ActiveRow)
        {
            int iRes = 0;
            string sql = "";

            NpgsqlCommand cmd = null;
            NpgsqlDataReader dr = null;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                sql = "SELECT rowlimit FROM settings WHERE id = @ActiveRow;";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ActiveRow", ActiveRow);

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    iRes = dr.GetInt32(0);
                }
                Debug.Print(sql);
                Debug.WriteLine($"Executing SQL: {cmd.CommandText} with CustomerID: {ActiveRow}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return iRes;
        }
    }
}
