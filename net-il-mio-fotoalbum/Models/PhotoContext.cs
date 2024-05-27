﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : IdentityDbContext<IdentityUser>
    {
        private const string SqlServer = "Data Source=localhost;Initial Catalog=photos;Integrated Security=True;Trust Server Certificate=True";
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SqlServer);
        }

      //protected override void OnModelCreating(ModelBuilder modelBuilder)
      //  {
      //      base.OnModelCreating(modelBuilder);

      //      modelBuilder.Entity<Photo>()
      //          .HasMany(p => p.Categories)
      //          .WithMany(c => c.Photos)
      //          .UsingEntity(j => j.ToTable("PhotoCategory"));

      //      modelBuilder.Entity<Category>()
      //          .HasMany(c => c.Photos)
      //          .WithMany(p => p.Categories)
      //          .UsingEntity(j => j.ToTable("PhotoCategory"));
      //  }
    }
}
