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

		public async Task<List<Voyage>?> GetAll(string userId)
		{
			if (_context.Voyage == null)
			{
				return null;
			}
			User user = await _context.Users.SingleAsync(u => u.Id == userId);

			if (user != null)
			{
				return user.Voyages;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<Voyage>?> GetVoyagesPubliques()
		{
			if (_context.Voyage == null)
			{
				return null;
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

			return null;
		}

		public async Task<Voyage?> Get(int id)
		{
			if (_context.Voyage == null)
			{
				return null;
			}
			Voyage? voyage = await _context.Voyage.FindAsync(id);

			if (voyage == null)
			{
				return null;
			}
			else
			{
				return voyage;
			}
		}

		public async Task<User?> GetUser(string email)
		{
			User user = await _context.Users.Where(res => res.Email == email).FirstOrDefaultAsync();
			if (user == null)
			{
				return null;
			}
			else
			{
				return user;
			}
		}

		public async Task<Voyage?> Post(Voyage? voyage, string userId)
		{
			if (_context.Voyage == null)
			{
				return null;
			}

			User user = _context.Users.Single(u => u.Id == userId);

			if(user != null)
			{
				voyage.Users.Add(user);
				user.Voyages.Add(voyage);
				_context.Voyage.Add(voyage);
				await _context.SaveChangesAsync();

				return voyage;
			}
			return null;
		}

		public async Task<Voyage> Delete(int id, string userId)
		{
			User user = _context.Users.Single(u => u.Id == userId);

			var voyage = Get(id).Result;

			if (_context.Voyage == null || user == null || voyage == null)
			{
				return null;
			}

			if (!user.Voyages.Contains(voyage))
			{
				return null;
			}
			_context.Voyage.Remove(voyage);
			await _context.SaveChangesAsync();

			return voyage;
		}
		private bool VoyageExists(int id)
		{
			return (_context.Voyage?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
