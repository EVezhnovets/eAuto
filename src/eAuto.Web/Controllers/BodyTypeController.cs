using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eAuto.Web.Controllers
{
    public class BodyTypeController : Controller
    {
        private readonly ILogger<BodyTypeController> _logger;
        private readonly IBodyTypeService _bodyTypeService;

        public BodyTypeController(IBodyTypeService bodyTypeService ,ILogger<BodyTypeController> logger)
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
				return View(result);
			}
			catch (BodyTypeNotFoundException ex)
			{
				//TODO logger
				return NotFound(ex.Message);
			}
			catch(Exception ex)
			{
				//TODO logger
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
		public async Task<IActionResult> Create(BodyTypeViewModel viewModel)
		{
			IBodyType bodyType;
			try
			{
                if (ModelState.IsValid)
                {
                    bodyType = await _bodyTypeService.CreateBodyTypeModelAsync(viewModel.Name);
                    bodyType.Save();
                    TempData["Success"] = "Body Type created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (BodyTypeNotFoundException)
			{
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
                    Name = viewModel.Name
                };
                return View(bodyTypeViewModel);
            }

            catch (BodyTypeNotFoundException ex)
            {
                _logger!.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger!.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BodyTypeViewModel viewModel)
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
            catch (BodyTypeNotFoundException)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var bodyType = _bodyTypeService.GetBodyTypeModel(id);
                bodyType.Delete();
                TempData["Success"] = "Body Type deleted successfully";
                return RedirectToAction("Index");
            }

            catch (BodyTypeNotFoundException ex)
            {
                _logger!.LogError(ex.Message);
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                _logger!.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            #endregion

        }
    }
}