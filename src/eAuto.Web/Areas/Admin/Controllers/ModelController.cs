using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModelController : Controller
    {
        private readonly IAppLogger<ModelController> _logger;
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;

        public ModelController(
            IModelService modelService, 
            IAppLogger<ModelController> logger, 
            IBrandService brandService)
        {
            _logger = logger;
            _modelService = modelService;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var iModelsList = await _modelService.GetModelModelsAsync();
                var modelsList = iModelsList
                    .Select(i => new ModelViewModel
                    {
                        ModelId = i.ModelId,
                        Name = i.Name,
                        BrandId = i.BrandId,
                        Brand = i.Brand
                    });
 
                return View(modelsList);
			}
			catch (ModelNotFoundException ex)
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
		public async Task<IActionResult> Create()
		{
            try
            {
                var brandsList = await _brandService.GetBrandModelsAsync();
                
                OneToOneModel<ModelViewModel> viewModel = new()
                {
                    ViewModel = new(),
                    ModelFK = brandsList.Select(s => new SelectListItem { Value = s.BrandId.ToString(), Text = s.Name})
                };
                return View(viewModel);
            }
            catch (ModelNotFoundException ex)
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
		public IActionResult Create(ModelViewModel viewModel)
		{
			IModel model;
			try
			{
                if (ModelState.IsValid)
                { 
                    model = _modelService.CreateModelDomainModel();
                    model.ModelId = viewModel.ModelId;
                    model.BrandId = viewModel.BrandId;
                    model.Name = viewModel.Name;

                    model.Save();
                    TempData["Success"] = "Model created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (ModelNotFoundException ex)
			{
				_logger.LogError(ex, ex.Message);
				return RedirectToAction("Index");
			}
        }
        #endregion

        #region Edit

        [HttpGet]
        public async Task <IActionResult> Edit(int id)
        {
            try
            {
                var brandsIList = await _brandService.GetBrandModelsAsync();
                var brandsList = brandsIList
                    .Select(b => new BrandViewModel()
                    {
                        BrandId = b.BrandId,
                        Name = b.Name
                    }).ToList();

                var viewModel = _modelService.GetModelModel(id);

                ModelCreateViewModel createViewModel = new()
                {
                    ModelVModel = new ModelViewModel
                    {
                        ModelId = viewModel.ModelId,
                        BrandId = viewModel.BrandId,
                        Name = viewModel.Name,
                        Brand = viewModel.Brand,
                    },
                    Brands = brandsList.Select(b => new SelectListItem { Value = b.BrandId.ToString(), Text = b.Name })
                };

                return View(createViewModel);
            }

            catch (ModelNotFoundException ex)
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
        public IActionResult Edit(ModelCreateViewModel viewModel)
        {
			IModel model;
            try
            {
                if (ModelState.IsValid)
                {
                    model = _modelService.GetModelModel(viewModel.ModelVModel.ModelId);
                    model.Name = viewModel.ModelVModel.Name;
                    model.BrandId = viewModel.ModelVModel.BrandId;
                    model.Brand = _brandService.GetBrandModel(viewModel.ModelVModel.BrandId).Name.ToString();
                    model.Save();
                    TempData["Success"] = "Model edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (ModelNotFoundException ex)
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
                var model = _modelService.GetModelModel(id);
                model.Delete();
                TempData["Success"] = "Model deleted successfully";
                return RedirectToAction("Index");
            }

            catch (ModelNotFoundException ex)
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