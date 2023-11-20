using System.Text.Json.Serialization;

namespace TP3.Models
{
    public class Voyage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Img { get; set; } = "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg";
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
