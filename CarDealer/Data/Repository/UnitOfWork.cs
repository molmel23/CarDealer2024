using CarDealer.Data.Repository.Interfaces;

namespace CarDealer.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Make = new MakeRepository(_db);
            VehicleModel = new VehicleModelRepository(_db);
            Vehicle = new VehicleRepository(_db);
        }

        public IMakeRepository Make { get; private set; }
        public IVehicleModelRepository VehicleModel { get; private set; }
        public IVehicleRepository Vehicle { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
