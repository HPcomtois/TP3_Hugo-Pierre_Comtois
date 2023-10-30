using System.Text.Json.Serialization;

namespace TP3.Models
{
    public class Voyage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Img { get; set; } = "";
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
