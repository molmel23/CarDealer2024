using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;

namespace CarDealer.Data.Repository
{
    public class MakeRepository : Repository<Make>, IMakeRepository
    {

        private ApplicationDbContext _db;

        public MakeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Make make)
        {
            _db.Makes.Update(make);
        }
    }

}
