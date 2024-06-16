using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;

namespace CarDealer.Data.Repository
{
    public class VehicleModelRepository : Repository<VehicleModel>, IVehicleModelRepository
    {

        private ApplicationDbContext _db;

        public VehicleModelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(VehicleModel vehicleModel)
        {
            _db.VehicleModels.Update(vehicleModel);
        }
    }

}
