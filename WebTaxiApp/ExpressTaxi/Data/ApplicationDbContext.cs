using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using ExpressTaxi.Domain;
using ExpressTaxi.Models;
using ExpressTaxi.Models.Taxi;
using ExpressTaxi.Models.Brand;

namespace ExpressTaxi.Data
{
    public class ApplicationDbContext : IdentityDbContext<TaxiUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Taxi> Taxies { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
