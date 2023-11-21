using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TP3.Models;

namespace TP3.Data
{
    public class TP3Context : IdentityDbContext<User>
    {
        public TP3Context (DbContextOptions<TP3Context> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			PasswordHasher<User> hasher = new PasswordHasher<User>();
			User u1 = new User
			{
				Id = "11111111-1111-1111-1111-111111111111",
				UserName = "Bigboy32",
				Email = "candyCruise@mail.com",
				NormalizedEmail = "CANDYCRUISE@MAIL.COM",
				NormalizedUserName = "BIGBOY32"
			};
			u1.PasswordHash = hasher.HashPassword(u1, "Bonjour !");
			builder.Entity<User>().HasData(u1);

			builder.Entity<Voyage>().HasData(new Voyage()
            {
                Id = 1,
                Name = "Allemagne",
                Img = "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg",
                Visible = true
            },
            new Voyage()
            {
                Id = 2,
                Name="Algérie",
                Img = "https://www.routesdumonde.com/wp-content/uploads/2019/06/Thumbnail-Japon.jpg",
                Visible = false
			});

            builder.Entity<Voyage>()
                .HasMany(u => u.Users)
                .WithMany(v => v.Voyages)
                .UsingEntity(e =>
                {
                    e.HasData(new { UsersId = u1.Id, VoyagesId = 1 });
					e.HasData(new { UsersId = u1.Id, VoyagesId = 2 });
				});
		}

		public DbSet<TP3.Models.Voyage> Voyage { get; set; } = default!;
    }
}
