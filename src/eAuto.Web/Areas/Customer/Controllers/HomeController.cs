using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eAuto.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
    {
		private readonly IAppLogger<HomeController> _logger;

		private readonly ICarService _carService;
		
		public HomeController(
			ICarService carService,
			IAppLogger<HomeController> logger)
		{
			_carService = carService;
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
    }
}