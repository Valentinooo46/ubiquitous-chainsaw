using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            MyAppContext appContext = new MyAppContext();
            appContext.Database.EnsureCreated();
            Boolean exit = false;
            short choice;
            do
            {
                Console.WriteLine("1. Переглянути список користувачів");
                Console.WriteLine("2. Додати нового користувача");
                Console.WriteLine("3. Видалити користувача");

                Console.WriteLine("4. Вийти");
                Console.WriteLine("5.Додати користувачів");
                Console.Write("Ваш вибір: ");
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        List<UserEntityNS.UserEntity> list = ManagerUsers.GetUsers();
                        foreach (var item in list)
                        {
                            Console.WriteLine(item.ToString());
                        }
                        break;
                    case 2:
                        UserEntityNS.UserEntity user = new UserEntityNS.UserEntity();
                        Console.Write("Введіть ім'я: ");
                        user.FirstName = Console.ReadLine();
                        Console.Write("Введіть прізвище: ");
                        user.LastName = Console.ReadLine();
                        Console.Write("Введіть телефон: ");
                        user.Phone = Console.ReadLine();
                        Console.Write("Введіть дату народження: ");
                        user.BirthDate = DateOnly.Parse(Console.ReadLine());
                        ManagerUsers.AddUser(user);

                        break;
                    case 3:
                        Console.Write("Введіть Id користувача: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        ManagerUsers.DeleteUser(Id);

                        break;
                    case 4:
                        exit = true;
                        break;
                    case 5:
                        Console.Write("Введіть кількість користувачів: ");
                        int count = Convert.ToInt32(Console.ReadLine());
                        ManagerUsers.SeedData(count);
                        break;
                    default:
                        Console.WriteLine("Невірний вибір");
                        break;
                }
            } while (!exit);



        }
    }
}
