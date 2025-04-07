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
            List<Task> tasks = new List<Task>();

            if (count % 10 == 0)
            {
                for (int i = 0; i < count / 10; i++)
                {
                    tasks.Add(Task.Run(() => AddGuestAsync((uint)10)));
                }
            }
            else if (count % 2 == 0)
            {
                for (int i = 0; i < count / 2; i++)
                {
                    tasks.Add(Task.Run(() => AddGuestAsync((uint)2)));
                }
            }
            else if (count % 3 == 0)
            {
                for (int i = 0; i < count / 3; i++)
                {
                    tasks.Add(Task.Run(() => AddGuestAsync((uint)3)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    tasks.Add(Task.Run(() => AddGuestAsync((uint)1)));
                }
            }

            Task.WaitAll(tasks.ToArray());
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
