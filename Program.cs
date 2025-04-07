using Bogus.DataSets;
using System.Diagnostics;
using System.Text;

namespace ThreadExamples
{

    internal class Program
    {

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            GuestManager guestManager = new GuestManager();
            guestManager.AddGuestAsyncEvent += PrintResult;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            guestManager.AddGuest(1);
            stopwatch.Stop();
            Console.WriteLine($"Використано часу на додавання 1 користувача: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Введіть кількість користувачів для додавання:");
            uint count = uint.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine($"Прогнозований час додавання:{stopwatch.ElapsedMilliseconds * (long)count} ms");
            if (count > 0)
            {
                stopwatch.Restart();
                guestManager.AddGuest(count);
                stopwatch.Stop();
                Console.WriteLine($"Використано часу на додавання {count} користувачів: {stopwatch.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.WriteLine("не валідна к-сть користувачів....");
            }

        }
        static void PrintResult(uint count)
        {
            Console.WriteLine($"Added {count} guests");
        }

    }
}
