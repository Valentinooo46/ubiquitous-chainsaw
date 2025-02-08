using System;
using System.Data.SqlClient;
using System.Text.Json;
using System.IO;

public class EmployeeManager
{
    SqlConnection _connection;

    public int LastKey()
    {
        int result;
        using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT MAX(Id) FROM Employees", connection))
            {
                result = (int)command.ExecuteScalar();
                
            }
        }
        return result;
    }

    

    public EmployeeManager(string urlDatabase, string user, string password, string nameDatabase)
    {
        _connection = new SqlConnection($"Data Source={urlDatabase};User={user};Password={password};Initial Catalog={nameDatabase}");
        if (_connection == null)
        {
            throw new Exception("Connection is null");
        }
    }

    public void LoadSqlConnectionToJson(string filepath)
    {
        var connectionData = new SqlConnectionData((string)_connection.ConnectionString.Clone(), _connection.ConnectionTimeout);
        File.WriteAllText(filepath, JsonSerializer.Serialize(connectionData));
    }

    public void Create(Employee employee)
    {
        using (var connection =new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO dbo.Employees (ID, PIB, fax) VALUES (@ID, @PIB, @fax)", connection);
            command.Parameters.AddWithValue("@ID", employee.ID);
            command.Parameters.AddWithValue("@PIB", employee.PIB);
            command.Parameters.AddWithValue("@fax", employee.fax);
            command.ExecuteNonQuery();
        }
    }

    public Employee Read(int id)
    {
        using (var connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Employees WHERE ID = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Employee
                    (
                        id : (int)reader["Id"],
                        PIB : (string)reader["PIB"],
                        fax : (string)reader["fax"]
                    );
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public void Update(Employee employee)
    {
        using (var connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand("UPDATE Employees SET PIB = @PIB,fax = @fax WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@PIB", employee.PIB);
            command.Parameters.AddWithValue("@fax", employee.fax);
            command.Parameters.AddWithValue("@Id", employee.ID);
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(_connection.ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand("DELETE FROM Employees WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }
}

public class Employee
{
    public static int _id = 0;
    public int ID { get; set; }
    public string PIB { get; set; }
    public string fax { get; set; }
   
    static public Employee Generate(string PIB, string fax)
    {
        return new Employee(++_id, PIB, fax);

    }
    public Employee(int id ,string PIB,string fax) {
        this.ID = id;
        this.PIB = PIB;
        this.fax = fax;
    }

}

public class SqlConnectionData
{
    public string ConnectionString { get; set; }
    public int ConnectionTimeout { get; set; }

    public SqlConnectionData(string connectionString, int connectionTimeout)
    {
        ConnectionString = connectionString;
        ConnectionTimeout = connectionTimeout;
    }
}
