namespace eAuto.Domain.Interfaces.Exceptions
{
    public class BrandNotFoundException : Exception
    {
        public BrandNotFoundException() { }
        public BrandNotFoundException(string message) : base(message) { }
    }
}