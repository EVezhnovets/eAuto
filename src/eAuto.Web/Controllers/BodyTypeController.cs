using eAuto.Domain.Interfaces;
using eAutoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace eAutoApp.Web.Controllers
{
    public class BodyTypeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBodyTypeService _bodyTypeService;

        public BodyTypeController(IBodyTypeService bodyTypeService ,ILogger<HomeController> logger)
        {
            _logger = logger;
            _bodyTypeService = bodyTypeService;
        }

        public async Task<IActionResult> Index()
        {  
            var result = await _bodyTypeService.GetBodyTypeViewModelsAsync();
            return View(result);
        }
    }
}