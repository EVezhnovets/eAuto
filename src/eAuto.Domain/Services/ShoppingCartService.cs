using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Domain.DomainModels;
using eAuto.Domain.Interfaces;
using eAuto.Domain.Interfaces.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Security.Claims;

namespace eAuto.Domain.Services
{
    public sealed class ShoppingCartService : IShoppingCartService<ShoppingCartDataModel>
	{
        private readonly IRepository<ShoppingCartDataModel> _shoppingCartRepository;
        private readonly IRepository<MotorOilDataModel> _motorOilRepository;
        private readonly IAppLogger<ShoppingCartService> _logger;

        public ShoppingCartService(
            IRepository<ShoppingCartDataModel> shoppingCartRepository,
            IRepository<MotorOilDataModel> motorOilRepository,
            IAppLogger<ShoppingCartService> logger)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _motorOilRepository = motorOilRepository;
            _logger = logger;
        }

        public IShoppingCart GetShoppingCartModel(int id)
        {
            var shoppingCartDataModel = GetShoppingCart(id);

            if (shoppingCartDataModel == null)
            {
				var exception = new GenericNotFoundException<ShoppingCartDataModel>($"{nameof(ShoppingCartDataModel)} not found");
				_logger.LogError(exception, exception.Message);
			}

            var shoppingCartViewModel = new ShoppingCartDomainModel(shoppingCartDataModel, _shoppingCartRepository);
            return shoppingCartViewModel;
        }

        public async Task<IEnumerable<IShoppingCart>> GetShoppingCartModelsAsync()
        {
            var shoppingCartEntities = await _shoppingCartRepository
                .GetAllAsync(
                include: query => query
                .Include(e => e.Product)
                );

            if (shoppingCartEntities == null)
            {
				var exception = new GenericNotFoundException<ShoppingCartDataModel>($"{nameof(ShoppingCartDataModel)} not found");
				_logger.LogError(exception, exception.Message);
			}

            var shoppingCartViewModels = shoppingCartEntities
                .Select(i => new ShoppingCartDomainModel()
                {
                    ShoppingCartId = i.ShoppingCartId,
                    ProductId = i.ProductId,
                    Product = i.Product.Name,
                    Count = i.Count,
                    ApplicationUserId = i.ApplicationUserId,
                    Price = i.Price
                }).ToList();
            var shoppingCartModels = shoppingCartViewModels.Cast<IShoppingCart>();
            return shoppingCartModels;
        }
 
        public IShoppingCart CreateShoppingCartDomainModel()
        {
            var shoppingCart = new ShoppingCartDomainModel(_shoppingCartRepository);
            return shoppingCart;
        }

        public IShoppingCart CreateShoppingCartDomainModel(IShoppingCart cart)
        {
            var shoppingCart = new ShoppingCartDomainModel(_shoppingCartRepository, cart);
            return shoppingCart;
        }

        public ShoppingCartDataModel GetShoppingCart(int shoppingCartId)
        {
            var shoppingCart = _shoppingCartRepository
                .Get(
                    predicate: bt => bt.ShoppingCartId == shoppingCartId, include: query => query
                        .Include(g => g.Product)
                );

            if (shoppingCart == null)
            {
                var exception = new GenericNotFoundException<ShoppingCartDataModel>($"{nameof(ShoppingCartDataModel)} not found");
                _logger.LogError(exception, exception.Message);
            }
            return shoppingCart;
        }

        public async Task<IEnumerable<IShoppingCart>> GetShoppingCartModelsAsync(Claim claim)
        {
            var cartList = await _shoppingCartRepository.GetAllAsync(
                predicate: c => c.ApplicationUserId == claim.Value,
                include: query => query.Include(i => i.Product)
                                       .Include(i => i.Product.ProductBrand));
                
            var responseCart = cartList.Select(i => 
            new ShoppingCartDomainModel()
            {
                ShoppingCartId= i.ShoppingCartId,
                ProductId = i.ProductId,
                OilProduct = new MotorOilDomainModel()
                {
                    MotorOilId = i.Product.MotorOilDataModelId,
                    Name = i.Product.Name,
                    PictureUrl = i.Product.PictureUrl,
                    Price = i.Product.Price,
                    Viscosity = i.Product.Viscosity,
                    Composition = i.Product.Composition,
                    Volume = i.Product.Volume,
                    ProductBrandId = i.Product.ProductBrandId,
                    ProductBrand = i.Product.ProductBrand.Name
                },
                Count = i.Count,
                ApplicationUserId = i.ApplicationUserId,
                Price = i.Price
            }).ToList();

            return responseCart;
        }
        public IShoppingCart GetFirstOrDefauttShoppingCart(Claim claim, IShoppingCart cart)
        {
            ShoppingCartDataModel newCartForDb = new()
            {
                ProductId = cart.ProductId,
                Count = cart.Count,
                ApplicationUserId = claim.Value,
                Price = cart.Price,
            };
                
            var cartFromDB = _shoppingCartRepository.Get(
                predicate: c => c.ApplicationUserId == claim.Value && c.ProductId == cart.ProductId);

            if (cartFromDB == null)
            {
                var newCart = _shoppingCartRepository.Create(newCartForDb);
                return cart;
            }
            else
            {
                var incrementedCart = IncrementCount(cartFromDB, cart.Count);
                _shoppingCartRepository.Update(incrementedCart);
                return cart;
            }
        }

        public void RemoveShoppingCart(IShoppingCart cart)
        {
            var cartForDelete = _shoppingCartRepository.Get(
                predicate: c => c.ShoppingCartId == cart.ShoppingCartId
                );
            _shoppingCartRepository.Delete(cartForDelete);
        }

		public void RemoveRangeShoppingCart(IEnumerable<IShoppingCart> list)
		{
            var listForDelete = list.Select(i => new ShoppingCartDataModel()
            {
                ShoppingCartId = i.ShoppingCartId,
                Count = i.Count,
                ApplicationUserId = i.ApplicationUserId,
                ProductId = i.ProductId,
                Product = _motorOilRepository.Get(
                    predicate: c => c.MotorOilDataModelId == i.ProductId),
            });
            _shoppingCartRepository.DeleteRange(listForDelete);
		}

		public void IncrementCount(IShoppingCart cart)
        {
            var cartIncluded = _shoppingCartRepository.Get(
                predicate: c => c.ShoppingCartId == cart.ShoppingCartId,
                include: query => query.Include(e => e.Product));

            var cartForDb = new ShoppingCartDataModel()
            {
                ShoppingCartId = cart.ShoppingCartId,
                ProductId = cart.ProductId,
                Product = cartIncluded.Product,
                Count = cart.Count,
                ApplicationUserId = cart.ApplicationUserId,
                Price = cart.Price,
            };
            var newCart = IncrementCount(cartForDb, 1);
            _shoppingCartRepository.Update(newCart);
        }

        public void DecrementCount(IShoppingCart cart)
        {
            var cartIncluded = _shoppingCartRepository.Get(
                predicate: c => c.ShoppingCartId == cart.ShoppingCartId,
                include: query => query.Include(e => e.Product));

            var cartForDb = new ShoppingCartDataModel()
            {
                ShoppingCartId = cart.ShoppingCartId,
                ProductId = cart.ProductId,
                Product = cartIncluded.Product,
                Count = cart.Count,
                ApplicationUserId = cart.ApplicationUserId,
                Price = cart.Price,
            };
            var newCart = DecrementCount(cartForDb, 1);
            _shoppingCartRepository.Update(newCart);
        }

        private static ShoppingCartDataModel IncrementCount(ShoppingCartDataModel cart, int count)
        {
            cart.Count += count;
            
            return cart;  
        }

        private static ShoppingCartDataModel DecrementCount(ShoppingCartDataModel cart, int count)
        {
            cart.Count -= count;

            return cart;
        }

		public async Task<IEnumerable<IShoppingCart>> GetShoppingCartModelsAsync(string applicationUserId)
		{
            var list = await _shoppingCartRepository.GetAllAsync(
                predicate: u => u.ApplicationUserId == applicationUserId);

            var listDomain = list.Select(i => new ShoppingCartDomainModel()
            {
                ApplicationUserId = i.ApplicationUserId,
                ShoppingCartId = i.ShoppingCartId,
                ProductId = i.ProductId,
                Count = i.Count,
                Price = i.Price,
            }).ToList();

            return listDomain;
		}
	}
}