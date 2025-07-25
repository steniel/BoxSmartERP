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
                                    WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 86400) / 3600)::int > 1 
                                    THEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 86400) / 3600)::int || ' hours'
                                END,
                                CASE 
                                    WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 86400) / 3600)::int = 1 
                                    THEN '1 hour'
                                END,
                                CASE 
                                    WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 3600) / 60)::int > 1 
                                    THEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 3600) / 60)::int || ' minutes ago'
                                END,
                                CASE 
                                    WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 3600) / 60)::int = 1 
                                    THEN '1 minute ago'
                                END,
                                CASE 
                                    WHEN FLOOR(MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 60))::int < 60 AND 
                                         FLOOR(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)) / 60)::int = 0
                                    THEN MOD(EXTRACT(EPOCH FROM (now() - action_tstamp_tx)), 60)::int || ' seconds ago'
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