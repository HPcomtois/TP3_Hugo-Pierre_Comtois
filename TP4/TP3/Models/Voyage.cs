using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TP3.Models
{
    public class Voyage
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Img { get; set; }
        public bool Visible { get; set; }
        [JsonIgnore]
        public virtual List<User> Users { get; set; } = new List<User>();
		[JsonIgnore]
		public virtual List<Photo> Photos { get; set; } = new List<Photo>();
        [JsonIgnore]
        public int photoId { get; set; }

    }
}
