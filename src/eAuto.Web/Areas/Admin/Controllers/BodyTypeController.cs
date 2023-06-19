using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdminRole)]
    public class BodyTypeController : Controller
    {
        private readonly IAppLogger<BodyTypeController> _logger;
        private readonly IBodyTypeService _bodyTypeService;

        public BodyTypeController(IBodyTypeService bodyTypeService ,IAppLogger<BodyTypeController> logger)
        {
            _logger = logger;
            _bodyTypeService = bodyTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var result = await _bodyTypeService.GetBodyTypeModelsAsync();
                var orderedResult = result
                    .Select(i => new BodyTypeViewModel
                {
                    BodyTypeId = i.BodyTypeId,
                    Name = i.Name!
                })
                    .OrderBy(i => i.Name);
				return View(orderedResult);
			}
			catch (GenericNotFoundException<BodyTypeController> ex)
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
		public IActionResult Create(BodyTypeViewModel viewModel)
		{
			IBodyType bodyType;
			try
			{
                if (ModelState.IsValid)
                {
                    bodyType = _bodyTypeService.CreateBodyTypeModel(viewModel.Name!);
                    bodyType.Save();
                    TempData["Success"] = "Body Type created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (GenericNotFoundException<BodyTypeController> ex)
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
                var viewModel = _bodyTypeService.GetBodyTypeModel(id);

                var bodyTypeViewModel = new BodyTypeViewModel
                {
                    BodyTypeId = id,
                    Name = viewModel.Name!
                };
                return View(bodyTypeViewModel);
            }

            catch (GenericNotFoundException<BodyTypeController> ex)
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
        public IActionResult Edit(BodyTypeViewModel viewModel)
        {
            IBodyType bodyType;
            try
            {
                if (ModelState.IsValid)
                {
                    bodyType = _bodyTypeService.GetBodyTypeModel(viewModel.BodyTypeId);
                    bodyType.Name = viewModel.Name;
                    bodyType.Save();
                    TempData["Success"] = "Body Type edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (GenericNotFoundException<BodyTypeController> ex )
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
                var bodyType = _bodyTypeService.GetBodyTypeModel(id);
                bodyType.Delete();
                TempData["Success"] = "Body Type deleted successfully";
                return RedirectToAction("Index");
            }

            catch (GenericNotFoundException<BodyTypeController> ex)
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