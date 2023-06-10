using eAuto.Data;
using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace eAuto.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IAppLogger<ShoppingCartController> _logger;
        private readonly IMotorOilService _motorOilService;
        private readonly IEmailSender _emailSender;
        private readonly IShoppingCartService<ShoppingCartDataModel> _shoppingCartService;
        private readonly IOrderHeaderRepository _orderHeaderRepository;
        private readonly IRepository<OrderDetailsDataModel> _orderDetailsRepository;
        private readonly UserManager<ApplicationUser> _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public ShoppingCartIndexViewModel ShoppingCartIndex { get; set; }
        public int OrderTotal { get; set; }

        public ShoppingCartController(
            IAppLogger<ShoppingCartController> logger,
            IMotorOilService motorOilService,
            IEmailSender emailSender,
			IShoppingCartService<ShoppingCartDataModel> shoppingCartService,
			IOrderHeaderRepository orderHeaderRepository,
			IRepository<OrderDetailsDataModel> orderDetailsRepository,
			UserManager<ApplicationUser> userRepository,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _motorOilService = motorOilService;
            _emailSender = emailSender;
            _shoppingCartService = shoppingCartService;
            _orderHeaderRepository = orderHeaderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var motorOils = await _motorOilService.GetMotorOilModelsAsync();

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var shoppingCartListDomain = await _shoppingCartService.GetShoppingCartModelsAsync(claim);
            var orderHeader = _orderHeaderRepository.Get(u => u.ApplicationUserId == claim.Value);
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
                OrderHeader = new()
            };

            foreach (var cart in ShoppingCartIndex.ShoppingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartIndex.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

		    return View(ShoppingCartIndex);
        }

        public async Task<IActionResult> Summary()
        {
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
                OrderHeader = new()
            };


            ShoppingCartIndex.OrderHeader.ApplicationUser = await _userRepository.GetUserAsync(User);

            ShoppingCartIndex.OrderHeader.Name = ShoppingCartIndex.OrderHeader.ApplicationUser.FirstName;
            ShoppingCartIndex.OrderHeader.PhoneNumber = ShoppingCartIndex.OrderHeader.ApplicationUser.Phone;
            ShoppingCartIndex.OrderHeader.StreetAddress = ShoppingCartIndex.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartIndex.OrderHeader.City = ShoppingCartIndex.OrderHeader.ApplicationUser.City;

            foreach (var cart in ShoppingCartIndex.ShoppingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartIndex.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            if (ShoppingCartIndex.ShoppingCartList.Count() == 0)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            else
            {
                return View(ShoppingCartIndex);

            }
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SummaryPOST()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var shoppingCartListDomain = await _shoppingCartService.GetShoppingCartModelsAsync(claim);
            ShoppingCartIndex.ShoppingCartList = shoppingCartListDomain
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
                }).ToList();

            ShoppingCartIndex.OrderHeader.PaymentStatus = WebConstants.PaymentStatusPending;
            ShoppingCartIndex.OrderHeader.OrderStatus = WebConstants.StatusPending;
            ShoppingCartIndex.OrderHeader.OrderDate = DateTime.UtcNow;
            ShoppingCartIndex.OrderHeader.ApplicationUserId = claim.Value;

            foreach (var cart in ShoppingCartIndex.ShoppingCartList)
            {
                cart.Price = cart.Product.Price;
                ShoppingCartIndex.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            var orderHeaderToDb = new OrderHeaderDataModel
            {
                ApplicationUserId = ShoppingCartIndex.OrderHeader.ApplicationUserId,
                OrderDate = ShoppingCartIndex.OrderHeader.OrderDate,
                ShippingDate = ShoppingCartIndex.OrderHeader.OrderDate.AddDays(7),
                OrderTotal = ShoppingCartIndex.OrderHeader.OrderTotal,
                OrderStatus = ShoppingCartIndex.OrderHeader.OrderStatus,
                PaymentStatus = ShoppingCartIndex.OrderHeader.PaymentStatus,
                TrackingNumber = ShoppingCartIndex.OrderHeader.TrackingNumber,
                Carrier = ShoppingCartIndex.OrderHeader.Carrier,
                PaymentDate = ShoppingCartIndex.OrderHeader.PaymentDate,
                PaymentDueDate = ShoppingCartIndex.OrderHeader.PaymentDueDate,
                SessionId = ShoppingCartIndex.OrderHeader.SessionId,
                PaymentIntentId = ShoppingCartIndex.OrderHeader.PaymentIntentId,
                PhoneNumber = ShoppingCartIndex.OrderHeader.PhoneNumber,
                StreetAddress = ShoppingCartIndex.OrderHeader.StreetAddress,
                City = ShoppingCartIndex.OrderHeader.City,
                Name = ShoppingCartIndex.OrderHeader.Name
            };

            _orderHeaderRepository.Create(orderHeaderToDb);

            ShoppingCartIndex.OrderHeader.Id = orderHeaderToDb.Id;

            foreach (var cart in ShoppingCartIndex.ShoppingCartList)
            {
                var orderDetails = new OrderDetailsDataModel()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartIndex.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };

                _orderDetailsRepository.Create(orderDetails);
            }

			//stripe settings
			var domain = "https://localhost:7261/";
			var options = new SessionCreateOptions
			{
                PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                LineItems = new List<SessionLineItemOptions>(),
				Mode = "payment",
				SuccessUrl = domain + $"Customer/ShoppingCart/OrderConfirmation?id={ShoppingCartIndex.OrderHeader.Id}",
				CancelUrl = domain + "Customer/ShoppingCart/Index",
			};

			foreach (var item in ShoppingCartIndex.ShoppingCartList)
			{
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name
						},
					},
					Quantity = item.Count,
				};
				options.LineItems.Add(sessionLineItem);
			}

			var service = new SessionService();
			Session session = service.Create(options);
			_orderHeaderRepository.UpdateStripePaymentId(
				ShoppingCartIndex.OrderHeader.Id,
				session.Id,
				session.PaymentIntentId);

			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);
		}

		public async Task<IActionResult> OrderConfirmation(int id)
        {
			OrderHeaderDataModel orderHeader = _orderHeaderRepository.Get(u => u.Id == id);
            orderHeader.ApplicationUser = await _userRepository.GetUserAsync(User);
            var service = new SessionService();
			Session session = service.Get(orderHeader.SessionId);

			//check the stripe status
			if (session.PaymentStatus.ToLower() == "paid")
			{
                _orderHeaderRepository.UpdateStripePaymentId(id, orderHeader.SessionId, session.PaymentIntentId);
				_orderHeaderRepository.UpdateStatus(id, WebConstants.StatusApproved, WebConstants.PaymentStatusApproved);
			}

            _emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "eAuto Center - New Order", $"<p>New Order {orderHeader.Id} Created</p>");

            var list = await _shoppingCartService.GetShoppingCartModelsAsync(orderHeader.ApplicationUserId);

			_shoppingCartService.RemoveRangeShoppingCart(list);

			return View(id);
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