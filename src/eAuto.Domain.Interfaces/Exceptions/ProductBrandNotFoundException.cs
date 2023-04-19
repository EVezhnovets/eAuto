namespace eAuto.Domain.Interfaces.Exceptions
{
    public class ProductBrandNotFoundException : Exception
    {
        public ProductBrandNotFoundException() { }
        public ProductBrandNotFoundException(string message) : base(message) { }
    }
}