using Microsoft.AspNetCore.Identity;

namespace TP3.Models
{
	public class User : IdentityUser
	{
		public virtual List<Voyage>? Voyages { get; set; }
    }
}
