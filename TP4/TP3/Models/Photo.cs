using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace TP3.Models
{
	public class Photo
	{
		public int Id { get; set; }
		[Required]
        public string? NomDuFichier { get; set; }
        public string? MimeType { get; set; }

        [JsonIgnore]
		public virtual Voyage Voyage { get; set; }
    }
}
