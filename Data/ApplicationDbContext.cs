using HotelApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace HotelApp1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }


        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=HotelJenni;Trusted_Connection=True;TrustServerCertificate=true;");
                
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasSequence<int>("RoomNumbers", schema: "dbo")
                .StartsAt(200)
                .IncrementsBy(1);

            modelBuilder.Entity<Room>(b =>
            {
                b.HasIndex(r => r.RoomNumber).IsUnique();

                b.Property(r => r.RoomNumber)
                 .HasDefaultValueSql("NEXT VALUE FOR dbo.RoomNumbers")
                 .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Room>()
                .HasIndex(r => r.RoomNumber)
                .IsUnique();
        }
    }
}
