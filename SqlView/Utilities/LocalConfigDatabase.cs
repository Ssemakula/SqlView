using Microsoft.Data.Sqlite;
using SqlView.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlView.Utilities
{
    /// <summary>
    /// Sets and reads the local configuration database for storing connection information and user defaults. 
    /// Uses SQLite for simplicity and local storage.
    /// </summary>
    public static class LocalConfigDatabase
    {
        private static readonly string AppDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Czemi");

        private static readonly string DatabasePath = Path.Combine(AppDataPath, "CzemiSqlView.db");

        public static string ConnectionString => $"Data Source={DatabasePath}";

        /// <summary>
        /// Ensures the database exists and is properly initialized
        /// </summary>
        public static void Initialize()
        {
            // Create directory if it doesn't exist
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }

            bool isNewDatabase = !File.Exists(DatabasePath);

            // Create database and schema if needed
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var createTableCommand = connection.CreateCommand();
            createTableCommand.CommandText = @"
                CREATE TABLE IF NOT EXISTS Connections (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ConnectionName TEXT NOT NULL UNIQUE,
                    DataSource TEXT NOT NULL,
                    Database TEXT NOT NULL,
                    UserID TEXT NOT NULL,
                    Password TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS UserDefault (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ItemName TEXT NOT NULL UNIQUE,
                    ItemValue TEXT NULL
                );";
            createTableCommand.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// Adds a new connection configuration
        /// </summary>
        public static int AddConnection(ConnectionConfiguration config)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Connections (ConnectionName, DataSource, Database, UserID, Password)
                VALUES (@name, @dataSource, @database, @userId, @password);
                SELECT last_insert_rowid();";

            command.Parameters.AddWithValue("@name", config.ConnectionName);
            command.Parameters.AddWithValue("@dataSource", config.DataSource);
            command.Parameters.AddWithValue("@database", config.Database);
            // Encrypt credentials
            command.Parameters.AddWithValue("@userId", PasswordEncryption.EncryptPassword(config.UserID));
            command.Parameters.AddWithValue("@password", PasswordEncryption.EncryptPassword(config.Password));

            return Convert.ToInt32(command.ExecuteScalar());
        }

        /// <summary>
        /// Updates an existing connection configuration
        /// </summary>
        public static void UpdateConnection(ConnectionConfiguration config)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Connections 
                SET ConnectionName = @name, 
                    DataSource = @dataSource, 
                    Database = @database, 
                    UserID = @userId, 
                    Password = @password
                WHERE ID = @id";

            command.Parameters.AddWithValue("@id", config.ID);
            command.Parameters.AddWithValue("@name", config.ConnectionName);
            command.Parameters.AddWithValue("@dataSource", config.DataSource);
            command.Parameters.AddWithValue("@database", config.Database);
            // Encrypt credentials before storing
            command.Parameters.AddWithValue("@userId", PasswordEncryption.EncryptPassword(config.UserID));
            command.Parameters.AddWithValue("@password", PasswordEncryption.EncryptPassword(config.Password));

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes a connection configuration
        /// </summary>
        public static void DeleteConnection(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Connections WHERE ID = @id";
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Tests if a connection configuration works
        /// </summary>
        public static bool TestConnection(ConnectionConfiguration config, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var connectionString = ConnectionMethods.GetConnectionString(
                    config.DataSource,
                    config.Database,
                    config.UserID,
                    config.Password);

                using var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Checks if any connections exist
        /// </summary>
        public static bool HasConnections()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM Connections";

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        /// <summary>
        /// Resets password for a connection (requires manual re-entry, no export/import)
        /// </summary>
        public static void ResetConnectionCreds(int connectionId, string newUserId, string newPassword)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Connections SET Password = @password, UserID = @userID WHERE ID = @id";

            command.Parameters.AddWithValue("@id", connectionId);
            command.Parameters.AddWithValue("@userID", PasswordEncryption.EncryptPassword(newUserId));
            command.Parameters.AddWithValue("@password", PasswordEncryption.EncryptPassword(newPassword));

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Maps a SqliteDataReader to a ConnectionConfiguration object
        /// </summary>
        private static ConnectionConfiguration MapReadConnectionFromReader(SqliteDataReader reader)
        {
            var config = new ConnectionConfiguration
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                ConnectionName = reader.GetString(reader.GetOrdinal("ConnectionName")),
                DataSource = reader.GetString(reader.GetOrdinal("DataSource")),
                Database = reader.GetString(reader.GetOrdinal("Database")),
                UserID = reader.GetString(reader.GetOrdinal("UserID")),
                Password = reader.GetString(reader.GetOrdinal("Password"))
            };

            // Decrypt credentials
            try
            {
                config.UserID = PasswordEncryption.DecryptPassword(config.UserID);
                config.Password = PasswordEncryption.DecryptPassword(config.Password);
            }
            catch
            {
                // Mark as needing reset if decryption fails
                config.UserID = string.Empty;
                config.Password = string.Empty;
            }

            return config;
        }

        private static UserDefault MapDefaultFromReader(SqliteDataReader reader)
        {
            var defaultSet = new UserDefault
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                ItemValue = reader.GetString(reader.GetOrdinal("ItemValue")),
            };

            return defaultSet;
        }

        /// <summary>
        /// Safely gets connections, marking those with decryption failures
        /// </summary>
        public static List<ConnectionConfiguration> GetAllConnections()
        {
            var connections = new List<ConnectionConfiguration>();

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT ID, ConnectionName, DataSource, Database, UserID, Password FROM Connections -- ORDER BY ConnectionName";

            try
            {
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    connections.Add(MapReadConnectionFromReader(reader));
                }
            }
            catch (Exception)
            {
                throw new Exception("Unable to read settings database");
            }

            return connections;
        }

        public static List<UserDefault> GetAllDefaults()
        {
            var userDefaults = new List<UserDefault>();

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT ID, ItemName, ItemValue FROM UserDefaults ORDER BY ItemName";

            try
            {
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userDefaults.Add(MapDefaultFromReader(reader));
                }
            }
            catch (Exception)
            {
                throw new Exception("Unable to read settings database");
            }

            return userDefaults;
        }

        /// <summary>
        /// Gets a connection by ID
        /// </summary>
        public static ConnectionConfiguration? GetConnectionById(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT ID, ConnectionName, DataSource, Database, UserID, Password FROM Connections WHERE ID = @id";
            command.Parameters.AddWithValue("@id", id);

            try
            {
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapReadConnectionFromReader(reader);
                }
            }
            catch (Exception)
            {
                throw new Exception("Unable to read settings database");
            }

            return null;
        }
    }
}
