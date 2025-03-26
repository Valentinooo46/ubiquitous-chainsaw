// See https://aka.ms/new-console-template for more information
using Mobizon;


//UserEntity user = new UserEntity
//{
//    Name = "Ivan",
//    PhoneNumber = "+380123456789"
//};
//ManagerUser.AddUser(user);
//EventTypeEntity eventType = new EventTypeEntity
//{
//    Name = "Birthday"
//};
//ManagerEventType.AddEventType(eventType);
//UserEventEntity userEvent = new UserEventEntity
//{
//    UserId = 1,
//    EventTypeId = 1,
//    EventDate = DateTime.Now
//};
//ManagerEvent.AddUserEvent(userEvent);
var EventList = ManagerEvent.GetUserEvent();

var mobizonClient = new MobizonClient("your_api_key_here"); // Замініть на ваш API ключ

foreach (var item in EventList)
{
    if (item.EventDate.Date == DateTime.Now.Date)
    {
        Console.WriteLine("Happy Birthday ");
        UserNotificationEntity userNotification = new UserNotificationEntity
        {
            UserId = item.UserId,
            SentAt = DateTime.Now,
            Message = "Happy Birthday"
        };
        ManagerUserNotification.AddUserNotification(userNotification);

        // Отримайте номер телефону користувача
        var user = ManagerUser.GetUserById(item.UserId);
        if (user != null)
        {
            mobizonClient.SendMessage(user.PhoneNumber, userNotification.Message);
        }
    }

}
