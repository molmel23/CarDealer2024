using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using CarDealer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CarDealer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CarDealerRoles.Role_Admin)]
    public class MakeController : Controller
    {

        private IUnitOfWork _unitOfWork;

        public MakeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var model = _unitOfWork.Make.GetAll().ToList(); // Obtener todas las entidades Make
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Make make)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Make.Add(make);
                _unitOfWork.Save();
                TempData["success"] = "Make created successfully";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Edit(Make make)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Make.Update(make);
                _unitOfWork.Save();
                TempData["success"] = "Make updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            Make? makeFromDb = _unitOfWork.Make.Get(x => x.Id == id);

            if (makeFromDb == null)
            {
                return NotFound();
            }
            return View(makeFromDb);
        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null && id <= 0)
            {
                return NotFound();
            }

            Make? makeFromDb = _unitOfWork.Make.Get(x => x.Id == id);

            if (makeFromDb == null)
            {
                return NotFound();
            }
            return View(makeFromDb);
        }



        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Make? makeFromDB = _unitOfWork.Make.Get(x => x.Id == id);

            if (makeFromDB == null)
            {
                return NotFound();
            }

            _unitOfWork.Make.Remove(makeFromDB);
            _unitOfWork.Save();

            TempData["success"] = "Make deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
