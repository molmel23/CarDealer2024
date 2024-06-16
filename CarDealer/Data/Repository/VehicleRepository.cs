using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;

namespace CarDealer.Data.Repository
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {

        private ApplicationDbContext _db;

        public VehicleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Vehicle Vehicle)
        {
            _db.Vehicles.Update(Vehicle);
        }
    }
}
