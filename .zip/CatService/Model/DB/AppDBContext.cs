using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CatService.Model
{
    public class AppDBContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }


        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Cat>().ToTable("Cats");

            builder.Entity<Cat>().HasIndex(cat => cat.Id).IsUnique();
            builder.Entity<Cat>().Property(cat => cat.Id).ValueGeneratedOnAdd();
            builder.Entity<Cat>().Property(cat => cat.OwnerId).HasDefaultValue(0);
            builder.Entity<Cat>().Property(cat => cat.OwnerId).HasDefaultValue(0);
        }
    }
}
