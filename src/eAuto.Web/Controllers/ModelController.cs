using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Controllers
{
    public class ModelController : Controller
    {
        private readonly ILogger<ModelController> _logger;
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;

        public ModelController(IModelService modelService ,ILogger<ModelController> logger, IBrandService brandService)
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
                List<ModelViewModel> modelsList = new();
                foreach (var iModel in iModelsList)
                {
                    modelsList.Add(new ModelViewModel
                    {
                        ModelId = iModel.ModelId,
                        Name = iModel.Name,
                        BrandId = iModel.BrandId,
                        Brand = iModel.Brand
                    });
                }
 
                return View(modelsList);
			}
			catch (ModelNotFoundException ex)
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
				//TODO logger
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				//TODO logger
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
			catch (ModelNotFoundException)
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
                var viewModel = _modelService.GetModelModel(id);

                var modelViewModel = new ModelViewModel
                {
                    ModelId = id,
                    Name = viewModel.Name
                };
                return View(modelViewModel);
            }

            catch (ModelNotFoundException ex)
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
        public IActionResult Edit(ModelViewModel viewModel)
        {
            IModel model;
            try
            {
                if (ModelState.IsValid)
                {
                    model = _modelService.GetModelModel(viewModel.ModelId);
                    model.Name = viewModel.Name;
                    model.Save();
                    TempData["Success"] = "Model edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (ModelNotFoundException)
            {
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