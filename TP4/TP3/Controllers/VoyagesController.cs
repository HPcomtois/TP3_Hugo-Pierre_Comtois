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
using TP3.Service;

namespace TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VoyagesController : ControllerBase
    {

		VoyagesService voyagesService;

		public VoyagesController(VoyagesService service)
        {
            voyagesService = service;
		}

        // GET: api/Voyages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyages()
        {
			string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			List<Voyage>? voyages = await voyagesService.GetAll(userid);
            if(voyages == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Aucun voyage détecté." });
            }
			return voyages;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyagePublique()
        {
			List<Voyage>? voyages = await voyagesService.GetVoyagesPubliques();
            if(voyages == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "La liste est vide." });
			}
            else
            {
                return voyages;
            }
		}

        // GET: api/Voyages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voyage>> GetVoyage(int id)
        {
            Voyage? voyage = await voyagesService.Get(id);
            if(voyage == null)
            {
                return NotFound("Voyage introuvalbe.");
            }
            else
            {
                return voyage;
            }
        }

        [HttpPut("{id}")]
		public async Task<IActionResult> Partager(int id, string email)
        {
            User? user = await voyagesService.GetUser(email);
            if(user == null)
            {
                return NotFound("Utilisateur introuvable.");
            }
            else
            {
				Voyage? voyage = await voyagesService.Get(id);

				if (user.Voyages.Contains(voyage))
				{
					return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Le user a déjà ce voyage." });
				}
				else
				{
                    user.Voyages.Add(voyage);
					return Ok(new { Message = "Voyage ajouté à l'utilisateur." });
				}
			}
		}

        // POST: api/Voyages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voyage>> PostVoyage(Voyage? voyage)
        {
			string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			voyage = await voyagesService.Post(voyage, userid);
			if (voyage.Users == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé." });
			}
            else
            {
				return CreatedAtAction("GetVoyage", new { id = voyage.Id }, voyage);
			}
		}

        // DELETE: api/Voyages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyage(int id)
        {
			string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Voyage voyage = await voyagesService.Get(id);
            if (voyage == null)
            {
                return NotFound("Le voyage n'a pas pu être supprimé.");
            }
            voyage = await voyagesService.Delete(id, userid);

            return Ok(new { Message = "Voyage supprimé." });
		}
    }
}
