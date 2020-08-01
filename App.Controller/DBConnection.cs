using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace App.Controller
{
    public class DBConnection
    {
        private string _server = "localhost";
        private string _userName = "postgres";
        private string _password = "admin";
        private string _database = "bd_project";
        private string _port = "5432";

        private NpgsqlConnection _npgSqlConnnection;

        public DBConnection()
        {
            string connectionString = $"Username={_userName};Password={_password};Host={_server};Port={_port};Database={_database};";

            _npgSqlConnnection = new NpgsqlConnection
            {
                ConnectionString = connectionString
            };
        }

        public NpgsqlConnection NpgsqlConnection
        {
            get { return _npgSqlConnnection; }
        }

        public void OpenConnection()
        {
            if (_npgSqlConnnection.State == System.Data.ConnectionState.Closed)
            {
                _npgSqlConnnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_npgSqlConnnection.State == System.Data.ConnectionState.Open)
            {
                _npgSqlConnnection.Close();
            }
        }
    }
}
