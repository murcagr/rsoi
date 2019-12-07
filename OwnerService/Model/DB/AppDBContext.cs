using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace OwnerService.Model
{
    public class AppDBContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }


        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Owner>().ToTable("Owners");

            builder.Entity<Owner>().HasIndex(cat => cat.Id).IsUnique();

            builder.Entity<Owner>().Property(cat => cat.Id).ValueGeneratedOnAdd();
        }
    }
}
