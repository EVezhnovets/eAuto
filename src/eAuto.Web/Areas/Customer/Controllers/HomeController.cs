using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Domain.Services;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
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
		private readonly IDriveTypeService _driveTypeService;
		private readonly ITransmissionService _transmissionService;

		public HomeController(
			ICarService carService,
			IBrandService brandService,
			IModelService modelService,
			IGenerationService generationService,
			IBodyTypeService bodyTypeService,
			IDriveTypeService driveTypeService,
			ITransmissionService transmissionService,
			IAppLogger<HomeController> logger)
		{
			_carService = carService;
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
						Description = i.Description,
						BrandId = i.BrandId,
						EngineIdentificationName = i.EngineIdentificationName,
						EngineCapacity = i.EngineCapacity,
						EngineFuelType = i.EngineFuelType,
						EnginePower = i.EnginePower,
						Brand = i.Brand,
						ModelId = i.ModelId,
						Model = i.Model,
						GenerationId = i.GenerationId,
						Generation = i.Generation,
						BodyTypeId = i.BodyTypeId,
						BodyType = i.BodyType,
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
							i.EngineIdentificationName,
							i.EngineCapacity,
							i.EngineFuelType,
							i.EnginePower,
							i.Brand,
							i.Model,
							i.Generation,
							i.BodyType,
							i.DriveType,
							i.Transmission
					)).ToList();

				var carsResult = new CarsIndexViewModel()
				{
					CarVModels = carsQuery,
					Brands = new List<SelectListItem>(),
					Models = new List<SelectListItem>(),
					Generations = new List<SelectListItem>(),
					BodyTypes = new List<SelectListItem>(),
					DriveTypes = new List<SelectListItem>(),
					Transmissions = new List<SelectListItem>()

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
					queryCar.EngineIdentificationName,
					queryCar.EngineCapacity,
					queryCar.EngineFuelType,
					queryCar.EnginePower,
					queryCar.Brand,
					queryCar.Model,
					queryCar.Generation,
					queryCar.BodyType,
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

		#region API
		[HttpGet]
		public async Task<ActionResult> GetBrands()
		{
			var brands = await _brandService.GetBrandModelsAsync();
			var query = brands
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.BrandId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetBodyTypes()
		{
			var bodyTypes = await _bodyTypeService.GetBodyTypeModelsAsync();
			var query = bodyTypes
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.BodyTypeId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetDriveTypes()
		{
			var driveTypes = await _driveTypeService.GetDriveTypeModelsAsync();
			var query = driveTypes
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.DriveTypeId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetTransmissions()
		{
			var transmissions = await _transmissionService.GetTransmissionModelsAsync();
			var query = transmissions
				.OrderBy(m => m.Name)
				.Select(m => new SelectListItem
				{
					Value = m.TransmissionId.ToString(),
					Text = m.Name
				});
			return Json(query);
		}

		[HttpGet]
		public async Task<ActionResult> GetModels(int BrandFilterApplied)
		{
			if (BrandFilterApplied != null)
			{
				var modelsSel = await _modelService.GetModelModelsAsync();
				var query = modelsSel
					.Where(m => m.BrandId == BrandFilterApplied)
					.OrderBy(m => m.Name)
					.Select(m => new SelectListItem
					{
						Value = m.ModelId.ToString(),
						Text = m.Name
					});
				return Json(query);
			}
			return null;
		}

		[HttpGet]
		public async Task<ActionResult> GetGenerations(int ModelFilterApplied)
		{
			if (ModelFilterApplied != null)
			{
				var generationsSel = await _generationService.GetGenerationModelsAsync();
				var query = generationsSel
					.Where(m => m.ModelId == ModelFilterApplied)
					.OrderBy(m => m.Name)
					.Select(m => new SelectListItem
					{
						Value = m.GenerationId.ToString(),
						Text = m.Name
					});
				return Json(query);
			}
			return null;
		}
		#endregion


		//private async Task<IEnumerable<SelectListItem>> GetBodyTypes()
		//{
		//	var bodyTypes = await _bodyTypeService.GetBodyTypeModelsAsync();
		//	IEnumerable<BodyTypeViewModel> bodyTypesVM = bodyTypes.Select(i => new BodyTypeViewModel(i.BodyTypeId, i.Name));

		//	var items = bodyTypesVM
		//		.Select(bodyType => new SelectListItem { Value = bodyType.BodyTypeId.ToString(), Text = bodyType.Name })
		//		.OrderBy(bodyType => bodyType.Text)
		//		.ToList();

		//	var allItems = new SelectListItem() { Value = null, Text = "All Body Types", Selected = true };
		//	items.Insert(0, allItems);

		//	return items;
		//}

		//private async Task<IEnumerable<SelectListItem>> GetDriveTypes()
		//{
		//	var driveTypes = await _driveTypeService.GetDriveTypeModelsAsync();
		//	IEnumerable<DriveTypeViewModel> driveTypesVM = driveTypes.Select(
		//		i => new DriveTypeViewModel(i.DriveTypeId, i.Name));

		//	var items = driveTypesVM
		//		.Select(driveType => new SelectListItem { Value = driveType.DriveTypeId.ToString(), Text = driveType.Name})
		//		.OrderBy(driveType => driveType.Text)
		//		.ToList();

		//	var allItems = new SelectListItem() { Value = null, Text = "All Drive Types", Selected = true };
		//	items.Insert(0, allItems);

		//	return items;
		//}

		//private async Task<IEnumerable<SelectListItem>> GetTransmissions()
		//{
		//	var transmissions = await _transmissionService.GetTransmissionModelsAsync();
		//	IEnumerable<TransmissionViewModel> transmissionsVM = transmissions.Select(
		//		i => new TransmissionViewModel(i.TransmissionId, i.Name));

		//	var items = transmissionsVM
		//		.Select(transmission => new SelectListItem { Value = transmission.TransmissionId.ToString(), Text = transmission.Name})
		//		.OrderBy(transmission => transmission.Text)
		//		.ToList();

		//	var allItems = new SelectListItem() { Value = null, Text = "All Transmissions", Selected = true };
		//	items.Insert(0, allItems);

		//	return items;
		//}
	}
}