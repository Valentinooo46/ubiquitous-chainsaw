using Bogus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Example
{
    internal class USERS_CH_Manager
    {
        SqlConnection Connection;
        Faker generator = new Faker("uk");

        public USERS_CH_Manager()
        {
            Connection = null!;
        }

        public USERS_CH_Manager(string StringConnection)
        {
            Connection = new SqlConnection(StringConnection);
            if (Connection == null)
            {
                throw new Exception("Connection is null");
            }
        }

        public Boolean INSERT_RANDOM_GENERATED_USERS(int count_users)
        {
            if (Connection == null)
            {
                throw new Exception("Connection is null");
            }
            if (count_users < 1)
            {
                throw new Exception("count_users < 1");
            }

            try
            {
                Connection.Open();
                using (var command = new SqlCommand("INSERT INTO dbo.USERS_CH (EMAIL, NAME, FAX) VALUES (@EMAIL, @NAME, @FAX)", Connection))
                {
                    command.Parameters.Add(new SqlParameter("@EMAIL", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@NAME", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@FAX", System.Data.SqlDbType.NVarChar));

                    for (int i = 0; i < count_users; i++)
                    {
                        command.Parameters["@EMAIL"].Value = generator.Internet.Email();
                        command.Parameters["@NAME"].Value = generator.Name.FullName();
                        command.Parameters["@FAX"].Value = generator.Phone.PhoneNumber();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return true;
        }
        public void SELECT_ALL_USERS()
        {
            if (Connection == null)
            {
                throw new Exception("Connection is null");
            }
            try
            {
                Connection.Open();
                using (var command = new SqlCommand("SELECT * FROM dbo.USERS_CH", Connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"EMAIL: {reader["EMAIL"]}, NAME: {reader["NAME"]}, FAX: {reader["FAX"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
        }
        public int WATCHDOG_TIMER_SELECT_ALL_USERS()
        {

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            SELECT_ALL_USERS();
            sw.Stop();
            return sw.Elapsed.Milliseconds;
        }
        public void SEARCH_USER()
        {
            if (Connection == null)
            {
                throw new Exception("Connection is null");
            }
            try
            {
                Connection.Open();
                using (var command = new SqlCommand("SELECT * FROM dbo.USERS_CH WHERE NAME = @NAME", Connection))
                {
                    Console.Write("Enter the name of the user you want to find: ");
                    string name = Console.ReadLine();
                    command.Parameters.AddWithValue("@NAME", name);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"EMAIL: {reader["EMAIL"]}, NAME: {reader["NAME"]}, FAX: {reader["FAX"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }
        }
        public string GetDatabaseSize()
        {
            if (Connection == null)
            {
                throw new Exception("Connection is null");
            }

            string size = null!;

            try
            {
                Connection.Open();
                using (var command = new SqlCommand("USE FirstData; EXEC sp_spaceused", Connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string databaseSize = reader["database_size"].ToString();
                            size = databaseSize;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Connection.Close();
            }

            return size;
        }
    }
}
