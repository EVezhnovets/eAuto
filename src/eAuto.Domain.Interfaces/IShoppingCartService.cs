using System.Security.Claims;

namespace eAuto.Domain.Interfaces
{
    public interface IShoppingCartService<T> where T : class
    {
		IShoppingCart GetShoppingCartModel(int id);
        Task<IEnumerable<IShoppingCart>> GetShoppingCartModelsAsync(Claim claim);
		Task<IEnumerable<IShoppingCart>> GetShoppingCartModelsAsync(string applicationUserId);
		IShoppingCart CreateShoppingCartDomainModel();
		IShoppingCart CreateShoppingCartDomainModel(IShoppingCart cart);
        IShoppingCart GetFirstOrDefauttShoppingCart(Claim claim, IShoppingCart cart);
        void RemoveShoppingCart(IShoppingCart cart);
        void RemoveRangeShoppingCart(IEnumerable<IShoppingCart> list);
        void IncrementCount(IShoppingCart cart);
        void DecrementCount(IShoppingCart cart);
    }
}