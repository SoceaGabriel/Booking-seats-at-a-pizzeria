using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using App.DataModels;

namespace App.Controller
{
    public class ClientiService
    {
        private DBConnection _dbConnection;

        public ClientiService()
        {
            _dbConnection = new DBConnection();
        }

        public void Insert(Clienti clienti)
        {
            string query = "INSERT INTO Clienti(NUME, PRENUME, NR_VIZITE) VALUES (@p0, @p1, @p2)";

            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                npgSqlCommand.Parameters.AddWithValue("p0", clienti.NUME);
                npgSqlCommand.Parameters.AddWithValue("p1", clienti.PRENUME);
                npgSqlCommand.Parameters.AddWithValue("p2", clienti.NR_VIZITE);
                npgSqlCommand.ExecuteNonQuery();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Clienti> GetAll()
        {
            string query = "SELECT ID_CLIENT, NUME, PRENUME, NR_VIZITE FROM CLIENTI";

            List<Clienti> clientiList = new List<Clienti>();
            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                NpgsqlDataReader dataReader = npgSqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    clientiList.Add(new Clienti
                    {
                        ID_CLIENT = Convert.ToInt32(dataReader["ID_CLIENT"]),
                        NUME = Convert.ToString(dataReader["NUME"]),
                        PRENUME = Convert.ToString(dataReader["PRENUME"]),
                        NR_VIZITE = Convert.ToInt32(dataReader["NR_VIZITE"]),

                    });
                }

                dataReader.Close();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return clientiList;
        }

        public void Update(Clienti clienti)
        {
            string query = "UPDATE CLIENTI SET NUME = @p1, PRENUME = @p2, NR_VIZITE = @p3 WHERE ID_CLIENT = @p0";

            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                npgSqlCommand.Parameters.AddWithValue("p0", clienti.ID_CLIENT);
                npgSqlCommand.Parameters.AddWithValue("p1", clienti.NUME);
                npgSqlCommand.Parameters.AddWithValue("p2", clienti.PRENUME);
                npgSqlCommand.Parameters.AddWithValue("p3", clienti.NR_VIZITE);
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
            string query = "DELETE FROM CLIENTI WHERE ID_CLIENT = @p0";

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
