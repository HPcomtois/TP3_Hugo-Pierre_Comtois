using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<TP3.Models.Voyage> Voyage { get; set; } = default!;
    }
}
