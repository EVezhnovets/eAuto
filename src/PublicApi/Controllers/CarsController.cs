using Microsoft.AspNetCore.Mvc;
using PublicApi.Exceptions;
using PublicApi.Services;

namespace PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private CarsApiService _carsService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(CarsApiService carsService, ILogger<CarsController> logger)
        {
            _carsService = carsService;
            _logger = logger;
        }

        [HttpGet("get-all-cars")]
        public IActionResult GetAllCars()
        {
            try
            {
                var allCars = _carsService.GetAllCars();
                return Ok(allCars);
            }
            catch (GenericNotFoundException<CarsApiService> ex)
            {
                _logger.LogError("HTTP \"GET\" \"/api/Cars/get-all-cars\" responded 401");
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                _logger.LogError("HTTP \"GET\" \"/api/Cars/get-all-cars\" responded 401");
                return BadRequest("Unable to load all machines");
            }
        }

        [HttpGet("get-car-by-id/{id}")]
        public IActionResult GetCarById(int id)
        {
            try
            {
                var carResult = _carsService.GetCarById(id);
                return Ok(carResult);
            }
            catch (GenericNotFoundException<CarsApiService> ex)
            {
                _logger.LogError($"HTTP \"GET\" \"/api/Cars/get-car-by-id/{id}\" responded 401");
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                _logger.LogError($"HTTP \"GET\" \"/api/Cars/get-car-by-id/{id}\" responded 401");
                return BadRequest("Unable to load the car");
            }
        }
    }
}