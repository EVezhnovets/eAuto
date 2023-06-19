namespace eAuto.Domain.Interfaces
{
    public interface IShoppingCart
    {
        int ShoppingCartId { get; set; }
        int ProductId { get; set; }
        string? Product { get; set; }
        IMotorOil? OilProduct { get; set; }
        int Count { get; set; }
        string? ApplicationUserId { get; set; }
        double Price { get; set; }

        void Save();
        void Delete();
    }
}