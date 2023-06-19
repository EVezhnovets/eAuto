using eAuto.Data.Interfaces;
using eAuto.Domain.Interfaces;
using ShoppingCartDataM = eAuto.Data.Interfaces.DataModels.ShoppingCartDataModel;

namespace eAuto.Domain.DomainModels
{
    public sealed class ShoppingCartDomainModel : IShoppingCart
    {
        private readonly IRepository<ShoppingCartDataM>? _shoppingCartRepository;
        private readonly bool _isNew;

        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public string? Product { get; set; }
        public IMotorOil? OilProduct { get; set; }
        public int Count { get; set; }
        public string? ApplicationUserId { get; set; }
        public double Price { get; set; }

        public ShoppingCartDomainModel() { }

        public ShoppingCartDomainModel(IRepository<ShoppingCartDataM> cartRepository)
        {
            _shoppingCartRepository = cartRepository;
            _isNew = true;
        }

        public ShoppingCartDomainModel(
            IRepository<ShoppingCartDataM> cartRepository, 
            IShoppingCart cart)
        {
            _shoppingCartRepository = cartRepository;
            _isNew = true;

            ShoppingCartId = cart.ShoppingCartId;
            ProductId = cart.ProductId;
            Product = cart.Product;
            Count = cart.Count;
            ApplicationUserId = cart.ApplicationUserId;
            Price = cart.Price;
        }

        internal ShoppingCartDomainModel(
            ShoppingCartDataM cartDataModel,
            IRepository<ShoppingCartDataM> cartRepository)
        {
            _shoppingCartRepository = cartRepository;
            ShoppingCartId = cartDataModel.ShoppingCartId;
            ProductId = cartDataModel.ProductId;
            Product = cartDataModel.Product!.Name;
            Count = cartDataModel.Count;
            ApplicationUserId = cartDataModel.ApplicationUserId;
        }

		public void Save()
		{
			var shoppingCartDataModel = GetShoppingCartDataModel();

            if (_isNew)
            {
                var result = _shoppingCartRepository!.Create(shoppingCartDataModel);
                ShoppingCartId = result.ShoppingCartId;
                ApplicationUserId = result.ApplicationUserId;
                ShoppingCartId = result.ShoppingCartId; 
            }
            else
            {
                _shoppingCartRepository!.Update(shoppingCartDataModel);
            }
        }

		public void Delete()
		{
            var shoppingCartModel = GetShoppingCartDataModel();
            if (!_isNew)
            {
                _shoppingCartRepository!.Delete(shoppingCartModel);
            }
		}

        private ShoppingCartDataM GetShoppingCartDataModel()
        {
            var shoppingCartDataM = new ShoppingCartDataM
            {
                ShoppingCartId = ShoppingCartId,
                ApplicationUserId = ApplicationUserId,
                Count = Count,
            };
            return shoppingCartDataM;
		}
	}
}