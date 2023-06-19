using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Claims;

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
        public OrderVM? OrderVM { get; set; }
        public OrderController(
            IOrderHeaderRepository orderHeaderRepository,
            IRepository<OrderDetailsDataModel> orderDetailsRepository,
            UserManager<ApplicationUser> userManager)
        {
            _orderHeaderRepository = orderHeaderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _userRepository = userManager;
        }
        public async Task<IActionResult> Index(string status)
        {
            IEnumerable<OrderVM> orderVMListForView;
            if (User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.EmployeeRole) )
            {
                var orderHeaders = await _orderHeaderRepository.GetAllAsync();
                orderVMListForView = orderHeaders.Select(i => new OrderVM
                {
                    OrderHeader = new OrderHeaderViewModel()
                    {
                        Id = i.OrderId,
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
                        ApplicationUser = _userRepository.FindByIdAsync(i.ApplicationUserId!).Result
                    },
                });
            } 
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity!;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var orderHeaders = await _orderHeaderRepository.GetAllAsync(a => a.ApplicationUserId == claim!.Value);
                orderVMListForView = orderHeaders.Select(i => new OrderVM
                {
                    OrderHeader = new OrderHeaderViewModel()
                    {
                        Id = i.OrderId,
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
                        ApplicationUser = _userRepository.FindByIdAsync(i.ApplicationUserId!).Result
                    },
                });
            }

                switch (status)
            {
                case "pending":
                    orderVMListForView = orderVMListForView.Where(u => u.OrderHeader!.PaymentStatus == WebConstants.PaymentStatusPending);
                    break;
                case "inprocess":
                    orderVMListForView = orderVMListForView.Where(u => u.OrderHeader!.OrderStatus == WebConstants.StatusProcessing);
                    break;
                case "completed":
                    orderVMListForView = orderVMListForView.Where(u => u.OrderHeader!.OrderStatus == WebConstants.StatusShipped);
                    break;
                case "approved":
                    orderVMListForView = orderVMListForView.Where(u => u.OrderHeader!.OrderStatus == WebConstants.StatusApproved);
                    break;
                default:
                    break;
            }
            

            return View(orderVMListForView);
        }

        public async Task<IActionResult> Details(int orderId)
        {
            var orderHeaderFromDb = _orderHeaderRepository.Get(u => u.OrderId == orderId);
            var orderHeaderToVM = new OrderHeaderViewModel
            {
                Id = orderHeaderFromDb!.OrderId,
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
                ApplicationUser = _userRepository.FindByIdAsync(orderHeaderFromDb.ApplicationUserId!).Result
            };
            var orderDetailFromDb = await _orderDetailsRepository.GetAllAsync(
                predicate: u => u.OrderId == orderId, include: query => query
                    .Include(e => e.Product!));

            OrderVM = new()
            {
                OrderHeader = orderHeaderToVM,
                OrderDetails = orderDetailFromDb
            };
            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFromDb = _orderHeaderRepository.Get(u => u.OrderId == OrderVM!.OrderHeader!.Id);

            orderHeaderFromDb!.Name = OrderVM!.OrderHeader!.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;

            if (OrderVM.OrderHeader.Carrier != null)
            {
                orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (OrderVM.OrderHeader.TrackingNumber != null)
            {
                orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            _orderHeaderRepository.Update(orderHeaderFromDb);
            
            TempData["Success"] = "Order Details Updated Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = orderHeaderFromDb.OrderId });
        }
        
        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
        [ValidateAntiForgeryToken]
        public IActionResult StartProcessing()
        {
            _orderHeaderRepository.UpdateStatus(OrderVM!.OrderHeader!.Id, WebConstants.StatusProcessing);

            TempData["Success"] = "Order Status Updated Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = OrderVM!.OrderHeader!.Id });
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
        [ValidateAntiForgeryToken]
        public IActionResult ShipOrder()
        {
            var orderHeaderFromDb = _orderHeaderRepository.Get(u => u.OrderId == OrderVM!.OrderHeader!.Id);
            
            orderHeaderFromDb!.TrackingNumber = OrderVM!.OrderHeader!.TrackingNumber;
            orderHeaderFromDb.Carrier = OrderVM!.OrderHeader!.Carrier;
            orderHeaderFromDb.OrderStatus = WebConstants.StatusShipped;
            orderHeaderFromDb.ShippingDate = DateTime.Now;

            if (orderHeaderFromDb.PaymentStatus == WebConstants.PaymentStatusDelayedPayment)
            {
                orderHeaderFromDb.PaymentDueDate = DateTime.Now.AddDays(30);
            }
            _orderHeaderRepository.Update(orderHeaderFromDb);

            TempData["Success"] = "Order Shipped Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = OrderVM!.OrderHeader!.Id }); ;
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder()
        {
            var orderHeaderFromDb = _orderHeaderRepository.Get(u => u.OrderId == OrderVM!.OrderHeader!.Id);

            if (orderHeaderFromDb!.PaymentStatus == WebConstants.PaymentStatusApproved)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeaderFromDb.PaymentIntentId
                };

                var service = new RefundService();
                Refund refund = service.Create(options);

                _orderHeaderRepository.UpdateStatus(orderHeaderFromDb.OrderId, WebConstants.StatusCancelled, WebConstants.StatusRefunded);
            }
            else
            {
                _orderHeaderRepository.UpdateStatus(orderHeaderFromDb.OrderId, WebConstants.StatusCancelled, WebConstants.StatusRefunded);

            }

            TempData["Success"] = "Order Cancelled Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = OrderVM!.OrderHeader!.Id });
        }   
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var orderHeaders = _orderHeaderRepository.GetAllAsync(
                include: query => query 
                .Include(e => e!.ApplicationUser!));
            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}