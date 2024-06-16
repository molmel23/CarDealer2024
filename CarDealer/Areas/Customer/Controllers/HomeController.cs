using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            IEnumerable<Vehicle> vehicleList = _unitOfWork.Vehicle.GetAll(includeProperties: "Model,Model.Make");
            return View(vehicleList);
        }

        public IActionResult Details(int id)
        {

            Vehicle vehicle = _unitOfWork.Vehicle.Get(x => x.Id == id, "Model,Model.Make");
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }
    }
}
