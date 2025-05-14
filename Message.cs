using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServer
{
    [Table("Messages")]
    public class Message
    {
        [Key]public int ID { get; set; }
        [Required,StringLength(200)] public string Sender { get; set; } = string.Empty;
        [Required,StringLength(1024)]  public string Text { get; set; } = string.Empty;
    }
}
