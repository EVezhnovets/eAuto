using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderHeaderRepository _orderHeaderRepository;
        private readonly IRepository<OrderDetailsDataModel> _orderDetailsRepository;
        private readonly UserManager<ApplicationUser> _userRepository;

        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(
            IOrderHeaderRepository orderHeaderRepository, 
            UserManager<ApplicationUser> userManager)
        {
            _orderHeaderRepository = orderHeaderRepository;
            _userRepository = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var orderHeaders = await _orderHeaderRepository.GetAllAsync();
                
            var orderVMList = orderHeaders.Select( i => new OrderVM
            {
                OrderHeader = new OrderHeaderViewModel()
                {
                    Id = i.Id,
                    ApplicationUserId = i.ApplicationUserId,
                    OrderDate = i.OrderDate,
                    ShippingDate = i.ShippingDate,
                    OrderTotal = i.OrderTotal,
                    OrderStatus = i.OrderStatus,
                    PaymentStatus = i.PaymentStatus,
                    TrackingNumber = i.TrackingNumber,
                    Carrier = i.Carrier,
                    PaymentDate = i.PaymentDate,
                    PaymentDueDate = i.PaymentDueDate,
                    SessionId = i.SessionId,
                    PaymentIntentId = i.PaymentIntentId,
                    PhoneNumber = i.PhoneNumber,
                    StreetAddress = i.StreetAddress,
                    City = i.City,
                    Name = i.Name,
                    ApplicationUser = _userRepository.FindByIdAsync(i.ApplicationUserId).Result
                },
            });

            return View(orderVMList);
        }

        public async Task<IActionResult> Details(int orderId)
        {
            var orderHeaderFromDb = _orderHeaderRepository.Get(u => u.Id == orderId);
            var orderHeaderToVM = new OrderHeaderViewModel
            {
                Id = orderHeaderFromDb.Id,
                ApplicationUserId = orderHeaderFromDb.ApplicationUserId,
                OrderDate = orderHeaderFromDb.OrderDate,
                ShippingDate = orderHeaderFromDb.ShippingDate,
                OrderTotal = orderHeaderFromDb.OrderTotal,
                OrderStatus = orderHeaderFromDb.OrderStatus,
                PaymentStatus = orderHeaderFromDb.PaymentStatus,
                TrackingNumber = orderHeaderFromDb.TrackingNumber,
                Carrier = orderHeaderFromDb.Carrier,
                PaymentDate = orderHeaderFromDb.PaymentDate,
                PaymentDueDate = orderHeaderFromDb.PaymentDueDate,
                SessionId = orderHeaderFromDb.SessionId,
                PaymentIntentId = orderHeaderFromDb.PaymentIntentId,
                PhoneNumber = orderHeaderFromDb.PhoneNumber,
                StreetAddress = orderHeaderFromDb.StreetAddress,
                City = orderHeaderFromDb.City,
                Name = orderHeaderFromDb.Name,
                ApplicationUser = _userRepository.FindByIdAsync(orderHeaderFromDb.ApplicationUserId).Result
            };
            var orderDetailFromDb = await _orderDetailsRepository.GetAllAsync(
                predicate: u => u.Id == orderId, include: query => query
                    .Include(e => e.Product));

            OrderVM = new()
            {
                OrderHeader = orderHeaderToVM,
                OrderDetails = orderDetailFromDb
            };
            return View(OrderVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var orderHeaders = _orderHeaderRepository.GetAllAsync(
                include: query => query 
                .Include(e => e.ApplicationUser));
            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}