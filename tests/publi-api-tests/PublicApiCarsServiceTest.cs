namespace publi_api_tests
{
    public class PublicApiCarsServiceTest
    {
        private EAutoContext _context;
        private CarsApiService _carsApiService;
        private Repository<CarDataModel>? _repository;

        private static DbContextOptions<EAutoContext> eAutoContextOptions = new DbContextOptionsBuilder<EAutoContext>()
            .UseInMemoryDatabase(databaseName: "CarsDbTest")
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
        public void GetAllCars_Return_200_Test()
        {
            var result = _carsApiService.GetAllCars();
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.GreaterThan(0));
        }
        
        [Test, Order(2)]
        public void GetCarById_One_Return_200_Test()
        {
            var result = _carsApiService.GetCarById(1);
            Assert.That(result.CarId, Is.EqualTo(1));
            Assert.That(result.PriceInitial, Is.EqualTo(60000));
        }

        [Test, Order(3)]
        public void GetCarById_OutOfRange_Exception_Test()
        {
            Assert.That(() => _carsApiService.GetCarById(99),
                Throws.Exception.TypeOf<GenericNotFoundException<CarsApiService>>().With.Message.EqualTo("Car not found"));
        }
    }
}