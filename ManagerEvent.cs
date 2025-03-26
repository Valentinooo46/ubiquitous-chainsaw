using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobizon
{
    internal class ManagerEvent
    {
        public static void AddUserEvent(UserEventEntity userEvent)
        {
            using var db = new AppContext();
            userEvent.EventDate = userEvent.EventDate.ToUniversalTime();
            db.UserEvents.Add(userEvent);
            db.SaveChanges();
        }
        public static void AddUserEvent(int UserId, int EventTypeId, DateTime EventDate)
        {
            using var db = new AppContext();
            var userEvent = new UserEventEntity
            {
                UserId = UserId,
                EventTypeId = EventTypeId,
                EventDate = EventDate.ToUniversalTime()
            };
            db.UserEvents.Add(userEvent);
            db.SaveChanges();
        }

        public static List<UserEventEntity> GetUserEvent()
        {
            using var db = new AppContext();
            return [.. db.UserEvents.Include(x=>x.EventType)];
        }
        public static UserEventEntity? GetUserEventById(int id)
        {
            using var db = new AppContext();
            return db.UserEvents.FirstOrDefault(x => x.Id == id);
        }
        public static void DeleteUserEvent(int id)
        {
            using var db = new AppContext();
            var userEvent = db.UserEvents.FirstOrDefault(x => x.Id == id);
            if (userEvent != null)
            {
                db.UserEvents.Remove(userEvent);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Подію з таким Id не знайдено");
                db.SaveChanges();
            }
        }
        public static void UpdateUserEvent(UserEventEntity userEvent)
        {
            using var db = new AppContext();
            db.UserEvents.Update(userEvent);
            db.SaveChanges();
        }
    }
    public class ManagerUser
    {
        public static void AddUser(UserEntity user)
        {
            using (var db = new AppContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static List<UserEntity> GetUser()
        {
            using (var db = new AppContext())
            {
                return [.. db.Users];
            }
        }
        public static UserEntity? GetUserById(int id)
        {
            using (var db = new AppContext())
            {
                return db.Users.FirstOrDefault(x => x.Id == id);
            }
        }
        public static void DeleteUser(int id)
        {
            using (var db = new AppContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Користувача з таким Id не знайдено");
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateUser(UserEntity user)
        {
            using (var db = new AppContext())
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
        }
    }
    public class ManagerEventType
    {
        public static void AddEventType(EventTypeEntity eventType)
        {
            using (var db = new AppContext())
            {
                db.EventTypes.Add(eventType);
                db.SaveChanges();
            }
        }
        public static void AddEventType(string name)
        {
            using (var db = new AppContext())
            {
                var eventType = new EventTypeEntity
                {
                    Name = name
                };
                db.EventTypes.Add(eventType);
                db.SaveChanges();
            }
        }
        public static List<EventTypeEntity> GetEventType()
        {
            using (var db = new AppContext())
            {
                return [.. db.EventTypes];
            }
        }
        public static EventTypeEntity? GetEventTypeById(int id)
        {
            using (var db = new AppContext())
            {
                return db.EventTypes.FirstOrDefault(x => x.Id == id);
            }
        }
        public static void DeleteEventType(int id)
        {
            using (var db = new AppContext())
            {
                var eventType = db.EventTypes.FirstOrDefault(x => x.Id == id);
                if (eventType != null)
                {
                    db.EventTypes.Remove(eventType);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Тип події з таким Id не знайдено");
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateEventType(EventTypeEntity eventType)
        {
            using (var db = new AppContext())
            {
                db.EventTypes.Update(eventType);
                db.SaveChanges();
            }
        }
    }
    public class ManagerUserNotification
    {
        public static void AddUserNotification(UserNotificationEntity userNotification)
        {
            using (var db = new AppContext())
            {
                userNotification.SentAt = userNotification.SentAt.ToUniversalTime();
                db.UserNotifications.Add(userNotification);

                db.SaveChanges();
            }
        }
        public static List<UserNotificationEntity> GetUserNotification()
        {
            using (var db = new AppContext())
            {
                return [.. db.UserNotifications];
            }
        }
        public static UserNotificationEntity? GetUserNotificationById(int id)
        {
            using (var db = new AppContext())
            {
                return db.UserNotifications.FirstOrDefault(x => x.Id == id);
            }
        }
        public static void DeleteUserNotification(int id)
        {
            using (var db = new AppContext())
            {
                var userNotification = db.UserNotifications.FirstOrDefault(x => x.Id == id);
                if (userNotification != null)
                {
                    db.UserNotifications.Remove(userNotification);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Повідомлення з таким Id не знайдено");
                    db.SaveChanges();
                }
            }
        }
        public static void UpdateUserNotification(UserNotificationEntity userNotification)
        {
            using (var db = new AppContext())
            {
                db.UserNotifications.Update(userNotification);
                db.SaveChanges();
            }
        }
    }
}
