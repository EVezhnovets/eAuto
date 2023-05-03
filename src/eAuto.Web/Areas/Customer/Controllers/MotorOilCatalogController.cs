using eAuto.Data.Interfaces;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eAuto.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class MotorOilCatalogController : Controller
	{
		private readonly IAppLogger<MotorOilCatalogController> _logger;

		private readonly IMotorOilService _motorOilService;
		private readonly IProductBrandService _productBrandService;
		private readonly IShoppingCartService _shoppingCartService;

		public MotorOilCatalogController(
			IMotorOilService motorOilService,
			IProductBrandService productBrandService,
            IShoppingCartService shoppingCartService,

            IAppLogger<MotorOilCatalogController> logger)
		{
			_motorOilService = motorOilService;
			_productBrandService = productBrandService;
            _shoppingCartService = shoppingCartService;
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
			catch (GenericNotFoundException<MotorOilCatalogController> ex)
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

        public IActionResult Details(int productId)
		{
			var product = _motorOilService.GetMotorOilModel(productId);

			ShoppingCartViewModel cart = new()
			{
				Count = 1,
				ProductId = productId,
				Product = new MotorOilViewModel(
                    productId,
                    product.Name,
                    product.PictureUrl,
                    product.Price,
                    product.Viscosity,
                    product.Composition,
                    product.Volume,
                    product.ProductBrand
                    )
			};

			return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCartViewModel shoppingCart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

			var oilFromDb = _motorOilService.GetMotorOilModel(shoppingCart.ProductId);

			ShoppingCartDomainModel shoppingCartToService = new()
			{
				ShoppingCartId = shoppingCart.ShoppingCartId,
				ProductId = shoppingCart.ProductId,
				Product = default,
				Count = shoppingCart.Count,
				ApplicationUserId = shoppingCart.ApplicationUserId,
				Price = oilFromDb.Price
			};

			var cartFromDb = _shoppingCartService.GetFirstOrDefauttShoppingCart(claim, shoppingCartToService);

            return RedirectToAction(nameof(Index));
        }
    }
}