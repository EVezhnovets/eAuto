using System.Security.Claims;

namespace eAuto.Domain.Interfaces
{
    public interface IShoppingCartService
    {
		IShoppingCart GetShoppingCartModel(int id);
        Task<IEnumerable<IShoppingCart>> GetShoppingCartModelsAsync(Claim claim);
		IShoppingCart CreateShoppingCartDomainModel();
		IShoppingCart CreateShoppingCartDomainModel(IShoppingCart cart);
        IShoppingCart GetFirstOrDefauttShoppingCart(Claim claim, IShoppingCart cart);
        void RemoveShoppingCart(IShoppingCart cart);
        void IncrementCount(IShoppingCart cart);
        void DecrementCount(IShoppingCart cart);
    }
}