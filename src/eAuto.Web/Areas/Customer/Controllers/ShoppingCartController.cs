using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eAuto.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IAppLogger<ShoppingCartController> _logger;
        private readonly IMotorOilService _motorOilService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartIndexViewModel ShoppingCartIndex { get; set; }
        public int OrderTotal { get; set; }

        public ShoppingCartController(
            IAppLogger<ShoppingCartController> logger,
            IMotorOilService motorOilService,
            IShoppingCartService shoppingCartService)
        {
            _logger = logger;
            _motorOilService = motorOilService;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var motorOils = await _motorOilService.GetMotorOilModelsAsync();

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var shoppingCartListDomain = await _shoppingCartService.GetShoppingCartModelsAsync(claim);

            ShoppingCartIndex = new ShoppingCartIndexViewModel()
            {
                ShoppingCartList = shoppingCartListDomain
                .Select(i => new ShoppingCartViewModel()
                {
                    ShoppingCartId = i.ShoppingCartId,
                    ProductId = i.ProductId,
                    Product = new MotorOilViewModel()
                    {
                        MotorOilId = i.OilProduct.MotorOilId,
                        PictureUrl = i.OilProduct.PictureUrl,
                        Price = i.OilProduct.Price,
                        Name = i.OilProduct.Name,
                        Viscosity = i.OilProduct.Viscosity,
                        Composition = i.OilProduct.Composition,
                        Volume = i.OilProduct.Volume,
                        ProductBrandId = i.OilProduct.ProductBrandId,
                        ProductBrand = i.OilProduct.ProductBrand
                    },
                    Count = i.Count,
                    ApplicationUserId = i.ApplicationUserId,
                }).ToList(),
            };

            foreach (var cart in ShoppingCartIndex.ShoppingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartIndex.CartTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartIndex);
        }

        public IActionResult Summary()
        {
            return View();
        }

        public IActionResult Plus(int cartId)
        {
            var cart = _shoppingCartService.GetShoppingCartModel(cartId);
            _shoppingCartService.IncrementCount(cart);

            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _shoppingCartService.GetShoppingCartModel(cartId);
            if(cart.Count <= 1)
            {
                _shoppingCartService.RemoveShoppingCart(cart);
            }
            else
            {
                _shoppingCartService.DecrementCount(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _shoppingCartService.GetShoppingCartModel(cartId);

            _shoppingCartService.RemoveShoppingCart(cart);
            return RedirectToAction("Index");
        }
    }
}