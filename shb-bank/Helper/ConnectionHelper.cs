using System;
using MySql.Data.MySqlClient;

namespace shb_bank.Helper
{
    public class ConnectionHelper
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "test bank";
        private const string DatabaseUid = "root";
        private const string DatabasePassword = "";
        private static MySqlConnection _connection;

        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                Console.WriteLine("Create new connection...");
                _connection =
                    new MySqlConnection(
                        $"SERVER={DatabaseServer};DATABASE={DatabaseName};UID={DatabaseUid};PASSWORD={DatabasePassword}");
            }

            return _connection;
        }
    }
}