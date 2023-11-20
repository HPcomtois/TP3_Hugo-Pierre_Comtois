using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TP3.Models
{
    public class Voyage
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string Img { get; set; } = "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg";
        public bool Visible { get; set; }
        [JsonIgnore]
        public virtual List<User> Users { get; set; } = new List<User>();
    }
}
