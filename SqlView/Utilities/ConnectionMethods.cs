using Microsoft.Data.SqlClient;
using SqlView.Types;
using SqlView.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlView.Utilities
{
    public static class ConnectionMethods
    {
        public static string GetConnectionString(string server, string database, string userId, string password)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                UserID = userId,
                Password = password,
                PersistSecurityInfo = false,
                Pooling = false,
                MultipleActiveResultSets = false,
                Encrypt = false,
                TrustServerCertificate = true,
                ApplicationName = "Czemi-SqlView",
                ConnectTimeout = 120
            };

            return builder.ConnectionString;
        }

        public static string GetConnectionString(ConnectionConfiguration config)
        {
            return GetConnectionString(config.DataSource,
                config.Database, config.UserID, config.Password);
        }
    }
}
