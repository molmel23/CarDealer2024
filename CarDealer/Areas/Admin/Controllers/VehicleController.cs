using CarDealer.Data.Repository.Interfaces;
using CarDealer.Models;
using CarDealer.Models.ViewModels;
using CarDealer.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CarDealerRoles.Role_Admin)]
    public class VehicleController : Controller
    {

        #region Properties_Constructor
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        public VehicleController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region HTTP_GET_POST

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            VehicleVM myModel = new()
            {
                Vehicle = new(),
                VehicleModelList = _unitOfWork.VehicleModel.GetAll(includeProperties: "Make").Select(i => new SelectListItem
                {
                    Text = i.Make.Name + " " + i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return View(myModel);
            }

            myModel.Vehicle = _unitOfWork.Vehicle.Get(x => x.Id == id);

            if (myModel.Vehicle == null)
            {
                return NotFound();
            }

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Upsert(VehicleVM _vehicleVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\vehicles");

                    if (_vehicleVM.Vehicle.PictureUrl != null)// Update
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, _vehicleVM.Vehicle.PictureUrl);

                        if (System.IO.File.Exists(oldImageUrl))
                            System.IO.File.Delete(oldImageUrl);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    _vehicleVM.Vehicle.PictureUrl = @"images\vehicles\" + fileName + extension;

                }


                if (_vehicleVM.Vehicle.Id == 0)
                    _unitOfWork.Vehicle.Add(_vehicleVM.Vehicle);
                else
                    _unitOfWork.Vehicle.Update(_vehicleVM.Vehicle);

                _unitOfWork.Save();
                TempData["success"] = "Vehicle saved successfully";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region API

        public IActionResult GetAll()
        {
            var vehicleList = _unitOfWork.Vehicle.GetAll(includeProperties: "Model,Model.Make");
            return Json(new { data = vehicleList });

        }

        public IActionResult Delete(int? id)
        {
            var vehicleToDelete = _unitOfWork.Vehicle.Get(x => x.Id == id);

            if (vehicleToDelete == null)
                return Json(new { success = false, message = "Error while deleting" });
            _unitOfWork.Vehicle.Remove(vehicleToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successfully" });

        }
        #endregion
    }
}
