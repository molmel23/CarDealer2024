using CarDealer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CarDealer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }

}
