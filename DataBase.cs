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


        public void InsertMark(int userID, string userName, string forTerm, string forUnit, string forFA, int totalMark)
        {
            try
            {
                OpenConnection();

                // Проверка наличия userID в таблице users
                string checkUserQuery = "SELECT COUNT(*) FROM users WHERE id = @userID";
                using (SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, sqlConnection))
                {
                    checkUserCommand.Parameters.AddWithValue("@userID", userID);
                    int userCount = (int)checkUserCommand.ExecuteScalar();

                    
                }

                // Вставка данных в таблицу mark
                string insertMarkQuery = "INSERT INTO mark (userName, forTerm, forUnit, forFA, totalMark) VALUES (@userName, @forTerm, @forUnit, @forFA, @totalMark)";
                using (SqlCommand command = new SqlCommand(insertMarkQuery, sqlConnection))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@forTerm", forTerm);
                    command.Parameters.AddWithValue("@forUnit", forUnit);
                    command.Parameters.AddWithValue("@forFA", forFA);
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
