// See https://aka.ms/new-console-template for more information
using Mobizon;


//UserEntity user = new UserEntity
//{
//    Name = "Ivan",
//    PhoneNumber = "+380123456789"
//};
//ManagerUser.AddUser(user);

//ManagerEventType.AddEventType("New Year");
//ManagerEventType.AddEventType("Christmas");
//ManagerEventType.AddEventType("Valentine's Day");

ManagerEvent.AddUserEvent(1, 9, DateTime.Now);
ManagerEvent.AddUserEvent(1, 10, DateTime.Now);
ManagerEvent.AddUserEvent(1, 11, DateTime.Now);
var EventList = ManagerEvent.GetUserEvent();

var mobizonClient = new MobizonClient("your_api_key_here"); // Замініть на ваш API ключ

foreach (var item in EventList)
{
    if (item.EventDate.Date == DateTime.Now.Date)
    {
        
        UserNotificationEntity userNotification = new UserNotificationEntity
        {
            UserId = item.UserId,
            SentAt = DateTime.Now
            
        };
        if (item.EventType.Name == "Birthday")
        {
            Console.WriteLine("Happy Birthday ");
            userNotification.Message = "Happy Birthday";
        }
        else if (item.EventType.Name == "Angel Day")
        {
            Console.WriteLine("Happy Angel Day ");
            userNotification.Message = "Happy Angel Day";
        }
        else if (item.EventType.Name == "New Year")
        {
            Console.WriteLine("Happy New Year ");
            userNotification.Message = "Happy New Year";
        }
        else if (item.EventType.Name == "Christmas")
        {
            Console.WriteLine("Merry Christmas ");
            userNotification.Message = "Merry Christmas";
        }
        else if (item.EventType.Name == "Valentine's Day")
        {
            Console.WriteLine("Happy Valentine's Day ");
            userNotification.Message = "Happy Valentine's Day";
        }
        ManagerUserNotification.AddUserNotification(userNotification);

        // Отримайте номер телефону користувача
        //var user = ManagerUser.GetUserById(item.UserId);
        //if (user != null)
        //{
        //    mobizonClient.SendMessage(user.PhoneNumber, userNotification.Message);
        //}
    }

}
