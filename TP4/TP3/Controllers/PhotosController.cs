using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;
using TP3.Data;
using TP3.Models;

namespace TP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly TP3Context _context;

        public PhotosController(TP3Context context)
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpGet("{id}/{idVoyage}")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotosVoyage(int idVoyage)
        {
            Voyage voyage = await _context.Voyage.FindAsync(idVoyage);
            if (_context.Photo == null)
            {
                return NotFound();
            }
            return voyage.Photos;
        }

        // GET: api/Photos/5
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]/{size}/{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(string size, int id)
        {
            if (_context.Photo == null)
            {
                return NotFound();
            }
            Voyage? voyage = await _context.Voyage.FindAsync(id);
            Photo? photo = await _context.Photo.FindAsync(voyage.photoId);
            if (photo == null || photo.NomDuFichier == null || photo.MimeType == null || id == 0)
            {
                byte[] org = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/photos/" + size + "/" + "Star_of_David3.svg.png");
                return File(org, "image/png");
            }
            if (!Regex.Match(size, "grosses|petites").Success)
            {
                return BadRequest(new { Message = "Taille est mauvaise." });
            }
            byte[] bytes = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/photos/" + size + "/" + photo.NomDuFichier);
            return File(bytes, photo.MimeType);
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
            if (id != photo.Id)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [DisableRequestSizeLimit]
        [Route("[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Upload(int id)
        {
            try
            {
                IFormCollection formCollection = await Request.ReadFormAsync();
                IFormFile? file = formCollection.Files.GetFile("image");

				if (file != null)
                {
                    Image image = Image.Load(file.OpenReadStream());

                    Photo photo = new Photo() { };
					photo.NomDuFichier = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    photo.MimeType = file.ContentType;

                    image.Save(Directory.GetCurrentDirectory() + "/photos/grosses/" + photo.NomDuFichier);
                    await _context.SaveChangesAsync();
					image.Mutate(i =>
                    {
                        i.Resize(new ResizeOptions()
                        {
                            Mode = ResizeMode.Min,
                            Size = new Size() { Height = 125 }
                        });
                    });
                    image.Save(Directory.GetCurrentDirectory() + "/photos/petites/" + photo.NomDuFichier);

                    Voyage? voyage = await _context.Voyage.FindAsync(id);

                    //Ajout de la photo à la liste
                    voyage!.Photos.Add(photo);
                    photo.Voyage = voyage;

                    _context.Photo.Add(photo);
					await _context.SaveChangesAsync();

                    //Liaison pour la photo de couverture
                    Photo? photoAvecId = await _context.Photo.Where
                        (nom => nom.NomDuFichier == photo.NomDuFichier).SingleOrDefaultAsync();
					voyage!.photoId = photoAvecId!.Id;
					await _context.SaveChangesAsync();
					return Ok(photo);
				}
                else
                {
                    return NotFound();
                }	
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool PhotoExists(int id)
        {
            return (_context.Photo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
