using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Domain.Services;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace eAuto.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly IAppLogger<HomeController> _logger;

		private readonly ICarService _carService;
		private readonly IBrandService _brandService;
		private readonly IModelService _modelService;
		private readonly IGenerationService _generationService;
		private readonly IBodyTypeService _bodyTypeService;
		private readonly IEngineService _engineService;
		private readonly IDriveTypeService _driveTypeService;
		private readonly ITransmissionService _transmissionService;

		public HomeController(
			ICarService carService,
			IBrandService brandService,
			IModelService modelService,
			IGenerationService generationService,
			IBodyTypeService bodyTypeService,
			IEngineService engineService,
			IDriveTypeService driveTypeService,
			ITransmissionService transmissionService,
			IAppLogger<HomeController> logger)
		{
			_carService = carService;
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
				var carsQuery = carsList
					.Where(i => (!carsIndex.BrandFilterApplied.HasValue || i.BrandId == carsIndex.BrandFilterApplied)
								&& (!carsIndex.ModelFilterApplied.HasValue || i.ModelId == carsIndex.ModelFilterApplied)
								&& (!carsIndex.GenerationFilterApplied.HasValue || i.GenerationId == carsIndex.GenerationFilterApplied)
								&& (!carsIndex.BodyTypeFilterApplied.HasValue || i.BodyTypeId == carsIndex.BodyTypeFilterApplied)
								&& (!carsIndex.EngineFilterApplied.HasValue || i.EngineId == carsIndex.EngineFilterApplied)
								&& (!carsIndex.DriveTypeFilterApplied.HasValue || i.DriveTypeId == carsIndex.DriveTypeFilterApplied)
								&& (!carsIndex.TransmissionFilterApplied.HasValue || i.TransmissionId == carsIndex.TransmissionFilterApplied))
					.Select(i =>
						new CarViewModel(
							i.CarId,
							i.PriceInitial,
							i.PictureUrl,
							i.Year,
							i.DateArrival,
							i.Odometer,
							i.Description,
							i.Brand,
							i.Model,
							i.Generation,
							i.BodyType,
							i.Engine,
							i.DriveType,
							i.Transmission
					)).ToList();

				var carsResult = new CarsIndexViewModel()
				{
					CarVModels = carsQuery,
					Brands = (await GetBrands()).ToList(),
					Models = (await GetModels()).ToList(),
					Generations = (await GetGenerations()).ToList(),
					BodyTypes = (await GetBodyTypes()).ToList(),
					Engines = (await GetEngines()).ToList(),
					DriveTypes = (await GetDriveTypes()).ToList(),
					Transmissions = (await GetTransmissions()).ToList(),

				};

				return View(carsResult);
			}
			catch (CarNotFoundException ex)
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

		public IActionResult Details(int id)
		{
			var queryCar = _carService.GetCarModel(id);

			CarBasketItem basket = new()
			{
				Count = 1,
				Car = new CarViewModel(
					id,
					queryCar.PriceInitial,
					queryCar.PictureUrl,
					queryCar.Year,
					queryCar.DateArrival,
					queryCar.Odometer,
					queryCar.Description,
					queryCar.Brand,
					queryCar.Model,
					queryCar.Generation,
					queryCar.BodyType,
					queryCar.Engine,
					queryCar.DriveType,
					queryCar.Transmission)
			};
			return View(basket);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private async Task<IEnumerable<SelectListItem>> GetBrands()
		{
			var brands = await _brandService.GetBrandModelsAsync();
			IEnumerable<BrandViewModel> brandsVM = brands.Select(i => new BrandViewModel(i.BrandId, i.Name));

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
			IEnumerable<ModelViewModel> modelsVM = models.Select(i => new ModelViewModel(i.ModelId, i.Name, i.Brand));

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
			IEnumerable<GenerationViewModel> generationsVM = generations.Select(i => new GenerationViewModel(i.GenerationId, i.Name, i.Brand, i.Model));

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
			IEnumerable<BodyTypeViewModel> bodyTypesVM = bodyTypes.Select(i => new BodyTypeViewModel(i.BodyTypeId, i.Name));

			var items = bodyTypesVM
				.Select(bodyType => new SelectListItem { Value = bodyType.BodyTypeId.ToString(), Text = bodyType.Name })
				.OrderBy(bodyType => bodyType.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Body Types", Selected = true };
			items.Insert(0, allItems);

			return items;
		}

		private async Task<IEnumerable<SelectListItem>> GetEngines()
		{
			var engines = await _engineService.GetEngineModelsAsync();
			IEnumerable<EngineViewModel> enginesVM = engines.Select(
				i => new EngineViewModel(
					i.EngineId, 
					i.IdentificationName,
					i.Type,
					i.Capacity,
					i.Power,
					i.Description,
					i.Brand,
					i.Model,
					i.Generation));

			var items = enginesVM
				.Select(engine => new SelectListItem { Value = engine.EngineId.ToString(), Text = engine.IdentificationName})
				.OrderBy(engine => engine.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Engines", Selected = true };
			items.Insert(0, allItems);

			return items;
		}

		private async Task<IEnumerable<SelectListItem>> GetDriveTypes()
		{
			var driveTypes = await _driveTypeService.GetDriveTypeModelsAsync();
			IEnumerable<DriveTypeViewModel> driveTypesVM = driveTypes.Select(
				i => new DriveTypeViewModel(i.DriveTypeId, i.Name));

			var items = driveTypesVM
				.Select(driveType => new SelectListItem { Value = driveType.DriveTypeId.ToString(), Text = driveType.Name})
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
				i => new TransmissionViewModel(i.TransmissionId, i.Name));

			var items = transmissionsVM
				.Select(transmission => new SelectListItem { Value = transmission.TransmissionId.ToString(), Text = transmission.Name})
				.OrderBy(transmission => transmission.Text)
				.ToList();

			var allItems = new SelectListItem() { Value = null, Text = "All Transmissions", Selected = true };
			items.Insert(0, allItems);

			return items;
		}
	}
}