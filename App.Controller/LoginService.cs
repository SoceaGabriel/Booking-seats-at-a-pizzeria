using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.DataModels;
using Npgsql;

namespace App.Controller
{
    public class LoginService
    {
        private DBConnection _dbConnection;
        public LoginService()
        {
            _dbConnection = new DBConnection();
        }

        public void Insert(Login log)
        {
            string query = "INSERT INTO Login(USER_LOGIN, PASSWORD_LOGIN) VALUES (@p0, @p1)";

            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                npgSqlCommand.Parameters.AddWithValue("p0", log.USER_LOGIN);
                npgSqlCommand.Parameters.AddWithValue("p1", log.PASSWORD_LOGIN);
                npgSqlCommand.ExecuteNonQuery();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Login> GetAll()
        {
            string query = "SELECT ID_LOGIN, USER_LOGIN, PASSWORD_LOGIN FROM Login";

            List<Login> loginList = new List<Login>();
            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                NpgsqlDataReader dataReader = npgSqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    loginList.Add(new Login
                    {
                        ID_LOGIN = Convert.ToInt32(dataReader["ID_LOGIN"]),
                        USER_LOGIN = Convert.ToString(dataReader["USER_LOGIN"]),
                        PASSWORD_LOGIN = Convert.ToString(dataReader["PASSWORD_LOGIN"]),
                    });
                }

                dataReader.Close();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return loginList;
        }

        public void Update(Login log)
        {
            string query = "UPDATE LOGIN SET USER_LOGIN = @p1, PASSWORD_LOGIN = @p2 WHERE ID_LOGIN = @p0";

            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                npgSqlCommand.Parameters.AddWithValue("p0", log.ID_LOGIN);
                npgSqlCommand.Parameters.AddWithValue("p1", log.USER_LOGIN);
                npgSqlCommand.Parameters.AddWithValue("p2", log.PASSWORD_LOGIN);
                npgSqlCommand.ExecuteNonQuery();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM LOGIN WHERE ID_LOGIN = @p0";

            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                npgSqlCommand.Parameters.AddWithValue("p0", id);
                npgSqlCommand.ExecuteNonQuery();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
