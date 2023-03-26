using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using eAuto.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly IImageManager _imageManager;

        private readonly ICarService _carService;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly IGenerationService _generationService;
        private readonly IBodyTypeService _bodyTypeService;
        private readonly IEngineService _engineService;
        private readonly IDriveTypeService _driveTypeService;
        private readonly ITransmissionService _transmissionService;

        public CarController(
            ICarService carService,
            IImageManager imageManager,
            IBrandService brandService, 
            IModelService modelService, 
            IGenerationService generationService,
            IBodyTypeService bodyTypeService,
            IEngineService engineService,
            IDriveTypeService driveTypeService,
            ITransmissionService transmissionService,
            ILogger<CarController> logger)
        {
            _carService = carService;
            _imageManager = imageManager;
			_brandService = brandService;
			_modelService = modelService;
			_generationService = generationService;
            _bodyTypeService = bodyTypeService;
            _engineService = engineService;
            _driveTypeService = driveTypeService;
            _transmissionService = transmissionService;
			_logger = logger;
		}

        [HttpGet]
        public async Task<IActionResult> Index()
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
                        Description = i.Description,
                        BrandId = i.BrandId,
                        Brand = i.Brand,
                        ModelId = i.ModelId,
                        Model = i.Model,
                        GenerationId = i.GenerationId,
                        Generation = i.Generation,
                        BodyTypeId = i.BodyTypeId,
                        BodyType = i.BodyType,
                        EngineId = i.EngineId,
                        Engine = i.Engine,
                        DriveTypeId = i.DriveTypeId,
                        DriveType = i.DriveType,
                        TransmissionId = i.TransmissionId,
                        Transmission = i.Transmission
					});
 
                return View(carsList);
			}
			catch (CarNotFoundException ex)
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
                var bodyTypesList = await _bodyTypeService.GetBodyTypeModelsAsync();
                var enginessList = await _engineService.GetEngineModelsAsync();
                var driveTypesList = await _driveTypeService.GetDriveTypeModelsAsync();
                var transmissionsList = await _transmissionService.GetTransmissionModelsAsync();

                CarCreateViewModel createViewModel = new()
                {
                    CarVModel = new(),
                    Brands = brandsList.Select(s => new SelectListItem { Value = s.BrandId.ToString(), Text = s.Name}),
                    Models = modelsList.Select(s => new SelectListItem { Value = s.ModelId.ToString(), Text = s.Name}),
                    Generations = generationsList.Select(s => new SelectListItem { Value = s.GenerationId.ToString(), Text = s.Name}),
                    BodyTypes = bodyTypesList.Select(s => new SelectListItem { Value = s.BodyTypeId.ToString(), Text = s.Name}),
                    Engines = enginessList.Select(s => new SelectListItem { Value = s.EngineId.ToString(), Text = s.IdentificationName}),
                    DriveTypes= driveTypesList.Select(s => new SelectListItem { Value = s.DriveTypeId.ToString(), Text = s.Name}),
                    Transmissions = transmissionsList.Select(s => new SelectListItem { Value = s.TransmissionId.ToString(), Text = s.Name})
                };
                return View(createViewModel);
            }
            catch (CarNotFoundException ex)
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
		public IActionResult Create(CarCreateViewModel viewModel)
		{
			ICar car;
            var files = HttpContext.Request.Form.Files;

            try
			{
                if(files.Count > 0)
                {
                    _imageManager.UploadFiles(files, WebConstants.CarsImages);
                    viewModel.CarVModel.PictureUrl = string.Concat(WebConstants.CarsImages, _imageManager.FilesName.FirstOrDefault());
                }
                if (ModelState.IsValid)
                {
					car = _carService.CreateCarDomainModel();

					car.CarId = viewModel.CarVModel.CarId;
                    car.PriceInitial = viewModel.CarVModel.PriceInitial;
                    car.PictureUrl = viewModel.CarVModel.PictureUrl;
                    car.Year = viewModel.CarVModel.Year;
                    car.DateArrival = viewModel.CarVModel.DateArrival;
                    car.Odometer = viewModel.CarVModel.Odometer;
					car.Description = viewModel.CarVModel.Description;
					car.BrandId = viewModel.CarVModel.BrandId;
					car.ModelId = viewModel.CarVModel.ModelId;
                    car.GenerationId = viewModel.CarVModel.GenerationId;
                    car.BodyTypeId = viewModel.CarVModel.BodyTypeId;
                    car.EngineId = viewModel.CarVModel.EngineId;
                    car.DriveTypeId = viewModel.CarVModel.DriveTypeId;
                    car.TransmissionId = viewModel.CarVModel.TransmissionId;
                    
                    car.Save();
                    TempData["Success"] = "Car created successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
			catch (CarNotFoundException)
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

                var bodyTypesIList = await _bodyTypeService.GetBodyTypeModelsAsync();
                var bodyTypesList = bodyTypesIList
                    .Select(bd => new BodyTypeViewModel()
                    {
                        BodyTypeId = bd.BodyTypeId,
                        Name = bd.Name
                    }).ToList();

                var enginesIList = await _engineService.GetEngineModelsAsync();
                var enginesList = enginesIList
                    .Select(e => new EngineViewModel()
                    {
                        EngineId = e.EngineId,
                        IdentificationName = e.IdentificationName,
                        Type = e.Type,
                        Capacity = e.Capacity,
                        Power = e.Power,
                        Description = e.Description,
                        Brand = e.Brand,
                        Model = e.Model,
                        Generation = e.Generation
                    }).ToList();

                var driveTypesIList = await _driveTypeService.GetDriveTypeModelsAsync();
                var driveTypesList = driveTypesIList
                    .Select(d => new DriveTypeViewModel()
                    {
                        DriveTypeId = d.DriveTypeId,
                        Name = d.Name
                    }).ToList();

                var transmissionsIList = await _transmissionService.GetTransmissionModelsAsync();
                var transmissionsList = transmissionsIList
                    .Select(t => new TransmissionViewModel()
                    {
                        TransmissionId = t.TransmissionId,
                        Name = t.Name
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
                        Description = viewModel.Description,
						BrandId = viewModel.BrandId,
                        Brand = viewModel.Brand,
						ModelId = viewModel.ModelId,
                        Model = viewModel.Model,
                        GenerationId = viewModel.GenerationId,
                        Generation = viewModel.Generation,
                        BodyTypeId = viewModel.BodyTypeId,
                        EngineId = viewModel.EngineId,
                        DriveTypeId = viewModel.DriveTypeId,
                        TransmissionId = viewModel.TransmissionId
            },
                    Brands = brandsList.Select(b => new SelectListItem { Value = b.BrandId.ToString(), Text = b.Name }),
                    Models = modelsList.Select(b => new SelectListItem { Value = b.ModelId.ToString(), Text = b.Name }),
                    Generations = generationsList.Select(b => new SelectListItem { Value = b.GenerationId.ToString(), Text = b.Name }),
                    BodyTypes = bodyTypesList.Select(b => new SelectListItem { Value = b.BodyTypeId.ToString(), Text = b.Name }),
                    Engines = enginesList.Select(b => new SelectListItem { Value = b.EngineId.ToString(), Text = b.IdentificationName }),
                    DriveTypes = driveTypesList.Select(b => new SelectListItem { Value = b.DriveTypeId.ToString(), Text = b.Name }),
                    Transmissions = transmissionsList.Select(b => new SelectListItem { Value = b.TransmissionId.ToString(), Text = b.Name })
                };

                return View(createViewModel);
            }

            catch (CarNotFoundException ex)
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
        public IActionResult Edit(CarCreateViewModel viewModel)
        {
			ICar car;
            try
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    _imageManager.RemoveFile(WebConstants.CarsImages, viewModel.CarVModel.PictureUrl);
                    _imageManager.UploadFiles(files, WebConstants.CarsImages);
                    viewModel.CarVModel.PictureUrl = string.Concat(WebConstants.CarsImages, _imageManager.FilesName.FirstOrDefault());
                }

                if (ModelState.IsValid)
                {
					car = _carService.GetCarModel(viewModel.CarVModel.CarId);

                    car.PriceInitial = viewModel.CarVModel.PriceInitial;
                    car.PictureUrl = viewModel.CarVModel.PictureUrl;
                    car.Year = viewModel.CarVModel.Year;
                    car.DateArrival = viewModel.CarVModel.DateArrival;
                    car.Odometer = viewModel.CarVModel.Odometer;
                    car.Description = viewModel.CarVModel.Description; 
                    car.BrandId = viewModel.CarVModel.BrandId;
                    car.Brand = _brandService.GetBrandModel(viewModel.CarVModel.BrandId).Name.ToString();
                    car.ModelId = viewModel.CarVModel.ModelId;
                    car.Model = _modelService.GetModelModel(viewModel.CarVModel.ModelId).Name.ToString();
                    car.GenerationId = viewModel.CarVModel.GenerationId;
                    car.Generation = _generationService.GetGenerationModel(viewModel.CarVModel.GenerationId).Name.ToString();
                    car.BodyTypeId = viewModel.CarVModel.BodyTypeId;
                    car.BodyType = _bodyTypeService.GetBodyTypeModel(viewModel.CarVModel.BodyTypeId).Name.ToString();
                    car.EngineId = viewModel.CarVModel.EngineId;
                    car.Engine = _engineService.GetEngineModel(viewModel.CarVModel.EngineId).IdentificationName.ToString();
                    car.DriveTypeId = viewModel.CarVModel.DriveTypeId;
                    car.DriveType = _driveTypeService.GetDriveTypeModel(viewModel.CarVModel.DriveTypeId).Name.ToString();
                    car.TransmissionId = viewModel.CarVModel.TransmissionId;
                    car.Transmission = _transmissionService.GetTransmissionModel(viewModel.CarVModel.TransmissionId).Name.ToString();

                    car.Save();

                    TempData["Success"] = "Car edited successfully";
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (CarNotFoundException)
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
                var car = _carService.GetCarModel(id);
                _imageManager.RemoveFile(WebConstants.CarsImages, car.PictureUrl);
                car.Delete();
                TempData["Success"] = "Car deleted successfully";
                return RedirectToAction("Index");
            }

            catch (CarNotFoundException ex)
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