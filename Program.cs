using Bogus.DataSets;
using System.Diagnostics;

namespace ThreadExamples
{

    internal class Program
    {

        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            GuestManager guestManager = new GuestManager();
            guestManager.AddGuestAsyncEvent += PrintResult;
            guestManager.AddGuest(50);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
        static void PrintResult(uint count)
        {
            Console.WriteLine($"Added {count} guests");
        }

    }
}
