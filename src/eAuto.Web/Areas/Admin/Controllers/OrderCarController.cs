using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAuto.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
    public class OrderCarController : Controller
    {
        private readonly IRepository<OrderCarDataModel> _orderCarRepository;

        [BindProperty]
        public OrderCarViewModel? OrderCarVM { get; set; }
        public OrderCarController(IRepository<OrderCarDataModel> orderCarRepository)
        {
            _orderCarRepository = orderCarRepository;
        }
        public async Task<IActionResult> Index(string status)
        {
            var orderCarList = await _orderCarRepository.GetAllAsync();
            IEnumerable<OrderCarDataModel> resultList = orderCarList;
            switch (status)
            {
                case "inprocess":
                    resultList = orderCarList.Where(u => u.OrderStatus == WebConstants.StatusProcessing);
                    break;
                case "pending":
                    resultList = orderCarList.Where(u => u.OrderStatus == WebConstants.StatusPending);
                    break;
                case "completed":
                    resultList = orderCarList.Where(u => u.OrderStatus == WebConstants.StatusCompleted);
                    break;
                case "canceled":
                    resultList = orderCarList.Where(u => u.OrderStatus == WebConstants.StatusCancelled);
                    break;
                default:
                    break;
            }

            return View(resultList);
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
        [ValidateAntiForgeryToken]
        public IActionResult StartProcessing(int id)
        {
            UpdateStatus(id, WebConstants.StatusProcessing);

            TempData["Success"] = "Order Status Updated Successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteCallProccessing(int id)
        {
            UpdateStatus(id, WebConstants.StatusCompleted);

            TempData["Success"] = "Order Status Updated Successfully.";
            return RedirectToAction("Index"); ;
        }

        [HttpPost]
        [Authorize(Roles = WebConstants.AdminRole + "," + WebConstants.EmployeeRole)]
        [ValidateAntiForgeryToken]
        public IActionResult RejectCall(int id)
        {
            UpdateStatus(id, WebConstants.StatusCancelled);

            TempData["Success"] = "Order Cancelled Successfully.";
            return RedirectToAction("Index");
        }

        private void UpdateStatus(int id, string orderStatus)
        {
            var orderCarFromDb = _orderCarRepository.Get(i => i.OrderCarId == id);

            if (orderCarFromDb != null)
            {
                orderCarFromDb.OrderStatus = orderStatus;
                _orderCarRepository.Update(orderCarFromDb);
            }
        }
    }
}