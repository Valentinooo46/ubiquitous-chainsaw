using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobizon
{
    [Table("User")]
    public class UserEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<UserNotificationEntity> UserNotifications { get; set; } = [];
        public ICollection<UserEventEntity> UserEvents { get; set; } =[];
    }

    [Table("UserNotification")]
    public class UserNotificationEntity
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        public DateTime SentAt { get; set; }
        [Required, MaxLength(100)]
        public string Message { get; set; } = string.Empty;
    }

    [Table("UserEvent")]
    public class UserEventEntity
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        [ForeignKey("EventType")]
        public int EventTypeId { get; set; }
        public EventTypeEntity EventType { get; set; } = null!;
        [Required]
        public DateTime EventDate { get; set; }
    }

    [Table("EventType")]
    public class EventTypeEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty; // "День народження", "День ангела" і т.д.
    }
}
