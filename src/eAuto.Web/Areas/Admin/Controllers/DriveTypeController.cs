using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DriveTypeController : Controller
    {
        private readonly IAppLogger<DriveTypeController> _logger;
        private readonly IDriveTypeService _driveTypeService;

        public DriveTypeController(
            IDriveTypeService driveTypeService ,IAppLogger<DriveTypeController> logger)
        {
            _logger = logger;
            _driveTypeService = driveTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var driveTypes = await _driveTypeService.GetDriveTypeModelsAsync();
                var viewModelList = driveTypes
                    .Select(i => new DriveTypeViewModel
                    {
                        DriveTypeId = i.DriveTypeId,
                        Name = i.Name
                    });
				return View(viewModelList);
			}
			catch (DriveTypeNotFoundException ex)
			{
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
            
        }

		#region Create

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Create(DriveTypeViewModel viewModel)
		{
			IDriveType driveType;
			try
			{
                if (ModelState.IsValid)
                {
                    driveType = _driveTypeService.CreateDriveTypeDomainModel();

                    driveType.DriveTypeId = viewModel.DriveTypeId;
                    driveType.Name = viewModel.Name;

                    driveType.Save();
                    TempData["Success"] = "Drive Type created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (DriveTypeNotFoundException ex)
			{
				_logger.LogError(ex, ex.Message);
				return RedirectToAction("Index");
			}
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var viewModel = _driveTypeService.GetDriveTypeModel(id);

                var driveTypeViewModel = new DriveTypeViewModel
                {
                    DriveTypeId = id,
                    Name = viewModel.Name
                };
                return View(driveTypeViewModel);
            }

            catch (DriveTypeNotFoundException ex)
            {
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DriveTypeViewModel viewModel)
        {
            IDriveType driveType;
            try
            {
                if (ModelState.IsValid)
                {
                    driveType = _driveTypeService.GetDriveTypeModel(viewModel.DriveTypeId);
                    driveType.Name = viewModel.Name;
                    driveType.Save();
                    TempData["Success"] = "Drive Type edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (DriveTypeNotFoundException ex)
            {
				_logger.LogError(ex, ex.Message);
				return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var driveType = _driveTypeService.GetDriveTypeModel(id);
                driveType.Delete();
                TempData["Success"] = "Drive Type deleted successfully";
                return RedirectToAction("Index");
            }

            catch (DriveTypeNotFoundException ex)
            {
				_logger.LogError(ex, ex.Message);
				return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
            }
            #endregion
        }
    }
}