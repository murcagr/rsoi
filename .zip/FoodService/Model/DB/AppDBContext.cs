using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Model
{
    public class AppDBContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }


        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Food>().ToTable("Foods");

            builder.Entity<Food>().HasIndex(cat => cat.Id).IsUnique();

            builder.Entity<Food>().Property(cat => cat.Id).ValueGeneratedOnAdd();
        }
    }
}
