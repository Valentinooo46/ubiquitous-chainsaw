using OrderSystem;
using System;
using System.Data.SqlClient;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        string ConString = "*";
        //Product product = new Product();
        //product.Description = null;
        //for (int i = 0; i < 10; i++)
        //{ 
        //    product.Name = $"Product{i + 1}";
        //    product.Price = 1000;
        //    product.CategoryId = 1;

        //    product.CreatedDate = DateTime.Now;
        //    product.SaveProduct(ConString);
        //}
        //User user = new User();
        //for (int i = 0; i < 10; i++)
        //{

        //    user.FirstName = $"John{i+1}";
        //    user.LastName = $"Dee{i}";
        //    user.Email = $"JohnDee{i+1}@gmail.com";
        //    user.PhoneNumber = "1234567890";
        //    user.Password = "1234567890";
        //    user.AddUser(ConString);
        //}

        //Order order = new Order();

        //for (int i = 0; i < 10; i++)
        //{
        //    order.UserId = i + 1;
        //    order.OrderStatusId = 1;
        //    order.CreatedDate = DateTime.Now;


        //    order.AddOrderItem(1,i+1);
        //    order.AddOrderItem(2, i + 2);
        //    order.AddOrderItem(3, i + 3);
        //    order.SaveOrder(ConString);

        //}
        DatabaseQueries databaseQueries = new DatabaseQueries(ConString);
        var result = databaseQueries.GetAverageProductPrice();
        Console.WriteLine($"Average Product Price: {result}");
        var result2 = databaseQueries.GetUsersWithTotalSpentMoreThan(500);
        Console.WriteLine("Users with total spent more than 500:");
        foreach (var item in result2)
        {
            Console.WriteLine($"User Id: {item.Id},User Email:{item.Email}");
        }
        

    }
}
