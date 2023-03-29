namespace eAuto.Domain.Interfaces.Exceptions
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException() { }
        public CarNotFoundException(string message) : base(message) { }
    }
}