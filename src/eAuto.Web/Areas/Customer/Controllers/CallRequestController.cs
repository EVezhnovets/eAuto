using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eAuto.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class CallRequestController : Controller
	{
		private readonly IAppLogger<CallRequestController> _logger;
        private readonly IRepository<OrderCarDataModel> _orderCarRepository;
        private readonly UserManager<ApplicationUser> _userRepository;

        

		public CallRequestController(
            IRepository<OrderCarDataModel> orderCarRepository,
            IAppLogger<CallRequestController> logger,
            UserManager<ApplicationUser> userRepository)
		{
			_orderCarRepository = orderCarRepository;
			_userRepository = userRepository;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Index(int id)
		{
            OrderCarViewModel orderCarView = new();
			return View(orderCarView);
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(OrderCarViewModel orderCarView)
		{
            if (ModelState.IsValid)
			{
                var orderCallToDb = new OrderCarDataModel()
                {
                    OrderDate = DateTime.UtcNow,
                    OrderStatus = WebConstants.StatusPending,
                    Message = orderCarView.Message,
                    PhoneNumber = orderCarView.PhoneNumber,
                    Name = orderCarView.Name
                };
                var claimIdentity = (ClaimsIdentity)User.Identity!;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (claim != null)
                {
                    orderCallToDb.ApplicationUserId = _userRepository.GetUserId(User);
                }
                else
                {
                    orderCallToDb.ApplicationUserId = orderCarView.Name + Guid.NewGuid().ToString();
                }
                _orderCarRepository.Create(orderCallToDb);
            }
			
			return RedirectToAction("Index", "CarsCatalog");
		}
    }
}