using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdminRole)]
    public class CarController : Controller
    {
        private readonly IAppLogger<CarController> _logger;
        private readonly IImageManager _imageManager;

        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly IGenerationService _generationService;
        private readonly IBodyTypeService _bodyTypeService;
        private readonly IDriveTypeService _driveTypeService;
        private readonly ITransmissionService _transmissionService;

        public CarController(
            ICarService carService,
            IImageManager imageManager,
            IBrandService brandService, 
            IModelService modelService, 
            IGenerationService generationService,
            IBodyTypeService bodyTypeService,
            IDriveTypeService driveTypeService,
            ITransmissionService transmissionService,
            IAppLogger<CarController> logger)
        {
            _carService = carService;
            _imageManager = imageManager;
			_brandService = brandService;
			_modelService = modelService;
			_generationService = generationService;
            _bodyTypeService = bodyTypeService;
            _driveTypeService = driveTypeService;
            _transmissionService = transmissionService;
			_logger = logger;
		}

        [HttpGet]
        public async Task<IActionResult> Index(CarsIndexViewModel carsIndex, int? pageId)
        {
			try
			{
				var iCarsList = await _carService.GetCarModelsAsync();
                var carsList = iCarsList
					.Select(i => new CarViewModel
					{
                        CarId = i.CarId,
                        PriceInitial = i.PriceInitial,
                        PictureUrl = i.PictureUrl,
                        Year = i.Year,
                        DateArrival = i.DateArrival,
                        Odometer = i.Odometer,
                        Description = i.Description!,
                        EngineIdentificationName = i.EngineIdentificationName,
                        EngineCapacity = i.EngineCapacity,
                        EngineFuelType = i.EngineFuelType!,
                        EnginePower = i.EnginePower,
                        BrandId = i.BrandId,
                        Brand = i.Brand!,
                        ModelId = i.ModelId,
                        Model = i.Model!,
                        GenerationId = i.GenerationId,
                        Generation = i.Generation!,
                        BodyTypeId = i.BodyTypeId,
                        BodyType = i.BodyType!,
                        DriveTypeId = i.DriveTypeId,
                        DriveType = i.DriveType!,
                        TransmissionId = i.TransmissionId,
                        Transmission = i.Transmission!
					});

				var carsQuery = carsList
					.Where(i => (!carsIndex.BrandFilterApplied.HasValue || i.BrandId == carsIndex.BrandFilterApplied)
								&& (!carsIndex.ModelFilterApplied.HasValue || i.ModelId == carsIndex.ModelFilterApplied)
								&& (!carsIndex.GenerationFilterApplied.HasValue || i.GenerationId == carsIndex.GenerationFilterApplied)
								&& (!carsIndex.BodyTypeFilterApplied.HasValue || i.BodyTypeId == carsIndex.BodyTypeFilterApplied)
								&& (!carsIndex.DriveTypeFilterApplied.HasValue || i.DriveTypeId == carsIndex.DriveTypeFilterApplied)
								&& (!carsIndex.TransmissionFilterApplied.HasValue || i.TransmissionId == carsIndex.TransmissionFilterApplied))
					.Select(i =>
						new CarViewModel(
							i.CarId,
							i.PriceInitial,
							i.PictureUrl!,
							i.Year,
							i.DateArrival,
							i.Odometer,
							i.Description!,
                            i.EngineIdentificationName,
                            i.EngineCapacity,
                            i.EngineFuelType!,
                            i.EngineFuelTypeId,
                            i.EnginePower,
							i.Brand!,
							i.Model!,
							i.Generation!,
							i.BodyType!,
							i.DriveType!,
							i.Transmission!
					)).ToList();

				var carsResult = new CarsIndexViewModel()
				{
					CarVModels = carsQuery,
					Brands = (await GetBrands()).ToList(),
					Models = (await GetModels()).ToList(),
					Generations = (await GetGenerations()).ToList(),
					BodyTypes = (await GetBodyTypes()).ToList(),
					DriveTypes = (await GetDriveTypes()).ToList(),
					Transmissions = (await GetTransmissions()).ToList(),

				};
				return View(carsResult);
			}
			catch (GenericNotFoundException<CarController> ex)
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
                var generationsList = await _generationService.GetGenerationModelsAsync();
                var bodyTypesList = await _bodyTypeService.GetBodyTypeModelsAsync();
                var driveTypesList = await _driveTypeService.GetDriveTypeModelsAsync();
                var transmissionsList = await _transmissionService.GetTransmissionModelsAsync();

                CarCreateViewModel createViewModel = new()
                {
                    CarVModel = new(),
                    Brands = brandsList.Select(s => new SelectListItem { Value = s.BrandId.ToString(), Text = s.Name}).OrderBy(s=>s.Text),
                    Models = modelsList.Select(s => new SelectListItem { Value = s.ModelId.ToString(), Text = s.Name}).OrderBy(s => s.Text),
                    Generations = generationsList.Select(s => new SelectListItem { Value = s.GenerationId.ToString(), Text = s.Name}).OrderBy(s => s.Text),
                    BodyTypes = bodyTypesList.Select(s => new SelectListItem { Value = s.BodyTypeId.ToString(), Text = s.Name}).OrderBy(s => s.Text),
                    DriveTypes= driveTypesList.Select(s => new SelectListItem { Value = s.DriveTypeId.ToString(), Text = s.Name}).OrderBy(s => s.Text),
                    Transmissions = transmissionsList.Select(s => new SelectListItem { Value = s.TransmissionId.ToString(), Text = s.Name}).OrderBy(s => s.Text)
				};
                return View(createViewModel);
            }
            catch (GenericNotFoundException<CarController> ex)
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
		public IActionResult Create(CarCreateViewModel viewModel)
		{
			ICar car;
            var files = HttpContext.Request.Form.Files;

            try
			{
                if(files.Count > 0)
                {
                    _imageManager.UploadFiles(files, WebConstants.CarsImages);
                    viewModel.CarVModel!.PictureUrl = string.Concat(WebConstants.CarsImages, _imageManager.FilesName.FirstOrDefault());
                }
                if (ModelState.IsValid)
                {
					car = _carService.CreateCarDomainModel();

					car.CarId = viewModel.CarVModel!.CarId;
                    car.PriceInitial = viewModel.CarVModel.PriceInitial;
                    car.PictureUrl = viewModel.CarVModel.PictureUrl;
                    car.Year = viewModel.CarVModel.Year;
                    car.DateArrival = viewModel.CarVModel.DateArrival;
                    car.Odometer = viewModel.CarVModel.Odometer;
					car.Description = viewModel.CarVModel.Description;
                    car.EngineIdentificationName = viewModel.CarVModel.EngineIdentificationName;
                    car.EngineCapacity = viewModel.CarVModel.EngineCapacity;
                    car.EngineFuelType = viewModel.CarVModel.EngineFuelType;
                    car.EnginePower = viewModel.CarVModel.EnginePower;
					car.BrandId = viewModel.CarVModel.BrandId;
					car.ModelId = viewModel.CarVModel.ModelId;
                    car.GenerationId = viewModel.CarVModel.GenerationId;
                    car.BodyTypeId = viewModel.CarVModel.BodyTypeId;
                    car.DriveTypeId = viewModel.CarVModel.DriveTypeId;
                    car.TransmissionId = viewModel.CarVModel.TransmissionId;
                    
                    car.Save();
                    TempData["Success"] = "Car created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (GenericNotFoundException<CarController> ex)
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
                        Name = b.Name!
                    }).ToList();

                var modelsIList = await _modelService.GetModelModelsAsync();
                var modelsList = modelsIList
					.Select(m => new ModelViewModel()
                    {
                        ModelId = m.ModelId,
                        Brand= m.Brand!,
                        Name = m.Name!
                    }).ToList();
                
                var generationsIList = await _generationService.GetGenerationModelsAsync();
                var generationsList = generationsIList
					.Select(g => new GenerationViewModel()
                    {
						GenerationId = g.GenerationId,
                        Brand= g.Brand!,
                        Model = g.Model!,
                        Name = g.Name!
                    }).ToList();

                var bodyTypesIList = await _bodyTypeService.GetBodyTypeModelsAsync();
                var bodyTypesList = bodyTypesIList
                    .Select(bd => new BodyTypeViewModel()
                    {
                        BodyTypeId = bd.BodyTypeId,
                        Name = bd.Name!
                    }).ToList();

                var driveTypesIList = await _driveTypeService.GetDriveTypeModelsAsync();
                var driveTypesList = driveTypesIList
                    .Select(d => new DriveTypeViewModel()
                    {
                        DriveTypeId = d.DriveTypeId,
                        Name = d.Name!
                    }).ToList();

                var transmissionsIList = await _transmissionService.GetTransmissionModelsAsync();
                var transmissionsList = transmissionsIList
                    .Select(t => new TransmissionViewModel()
                    {
                        TransmissionId = t.TransmissionId,
                        Name = t.Name!
                    }).ToList();

                var viewModel = _carService.GetCarModel(id);

				CarCreateViewModel createViewModel = new()
                {
                    CarVModel = new CarViewModel
					{
                        CarId = viewModel.CarId,
                        PriceInitial = viewModel.PriceInitial,
                        PictureUrl = viewModel.PictureUrl,
                        Year = viewModel.Year,
                        DateArrival = viewModel.DateArrival,
                        Odometer = viewModel.Odometer,
                        Description = viewModel.Description!,
                        EngineIdentificationName = viewModel.EngineIdentificationName,
                        EngineCapacity = viewModel.EngineCapacity,
                        EngineFuelType = viewModel.EngineFuelType!,
                        EnginePower = viewModel.EnginePower,
						BrandId = viewModel.BrandId,
                        Brand = viewModel.Brand!,
						ModelId = viewModel.ModelId,
                        Model = viewModel.Model!,
                        GenerationId = viewModel.GenerationId,
                        Generation = viewModel.Generation!,
                        BodyTypeId = viewModel.BodyTypeId,
                        DriveTypeId = viewModel.DriveTypeId,
                        TransmissionId = viewModel.TransmissionId
            },
                    Brands = brandsList.Select(b => new SelectListItem { Value = b.BrandId.ToString(), Text = b.Name }).OrderBy(b => b.Text),
                    Models = modelsList.Select(b => new SelectListItem { Value = b.ModelId.ToString(), Text = b.Name }).OrderBy(b => b.Text),
                    Generations = generationsList.Select(b => new SelectListItem { Value = b.GenerationId.ToString(), Text = b.Name }).OrderBy(b => b.Text),
                    BodyTypes = bodyTypesList.Select(b => new SelectListItem { Value = b.BodyTypeId.ToString(), Text = b.Name }).OrderBy(b => b.Text),
                    DriveTypes = driveTypesList.Select(b => new SelectListItem { Value = b.DriveTypeId.ToString(), Text = b.Name }).OrderBy(b => b.Text),
                    Transmissions = transmissionsList.Select(b => new SelectListItem { Value = b.TransmissionId.ToString(), Text = b.Name }).OrderBy(b => b.Text)
				};

                return View(createViewModel);
            }

            catch (GenericNotFoundException<CarController> ex)
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
        public IActionResult Edit(CarCreateViewModel viewModel)
        {
			ICar car;
            try
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    _imageManager.RemoveFile(WebConstants.CarsImages, viewModel.CarVModel!.PictureUrl!);
                    _imageManager.UploadFiles(files, WebConstants.CarsImages);
                    viewModel.CarVModel.PictureUrl = string.Concat(WebConstants.CarsImages, _imageManager.FilesName.FirstOrDefault());
                }

                if (ModelState.IsValid)
                {
					car = _carService.GetCarModel(viewModel.CarVModel!.CarId);

                    car.PriceInitial = viewModel.CarVModel.PriceInitial;
                    car.PictureUrl = viewModel.CarVModel.PictureUrl;
                    car.Year = viewModel.CarVModel.Year;
                    car.DateArrival = viewModel.CarVModel.DateArrival;
                    car.Odometer = viewModel.CarVModel.Odometer;
                    car.Description = viewModel.CarVModel.Description;
                    car.EngineIdentificationName = viewModel.CarVModel.EngineIdentificationName;
                    car.EngineCapacity = viewModel.CarVModel.EngineCapacity;
                    car.EngineFuelType = viewModel.CarVModel.EngineFuelType;
                    car.EnginePower = viewModel.CarVModel.EnginePower;
                    car.BrandId = viewModel.CarVModel.BrandId;
                    car.Brand = _brandService.GetBrandModel(viewModel.CarVModel.BrandId).Name!.ToString();
                    car.ModelId = viewModel.CarVModel.ModelId;
                    car.Model = _modelService.GetModelModel(viewModel.CarVModel.ModelId).Name!.ToString();
                    car.GenerationId = viewModel.CarVModel.GenerationId;
                    car.Generation = _generationService.GetGenerationModel(viewModel.CarVModel.GenerationId).Name!.ToString();
                    car.BodyTypeId = viewModel.CarVModel.BodyTypeId;
                    car.BodyType = _bodyTypeService.GetBodyTypeModel(viewModel.CarVModel.BodyTypeId).Name!.ToString();
                    car.DriveTypeId = viewModel.CarVModel.DriveTypeId;
                    car.DriveType = _driveTypeService.GetDriveTypeModel(viewModel.CarVModel.DriveTypeId).Name!.ToString();
                    car.TransmissionId = viewModel.CarVModel.TransmissionId;
                    car.Transmission = _transmissionService.GetTransmissionModel(viewModel.CarVModel.TransmissionId).Name!.ToString();

                    car.Save();

                    TempData["Success"] = "Car edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (GenericNotFoundException<CarController> ex)
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
                var car = _carService.GetCarModel(id);
                _imageManager.RemoveFile(WebConstants.CarsImages, car.PictureUrl!);
                car.Delete();
                TempData["Success"] = "Car deleted successfully";
                return RedirectToAction("Index");
            }

            catch (GenericNotFoundException<CarController> ex)
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

		private async Task<IEnumerable<SelectListItem>> GetBrands()
		{
			var brands = await _brandService.GetBrandModelsAsync();
			IEnumerable<BrandViewModel> brandsVM = brands.Select(i => new BrandViewModel(i.BrandId, i.Name!));

			var items = brandsVM
				.Select(brand => new SelectListItem() { Value = brand.BrandId.ToString(), Text = brand.Name })
				.OrderBy(brand => brand.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Brands", Selected = true };
			items.Insert(0, allItems);

			return items;
		}

		private async Task<IEnumerable<SelectListItem>> GetModels()
		{
			var models = await _modelService.GetModelModelsAsync();
			IEnumerable<ModelViewModel> modelsVM = models.Select(i => new ModelViewModel(i.ModelId, i.Name!, i.Brand!));

			var items = modelsVM
				.Select(model => new SelectListItem { Value = model.ModelId.ToString(), Text = model.Name })
				.OrderBy(model => model.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Models", Selected = true };
			items.Insert(0, allItems);

			return items;
		}

		private async Task<IEnumerable<SelectListItem>> GetGenerations()
		{
			var generations = await _generationService.GetGenerationModelsAsync();
			IEnumerable<GenerationViewModel> generationsVM = generations.Select(i => new GenerationViewModel(i.GenerationId, i.Name!, i.Brand!, i.Model!));

			var items = generationsVM
				.Select(generation => new SelectListItem { Value = generation.GenerationId.ToString(), Text = generation.Name })
				.OrderBy(generation => generation.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Generations", Selected = true };
			items.Insert(0, allItems);

			return items;
		}

		private async Task<IEnumerable<SelectListItem>> GetBodyTypes()
		{
			var bodyTypes = await _bodyTypeService.GetBodyTypeModelsAsync();
			IEnumerable<BodyTypeViewModel> bodyTypesVM = bodyTypes.Select(i => new BodyTypeViewModel(i.BodyTypeId, i.Name!));

			var items = bodyTypesVM
				.Select(bodyType => new SelectListItem { Value = bodyType.BodyTypeId.ToString(), Text = bodyType.Name })
				.OrderBy(bodyType => bodyType.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Body Types", Selected = true };
			items.Insert(0, allItems);

			return items;
		}

		private async Task<IEnumerable<SelectListItem>> GetDriveTypes()
		{
			var driveTypes = await _driveTypeService.GetDriveTypeModelsAsync();
			IEnumerable<DriveTypeViewModel> driveTypesVM = driveTypes.Select(
				i => new DriveTypeViewModel(i.DriveTypeId, i!.Name!));

			var items = driveTypesVM
				.Select(driveType => new SelectListItem { Value = driveType.DriveTypeId.ToString(), Text = driveType.Name })
				.OrderBy(driveType => driveType.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Drive Types", Selected = true };
			items.Insert(0, allItems);

			return items;
		}

		private async Task<IEnumerable<SelectListItem>> GetTransmissions()
		{
			var transmissions = await _transmissionService.GetTransmissionModelsAsync();
			IEnumerable<TransmissionViewModel> transmissionsVM = transmissions.Select(
				i => new TransmissionViewModel(i.TransmissionId, i.Name!));

			var items = transmissionsVM
				.Select(transmission => new SelectListItem { Value = transmission.TransmissionId.ToString(), Text = transmission.Name })
				.OrderBy(transmission => transmission.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Transmissions", Selected = true };
			items.Insert(0, allItems);

			return items;
		}
	}
}