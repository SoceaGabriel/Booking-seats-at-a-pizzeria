using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.DataModels;
using Npgsql;

namespace App.Controller
{
    public class MeseService
    {
        private DBConnection _dbConnection;
        public MeseService()
        {
            _dbConnection = new DBConnection();
        }

        public void Insert(Mese mese)
        {
            string query = "INSERT INTO Mese(NR_LOCURI, DISPONIBILITATE) VALUES (@p0, @p1)";

            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                npgSqlCommand.Parameters.AddWithValue("p0", mese.NR_LOCURI);
                npgSqlCommand.Parameters.AddWithValue("p1", mese.DISPONIBILITATE);
                npgSqlCommand.ExecuteNonQuery();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Mese> GetAll()
        {
            string query = "SELECT ID_MASA, NR_LOCURI, DISPONIBILITATE FROM Mese";

            List<Mese> meseList = new List<Mese>();
            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                NpgsqlDataReader dataReader = npgSqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    meseList.Add(new Mese
                    {
                        ID_MASA = Convert.ToInt32(dataReader["ID_MASA"]),
                        NR_LOCURI = Convert.ToInt32(dataReader["NR_LOCURI"]),
                        DISPONIBILITATE = Convert.ToString(dataReader["DISPONIBILITATE"]),
                    });
                }

                dataReader.Close();

                _dbConnection.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return meseList;
        }

        public void Update(Mese mese)
        {
            string query = "UPDATE MESE SET NR_LOCURI = @p1, DISPONIBILITATE = @p2 WHERE ID_MASA = @p0";

            try
            {
                _dbConnection.OpenConnection();

                NpgsqlCommand npgSqlCommand = new NpgsqlCommand(query, _dbConnection.NpgsqlConnection);
                npgSqlCommand.Parameters.AddWithValue("p0", mese.ID_MASA);
                npgSqlCommand.Parameters.AddWithValue("p1", mese.NR_LOCURI);
                npgSqlCommand.Parameters.AddWithValue("p2", mese.DISPONIBILITATE);
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
            string query = "DELETE FROM MESE WHERE ID_MASA = @p0";

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
