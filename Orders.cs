using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OrderSystem
{
    public class Category
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
    /// <summary>
    /// Товари
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public void SaveProduct(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO tbl_products (CategoryId, Name, Description, Price, CreatedDate) VALUES (@CategoryId, @Name, @Description, @Price, @CreatedDate)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", CategoryId);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Description", Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
    /// <summary>
    /// Замовник
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public void AddUser(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO tbl_users (FirstName, LastName, Email, PhoneNumber, Password) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE tbl_users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, Password = @Password WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static User GetUser(int id, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber, Password FROM tbl_users WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Password = reader.GetString(5)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static List<User> GetAllUsers(string connectionString)
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, FirstName, LastName, Email, PhoneNumber, Password FROM tbl_users";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Password = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return users;
        }
    }

    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Замовлення
    /// </summary>
    public class Order
    {
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public void AddOrderItem(int productId, int count)
        {
            OrderItems.Add(new OrderItem
            {
                ProductId = productId,
                Count = count,
                
            });
        }
            
        public void SaveOrder(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert order
                        string orderSql = "INSERT INTO tbl_orders (UserId, OrderStatusId, CreatedDate) OUTPUT INSERTED.Id VALUES (@UserId, @OrderStatusId, @CreatedDate)";
                        using (SqlCommand orderCommand = new SqlCommand(orderSql, connection, transaction))
                        {
                            orderCommand.Parameters.AddWithValue("@UserId", UserId);
                            orderCommand.Parameters.AddWithValue("@OrderStatusId", OrderStatusId);
                            orderCommand.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                            int orderId = (int)orderCommand.ExecuteScalar();

                            // Insert order items
                            string orderItemSql = "INSERT INTO tbl_order_items (OrderId, ProductId, Count) VALUES (@OrderId, @ProductId, @Count)";
                            foreach (var item in OrderItems)
                            {
                                using (SqlCommand orderItemCommand = new SqlCommand(orderItemSql, connection, transaction))
                                {
                                    orderItemCommand.Parameters.AddWithValue("@OrderId", orderId);
                                    orderItemCommand.Parameters.AddWithValue("@ProductId", item.ProductId);
                                    orderItemCommand.Parameters.AddWithValue("@Count", item.Count);
                                    
                                    orderItemCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Предмети замовлення
    /// </summary>u
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
    /// <summary>
    /// Статуси замовлень
    /// </summary>
    public class OrderStatusManager
    {
        private readonly string _connectionString;

        public OrderStatusManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddOrderStatus(string name)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO tbl_order_statuses (Name) VALUES (@Name)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateOrderStatus(int id, string name)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "UPDATE tbl_order_statuses SET Name = @Name WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public OrderStatus GetOrderStatus(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, Name FROM tbl_order_statuses WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OrderStatus
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<OrderStatus> GetAllOrderStatuses()
        {
            List<OrderStatus> statuses = new List<OrderStatus>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, Name FROM tbl_order_statuses";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statuses.Add(new OrderStatus
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return statuses;
        }
    }
    public class DatabaseQueries
    {
        private readonly string _connectionString;

        public DatabaseQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<(int OrderId, string FirstName, string LastName)> GetOrdersWithUserNames()
        {
            var result = new List<(int, string, string)>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT o.Id, u.FirstName, u.LastName FROM tbl_orders o JOIN tbl_users u ON o.UserId = u.Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                        }
                    }
                }
            }
            return result;
        }

        public List<(int OrderId, string ProductName, decimal Price)> GetOrderedProductsWithPrices()
        {
            var result = new List<(int, string, decimal)>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT oi.OrderId, p.Name, oi.CurrentPrice FROM tbl_order_items oi JOIN tbl_products p ON oi.ProductId = p.Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add((reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2)));
                        }
                    }
                }
            }
            return result;
        }

        public decimal GetAverageProductPrice()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT AVG(Price) FROM tbl_products";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    return (decimal)command.ExecuteScalar();
                }
            }
        }

        public List<(int UserId, int OrderCount)> GetOrderCountPerUser()
        {
            var result = new List<(int, int)>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT UserId, COUNT(*) FROM tbl_orders GROUP BY UserId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add((reader.GetInt32(0), reader.GetInt32(1)));
                        }
                    }
                }
            }
            return result;
        }

        public void DeleteUsersWithNoOrders()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM tbl_users WHERE Id NOT IN (SELECT DISTINCT UserId FROM tbl_orders)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetUsersWithTotalSpentMoreThan(decimal amount)
        {
            var result = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = @"
                    SELECT u.Id, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Password
                    FROM tbl_users u
                    JOIN (
                        SELECT o.UserId, SUM(oi.CurrentPrice * oi.Count) AS TotalSpent
                        FROM tbl_orders o
                        JOIN tbl_order_items oi ON o.Id = oi.OrderId
                        GROUP BY o.UserId
                    ) t ON u.Id = t.UserId
                    WHERE t.TotalSpent > @Amount";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Amount", amount);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Password = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return result;
        }

        public (int ProductId, string ProductName) GetMostOrderedProduct()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = @"
                    SELECT TOP 1 p.Id, p.Name
                    FROM tbl_order_items oi
                    JOIN tbl_products p ON oi.ProductId = p.Id
                    GROUP BY p.Id, p.Name
                    ORDER BY SUM(oi.Count) DESC";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                }
            }
            return (0, null);
        }
    }
}