using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {

        string connectionString = "workstation id=*;packet size=4096;user id=*;pwd=*;data source=*;persist security info=False;initial catalog=*;TrustServerCertificate=True";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Animals')
                CREATE TABLE Animals (
                    Id INT PRIMARY KEY IDENTITY,
                    Name NVARCHAR(50),
                    Species NVARCHAR(50),
                    Age INT,
                    ArrivalDate DATE
                )";
            using (SqlCommand command = new SqlCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }


        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();

        //    for (int i = 1; i <= 1000; i++)
        //    {
        //        string insertQuery = @"
        //            INSERT INTO Animals (Name, Species, Age, ArrivalDate)
        //            VALUES (@Name, @Species, @Age, @ArrivalDate)";

        //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@Name", "Animal" + i);
        //            command.Parameters.AddWithValue("@Species", "Species" + (i % 10));
        //            command.Parameters.AddWithValue("@Age", i % 15);
        //            command.Parameters.AddWithValue("@ArrivalDate", DateTime.Now.AddDays(-i));

        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT * FROM Animals";
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Species: {reader["Species"]}, Age: {reader["Age"]}, ArrivalDate: {reader["ArrivalDate"]}");
                    }
                }
            }
        }
    }
}
