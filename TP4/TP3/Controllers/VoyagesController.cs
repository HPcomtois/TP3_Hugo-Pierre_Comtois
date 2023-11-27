using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using TP3.Data;
using TP3.Models;

namespace TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VoyagesController : ControllerBase
    {
        private readonly TP3Context _context;
        UserManager<User> userManager;

        public VoyagesController(TP3Context context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: api/Voyages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyage()
        {
            if (_context.Voyage == null)
            {
                return NotFound();
            }
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User user = _context.Users.Single(u => u.Id == userid);

            if (user != null)
            {
                return user.Voyages;
            }

            return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé." });
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyagePublique()
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



        // GET: api/Voyages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voyage>> GetVoyage(int id)
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

        [HttpPut("{id}")]
		public async Task<IActionResult> Partager(int id, string email)
        {
            User user = await _context.Users.Where(res => res.Email == email).FirstOrDefaultAsync();
            if(user == null)
            {
                return NotFound();
            }

            var voyage = await _context.Voyage.FindAsync(id);
            if (voyage == null)
            {
                return NotFound();
            }

            if (user.Voyages.Contains(voyage))
            {
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Le user a déjà ce voyage." });
			}
            else
            {
				user.Voyages.Add(voyage);
                await _context.SaveChangesAsync();
				return Ok(new {Message = "Voyage ajouté à l'utilisateur."} );
			}
		}

        // POST: api/Voyages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voyage>> PostVoyage(Voyage voyage)
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

        // DELETE: api/Voyages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyage(int id)
        {
			string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			User user = _context.Users.Single(u => u.Id == userid);

            var voyage = await _context.Voyage.FindAsync(id);

			if (_context.Voyage == null || user == null || voyage == null)
            {
                return NotFound();
            }

            if (!user.Voyages.Contains(voyage))
            {
                return Unauthorized(new { Message = "Ce Voyage ne t'appartient pas."});
            }

            _context.Voyage.Remove(voyage);
            await _context.SaveChangesAsync();

            return Ok(new {Message = "Voyage supprimé."});
        }

        private bool VoyageExists(int id)
        {
            return (_context.Voyage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
