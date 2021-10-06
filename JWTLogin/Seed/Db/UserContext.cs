using JWTLogin.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTLogin.Seed.Db
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserViewModel>(x =>
            {
                x.ToTable("User");
                x.Property(c => c.Username)
                    .IsRequired()
                    .HasMaxLength(32);
                x.Property(c => c.Password)
                   .IsRequired()
                   .HasMaxLength(32);
            });

          
        }

        public DbSet<UserViewModel> Users { get; set; }
    }
}
