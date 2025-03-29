using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadExamples
{
    public delegate void AddGuestAsyncDelegate(uint count);
    public class GuestManager
    {
        public event AddGuestAsyncDelegate? AddGuestAsyncEvent;
        

        public void AddGuest(uint count)
        {

            if (count % 10 == 0)
            {
                Thread[] threads = new Thread[count / 10];
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(AddGuestAsync);
                    threads[i].Start((uint)10);

                }
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i].Join();
                }

            }
            else if (count % 2 == 0)
            {
                Thread[] threads = new Thread[count / 2];
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(AddGuestAsync);
                    threads[i].Start((uint)2);
                    
                }
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i].Join();
                }
            }
            else if (count % 3 == 0)
            {
                Thread[] threads = new Thread[count / 3];
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i] = new Thread(AddGuestAsync);
                    threads[i].Start((uint)3);

                }
                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i].Join();
                }
            }
            else
            {
                throw new ArgumentException("Not impemented yet...  :(");
            }
            AddGuestAsyncEvent?.Invoke(count);
        }
        private void AddGuestAsync(object? count)
        {
            if (count != null && count is uint uint_count)
            {
                Faker<Guest> faker = new Faker<Guest>()
                    .RuleFor(g => g.Name, f => f.Person.FullName)
                    .RuleFor(g => g.Email, f => f.Person.Email)
                    .FinishWith((f, g) =>
                    {
                        g.UploadImage();
                    });
                using (GuestContext context = new GuestContext())
                {
                    for (uint i = 0; i < uint_count; i++)
                    {
                        context.Guests.Add(faker.Generate());
                    }
                    context.SaveChanges();

                }
            }
            else
            {
                throw new ArgumentException("Invalid argument( waiting: <uint> )");
            }
        }
        public void DeleteGuest(uint Id)
        {
            using GuestContext context = new GuestContext();
            Guest? guest = context.Guests.Find(Id);
            if (guest != null)
            {
                context.Guests.Remove(guest);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Guest not found");
            }
        }
        public void UpdateGuest(uint Id)
        {
            using GuestContext context = new GuestContext();
            Guest? guest = context.Guests.Find(Id);
            if (guest != null)
            {
                guest.Name = "Updated Name";
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Guest not found");
            }
        }

    }
}
