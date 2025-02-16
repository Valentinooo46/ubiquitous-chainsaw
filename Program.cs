using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {

        string connectionString = "*";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Connected to SQL Server");
            string sql = "INSERT INTO [dbo].[GUESTS]([NAME],[FAX],[ORDER_ID]) VALUES (@NAME,@FAX,@ORDER_ID)";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                for (int i = 0; i < 200; i++)
                {
                    command .Parameters.AddWithValue("@NAME", "Guest " + i);
                    command.Parameters.AddWithValue("@FAX", "Fax +380" + i);
                    command.Parameters.AddWithValue("@ORDER_ID", i);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
            sql = """
                INSERT INTO [dbo].[ORDERS]([DESCRIPTION],[PRICE],[ORDER_ID])
                     VALUES
                           (@DESCRIPTION,
                           @PRICE,
                           @ORDER_ID)
                """;
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                for (int i = 0; i < 200; i++)
                {
                    command.Parameters.AddWithValue("@DESCRIPTION", "Order " + i);
                    command.Parameters.AddWithValue("@PRICE", i * 100);
                    command.Parameters.AddWithValue("@ORDER_ID", i);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }
    }
}
