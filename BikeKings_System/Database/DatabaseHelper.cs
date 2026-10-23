using System;
using Microsoft.Data.SqlClient;

namespace BikeKings_System.Database
{
    public static class DatabaseHelper
    {
        private static string? _activeServer;

        // Automatically detects whether this computer uses ., .\SQLEXPRESS, or (localdb)\MSSQLLocalDB
        public static string GetActiveServer()
        {
            if (_activeServer != null) return _activeServer;

            string[] candidateServers = new[]
            {
                ".",
                @".\SQLEXPRESS",
                @"(localdb)\MSSQLLocalDB",
                "localhost"
            };

            foreach (var server in candidateServers)
            {
                try
                {
                    string testConn = $"Server={server};Database=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
                    using var conn = new SqlConnection(testConn);
                    conn.Open();
                    _activeServer = server;
                    return _activeServer;
                }
                catch
                {
                    // Try next candidate
                }
            }

            _activeServer = "."; // Default fallback
            return _activeServer;
        }

        public static SqlConnection GetConnection()
        {
            string server = GetActiveServer();
            string connStr = $"Server={server};Database=BikeKingsDB;Integrated Security=True;TrustServerCertificate=True;";
            return new SqlConnection(connStr);
        }

        public static string GetMasterConnectionString()
        {
            string server = GetActiveServer();
            return $"Server={server};Database=master;Integrated Security=True;TrustServerCertificate=True;";
        }
    }
}