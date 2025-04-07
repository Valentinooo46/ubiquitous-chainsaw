using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ThreadExamples
{
    [Table("Guests")]
    public class Guest
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(50)] public string Name { get; set; } = null!;
        [Required, StringLength(100)] public string Email { get; set; } = null!;

        [StringLength(200)] public string? ImageAdress { get; set; }
        
        public void  UploadImage()
        {
            string url = "https://picsum.photos/1200/800?grayscale";

            using HttpClient client = new HttpClient();
            var bytes = client.GetByteArrayAsync(url).Result;

            ImageAdress = Path.GetRandomFileName() + ".jpg";
            File.WriteAllBytes(@"../../../images/" + ImageAdress, bytes);
            
        }
    }
}
