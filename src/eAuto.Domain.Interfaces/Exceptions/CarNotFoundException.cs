namespace eAuto.Domain.Interfaces.Exceptions
{
    public class CarNotFoundException : Exception
    {
        public CarNotFoundException(string message) : base(message) 
        {
        }
    }
}