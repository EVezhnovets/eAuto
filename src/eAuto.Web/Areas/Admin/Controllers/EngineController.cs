using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EngineController : Controller
    {
        private readonly ILogger<EngineController> _logger;
        private readonly IEngineService _engineService;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly IGenerationService _generationService;

        public EngineController(
            IEngineService engineService, 
            IBrandService brandService, 
            IModelService modelService, 
            IGenerationService generationService, 
            ILogger<EngineController> logger)
        {
            _engineService = engineService;
			_brandService = brandService;
			_modelService = modelService;
			_generationService = generationService;
			_logger = logger;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
			try
			{
				var iEnginesList = await _engineService.GetEngineModelsAsync();
                var enginesList = iEnginesList
					.Select(i => new EngineViewModel
					{
                        EngineId = i.EngineId,
                        IdentificationName = i.IdentificationName,
                        Type = i.Type,
                        Capacity = i.Capacity,
                        Power = i.Power,
                        Description = i.Description,
                        BrandId = i.BrandId,
                        Brand = i.Brand,
						ModelId = i.ModelId,
                        Model = i.Model,
                        GenerationId = i.GenerationId,
                        Generation = i.Generation,
					});
 
                return View(enginesList);
			}
			catch (EngineNameNotFoundException ex)
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
                var modelsList = await _modelService.GetModelModelsAsync();
                var generationsList = await _generationService.GetGenerationModelsAsync();

                EngineCreateViewModel createViewModel = new()
                {
                    EngineVModel = new(),
                    Brands = brandsList.Select(s => new SelectListItem { Value = s.BrandId.ToString(), Text = s.Name}),
                    Models = modelsList.Select(s => new SelectListItem { Value = s.ModelId.ToString(), Text = s.Name}),
                    Generations = generationsList.Select(s => new SelectListItem { Value = s.GenerationId.ToString(), Text = s.Name})
                };
                return View(createViewModel);
            }
            catch (EngineNameNotFoundException ex)
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
		public IActionResult Create(EngineCreateViewModel viewModel)
		{
			IEngine engine;
			try
			{
                if (ModelState.IsValid)
                {
					engine = _engineService.CreateEngineDomainModel();

					engine.EngineId = viewModel.EngineVModel.EngineId;
					engine.IdentificationName = viewModel.EngineVModel.IdentificationName;
                    engine.Type = viewModel.EngineVModel.Type;
                    engine.Capacity = viewModel.EngineVModel.Capacity;
                    engine.Power = viewModel.EngineVModel.Power;
                    engine.Description = viewModel.EngineVModel.Description;
					engine.BrandId = viewModel.EngineVModel.BrandId;
					engine.ModelId = viewModel.EngineVModel.ModelId;
                    engine.GenerationId = viewModel.EngineVModel.GenerationId;
                    
                    engine.Save();
                    TempData["Success"] = "Engine created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (EngineNameNotFoundException)
			{
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
					.Select(m => new ModelViewModel()
                    {
                        ModelId = m.ModelId,
                        Brand= m.Brand,
                        Name = m.Name
                    }).ToList();
                
                var generationsIList = await _generationService.GetGenerationModelsAsync();
                var generationsList = generationsIList
					.Select(g => new GenerationViewModel()
                    {
						GenerationId = g.GenerationId,
                        Brand= g.Brand,
                        Model = g.Model,
                        Name = g.Name
                    }).ToList();

                var viewModel = _engineService.GetEngineModel(id);

				EngineCreateViewModel createViewModel = new()
                {
                    EngineVModel = new EngineViewModel
					{
                        EngineId = viewModel.EngineId,
                        IdentificationName = viewModel.IdentificationName,
                        Type = viewModel.Type,
                        Capacity = viewModel.Capacity,
                        Power = viewModel.Power,
                        Description = viewModel.Description,
						BrandId = viewModel.BrandId,
                        Brand = viewModel.Brand,
						ModelId = viewModel.ModelId,
                        Model = viewModel.Model,
                        GenerationId = viewModel.GenerationId,
                        Generation = viewModel.Generation
					},
                    Brands = brandsList.Select(b => new SelectListItem { Value = b.BrandId.ToString(), Text = b.Name }),
                    Models = modelsList.Select(b => new SelectListItem { Value = b.ModelId.ToString(), Text = b.Name }),
                    Generations = generationsList.Select(b => new SelectListItem { Value = b.GenerationId.ToString(), Text = b.Name })
                };

                return View(createViewModel);
            }

            catch (EngineNameNotFoundException ex)
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
        public IActionResult Edit(EngineCreateViewModel viewModel)
        {
			IEngine engine;
            try
            {
                if (ModelState.IsValid)
                {
					engine = _engineService.GetEngineModel(viewModel.EngineVModel.EngineId);

                    engine.IdentificationName = viewModel.EngineVModel.IdentificationName;
                    engine.Type = viewModel.EngineVModel.Type;
                    engine.Capacity = viewModel.EngineVModel.Capacity;
                    engine.Power = viewModel.EngineVModel.Power;
                    engine.Description = viewModel.EngineVModel.Description; 
                    engine.BrandId = viewModel.EngineVModel.BrandId;
                    engine.Brand = _brandService.GetBrandModel(viewModel.EngineVModel.BrandId).Name.ToString();
                    engine.ModelId = viewModel.EngineVModel.ModelId;
                    engine.Model = _modelService.GetModelModel(viewModel.EngineVModel.ModelId).Name.ToString();
                    engine.GenerationId = viewModel.EngineVModel.GenerationId;
                    engine.Generation = _generationService.GetGenerationModel(viewModel.EngineVModel.GenerationId).Name.ToString();

                    engine.Save();

                    TempData["Success"] = "Engine edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (EngineNameNotFoundException)
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
                var engine = _engineService.GetEngineModel(id);
                engine.Delete();
                TempData["Success"] = "Engine deleted successfully";
                return RedirectToAction("Index");
            }

            catch (EngineNameNotFoundException ex)
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