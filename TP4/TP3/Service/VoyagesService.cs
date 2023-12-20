using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TP3.Data;
using TP3.Models;

namespace TP3.Service
{
	public class VoyagesService : ControllerBase
	{
		private readonly TP3Context _context;
		UserManager<User> userManager;

		public VoyagesService(TP3Context context, UserManager<User> userManager)
		{
			_context = context;
			this.userManager = userManager;
		}

		public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyages()
		{
			if (_context.Voyage == null)
			{
				return NotFound();
			}
			string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			User user = await _context.Users.SingleAsync(u => u.Id == userid);

			if (user != null)
			{
				return user.Voyages;
			}
			else
			{
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé." });
			}
		}

		public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyagesPubliques()
		{
			if (_context.Voyage == null)
			{
				return NotFound();
			}

			List<User> users = await _context.Users.ToListAsync();
			List<Voyage> voyages = new List<Voyage>();

			if (users != null)
			{
				foreach (var user in users)
				{
					for (int i = 0; i < user.Voyages.Count; i++)
					{
						if (user.Voyages[i].Visible && !voyages.Contains(user.Voyages[i]))
						{
							voyages.Add(user.Voyages[i]);
						}
					}
				}
				return voyages;
			}

			return StatusCode(StatusCodes.Status400BadRequest, new { Message = "La liste est vide." });
		}

		public async Task<ActionResult<Voyage>> Get(int id)
		{
			if (_context.Voyage == null)
			{
				return NotFound();
			}
			var voyage = await _context.Voyage.FindAsync(id);

			if (voyage == null)
			{
				return NotFound();
			}

			return voyage;
		}

		public async Task<IActionResult> PartagerService(int id, string email)
		{
			User user = await _context.Users.Where(res => res.Email == email).FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound();
			}

			var voyage = Get(id).Result;

			if (user.Voyages.Contains(voyage.Value))
			{
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Le user a déjà ce voyage." });
			}
			else
			{
				user.Voyages.Add(voyage.Value);
				await _context.SaveChangesAsync();
				return Ok(new { Message = "Voyage ajouté à l'utilisateur." });
			}
		}

		public async Task<ActionResult<Voyage>> Post(Voyage voyage)
		{
			if (_context.Voyage == null)
			{
				return Problem("Entity set 'TP3Context.Voyage'  is null.");
			}

			string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			User user = _context.Users.Single(u => u.Id == userid);

			if(user != null)
			{
				voyage.Users.Add(user);
				user.Voyages.Add(voyage);
				_context.Voyage.Add(voyage);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetVoyage", new { id = voyage.Id }, voyage);
			}
			return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé." });
		}

		public async Task<IActionResult> Delete(int id)
		{
			string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			User user = _context.Users.Single(u => u.Id == userid);

			var voyage = Get(id).Result;

			if (_context.Voyage == null || user == null || voyage == null)
			{
				return NotFound();
			}

			if (!user.Voyages.Contains(voyage.Value))
			{
				return Unauthorized(new { Message = "Ce Voyage ne t'appartiens pas." });
			}
			_context.Voyage.Remove(voyage.Value);
			await _context.SaveChangesAsync();

			return Ok(new { Message = "Voyage supprimé." });
		}
		private bool VoyageExists(int id)
		{
			return (_context.Voyage?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
