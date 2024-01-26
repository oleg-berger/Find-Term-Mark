using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=HOME-PC;Initial Catalog=userMark;Integrated Security=True;");

        public void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void LoseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }


        public void InsertMark(string userName, string forUnit, string maxForUnit, string forFA, string forTerm, string maxForTerm, int totalMark)
        {
            try
            {
                OpenConnection();

                
                string insertMarkQuery = "INSERT INTO users_and_mark (userName, forUnit, maxForUnit, forFA, forTerm, maxForTerm, totalMark) VALUES (@userName, @forUnit, @maxForUnit, @forFA, @forTerm, @maxForTerm, @totalMark)";
                using (SqlCommand command = new SqlCommand(insertMarkQuery, sqlConnection))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@forUnit", forUnit);
                    command.Parameters.AddWithValue("@maxForUnit", maxForUnit);
                    command.Parameters.AddWithValue("@forFA", forFA);
                    command.Parameters.AddWithValue("@forTerm", forTerm);
                    command.Parameters.AddWithValue("@maxForTerm", maxForTerm);
                    command.Parameters.AddWithValue("@totalMark", totalMark);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при вставке данных в базу данных: " + ex.Message);
            }
            finally
            {
                LoseConnection();
            }
        }


    }



}
