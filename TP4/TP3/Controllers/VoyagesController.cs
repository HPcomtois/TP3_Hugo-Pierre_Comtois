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
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyage()
        {
			return await voyagesService.GetVoyages();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Voyage>>> GetVoyagePublique()
        {
			return await voyagesService.GetVoyagesPubliques();
        }

        // GET: api/Voyages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voyage>> GetVoyage(int id)
        {
			return await voyagesService.Get(id);
        }

        [HttpPut("{id}")]
		public async Task<IActionResult> Partager(int id, string email)
        {
            return await voyagesService.PartagerService(id, email);
		}

        // POST: api/Voyages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Voyage>> PostVoyage(Voyage voyage)
        {
           return await voyagesService.Post(voyage);
        }

        // DELETE: api/Voyages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoyage(int id)
        {
            return await voyagesService.Delete(id);
        }
    }
}
