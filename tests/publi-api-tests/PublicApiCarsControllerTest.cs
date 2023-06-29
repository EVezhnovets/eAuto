namespace publi_api_tests
{
    public class PublicApiCarsControllerTest
    {
        private EAutoContext _context;
        private CarsApiService _carsApiService;
        private Repository<CarDataModel>? _repository;
        private CarsController _carsController;

        private static DbContextOptions<EAutoContext> eAutoContextOptions = new DbContextOptionsBuilder<EAutoContext>()
            .UseInMemoryDatabase(databaseName: "CarsDbControllerTest")
            .Options;


        [OneTimeSetUp]
        public void Setup()
        {

            _context = new EAutoContext(eAutoContextOptions);
            _context.Database.EnsureCreated();

            _repository = new Repository<CarDataModel>(_context);

            SeedEAutoContext.SeedDb(_context);

            _carsApiService = new CarsApiService(_context, _repository, new NullLogger<CarsApiService>());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [Test, Order(1)]
        public void HttpGet_GetAllCars_Test()
        {
            _carsController = new CarsController(_carsApiService, new NullLogger<CarsController>());
            var result = _carsController.GetAllCars();

            var castResult = (result as OkObjectResult).Value as List<CarDataModel>;

            Assert.That(castResult!.Any(), Is.True);
            Assert.That(castResult!.First().CarId, Is.EqualTo(1));
            Assert.That(castResult!.Count, Is.EqualTo(8));
        }

        [Test, Order(2)]
        public void HttpGet_GetCar_ById_Two_Test()
        {
            _carsController = new CarsController(_carsApiService, new NullLogger<CarsController>());
            var result = _carsController.GetCarById(2);

            var castResult = (result as OkObjectResult).Value as CarDataModel;

            Assert.That(castResult.CarId, Is.EqualTo(2));
        }

        [Test, Order(3)]
        public void HttpGet_GetCar_ById_Outofrange_Test()
        {
            _carsController = new CarsController(_carsApiService, new NullLogger<CarsController>());
            var result = _carsController.GetCarById(99);

            var castResult = (result as BadRequestObjectResult);

            Assert.That(castResult.Value.ToString(), Is.EqualTo("Car not found"));
        }
    }
}