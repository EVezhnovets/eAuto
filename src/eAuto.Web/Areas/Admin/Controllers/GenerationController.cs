using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenerationController : Controller
    {
        private readonly IAppLogger<GenerationController> _logger;
        private readonly IGenerationService _generationService;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;

        public GenerationController(
            IGenerationService generationService, 
            IBrandService brandService, 
            IModelService modelService, 
            IAppLogger<GenerationController> logger)
        {
            _generationService = generationService;
			_brandService = brandService;
			_modelService = modelService;
			_logger = logger;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var iGenerationsList = await _generationService.GetGenerationModelsAsync();
                var generationsList = iGenerationsList
					.Select(i => new GenerationViewModel
					{
                        GenerationId = i.GenerationId,
                        Name = i.Name,
                        BrandId = i.BrandId,
                        Brand = i.Brand,
						ModelId = i.ModelId,
                        Model = i.Model,
					})
                    .OrderBy( i=> i.Brand)
                    .ThenBy(i => i.Model)
                    .ThenBy(i => i.Name);
 
                return View(generationsList);
			}
			catch (GenericNotFoundException<GenerationController> ex)
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
                var modelsList = await _modelService.GetModelModelsAsync();

                GenerationCreateViewModel createViewModel = new()
                {
                    GenerationVModel = new(),
                    Brands = brandsList.Select(s => new SelectListItem { Value = s.BrandId.ToString(), Text = s.Name })
                                       .OrderBy(s => s.Text),
                    Models = modelsList.Select(s => new SelectListItem { Value = s.ModelId.ToString(), Text = s.Name})
                                     .OrderBy(s => s.Text)
				};
				return View(createViewModel);
            }
            catch (GenericNotFoundException<GenerationController> ex)
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
		public IActionResult Create(GenerationCreateViewModel viewModel)
		{
			IGeneration generation;
			try
			{
                if (ModelState.IsValid)
                {
					generation = _generationService.CreateGenerationDomainModel();

                    generation.GenerationId = viewModel.GenerationVModel.GenerationId;
					generation.BrandId = viewModel.GenerationVModel.BrandId;
					generation.ModelId = viewModel.GenerationVModel.ModelId;
                    generation.Name = viewModel.GenerationVModel.Name;

                    generation.Save();
                    TempData["Success"] = "Generation created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (GenericNotFoundException<GenerationController> ex)
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

                var modelsIList = await _modelService.GetModelModelsAsync();
                var modelsList = modelsIList
					.Select(b => new ModelViewModel()
                    {
                        ModelId = b.ModelId,
                        Brand= b.Brand,
                        Name = b.Name
                    }).ToList();

                var viewModel = _generationService.GetGenerationModel(id);

				GenerationCreateViewModel createViewModel = new()
                {
                    GenerationVModel = new GenerationViewModel
					{
                        GenerationId = viewModel.GenerationId,
						Name = viewModel.Name,
						BrandId = viewModel.BrandId,
                        Brand = viewModel.Brand,
						ModelId = viewModel.ModelId,
                        Model = viewModel.Model
					},
                    Brands = brandsList.Select(b => new SelectListItem { Value = b.BrandId.ToString(), Text = b.Name }),
                    Models = modelsList.Select(b => new SelectListItem { Value = b.ModelId.ToString(), Text = b.Name })
                };

                return View(createViewModel);
            }

            catch (GenericNotFoundException<GenerationController> ex)
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
        public IActionResult Edit(GenerationCreateViewModel viewModel)
        {
			IGeneration generation;
            try
            {
                if (ModelState.IsValid)
                {
					generation = _generationService.GetGenerationModel(viewModel.GenerationVModel.GenerationId);
                    generation.Name = viewModel.GenerationVModel.Name;
                    generation.BrandId = viewModel.GenerationVModel.BrandId;
                    generation.Brand = _brandService.GetBrandModel(viewModel.GenerationVModel.BrandId).Name.ToString();
                    generation.ModelId = viewModel.GenerationVModel.ModelId;
                    generation.Model = _modelService.GetModelModel(viewModel.GenerationVModel.ModelId).Name.ToString();
                    generation.Save();
                    TempData["Success"] = "Generation edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (GenericNotFoundException<GenerationController> ex)
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
                var generation = _generationService.GetGenerationModel(id);
                generation.Delete();
                TempData["Success"] = "Generation deleted successfully";
                return RedirectToAction("Index");
            }

            catch (GenericNotFoundException<GenerationController> ex)
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