using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace eAuto.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class MotorOilCatalogController : Controller
	{
		private readonly IAppLogger<MotorOilCatalogController> _logger;

		private readonly IMotorOilService _motorOilService;
		private readonly IProductBrandService _productBrandService;

		public MotorOilCatalogController(
			IMotorOilService motorOilService,
			IProductBrandService productBrandService,
			IAppLogger<MotorOilCatalogController> logger)
		{
			_motorOilService = motorOilService;
			_productBrandService = productBrandService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Index(MotorOilsIndexViewModel motorOilsIndex, int? pageId)
		{
			try
			{
				var iMotorOilsList = await _motorOilService.GetMotorOilModelsAsync();
				var motorOilsList = iMotorOilsList
					.Select(i => new MotorOilViewModel
					{
						MotorOilId = i.MotorOilId,
						Name = i.Name,
						PictureUrl = i.PictureUrl,
						Price = i.Price,
						Viscosity = i.Viscosity,
						Composition = i.Composition,
						Volume = i.Volume,
						ProductBrandId = i.ProductBrandId,
						ProductBrand = i.ProductBrand
					})
					.OrderBy(i => i.MotorOilId)
					.ThenBy(i => i.ProductBrand)
					.ThenBy(i => i.Name);

				var motorOilResult = new MotorOilsIndexViewModel()
				{
					MotorOilVModels = motorOilsList
				};

				return View(motorOilResult);
			}
			catch (MotorOilNotFoundException ex)
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
            var queryOil = _motorOilService.GetMotorOilModel(id);

            MotorOilBasketItemViewModel basket = new()
            {
                Count = 1,
                MotorOil = new MotorOilViewModel(
                    id,
                    queryOil.Name,
                    queryOil.PictureUrl,
                    queryOil.Price,
                    queryOil.Viscosity,
                    queryOil.Composition,
                    queryOil.Volume,
                    queryOil.ProductBrand)
            };
            return View(basket);
        }
    }
}