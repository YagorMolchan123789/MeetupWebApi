using MeetupWebAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.EFCore
{
    public class MeetupDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Meetup> Meetups { get; set; }

        public MeetupDbContext(DbContextOptions<MeetupDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(u => u.FullName)
                .HasComputedColumnSql(@"""FirstName"" || ' ' || ""LastName""", stored: true);

            builder.Entity<Meetup>()
                .HasMany(m => m.Users)
                .WithMany(u => u.Meetups)
                .UsingEntity(j => j.ToTable("MeetupUsers"));
        }
    }
}
