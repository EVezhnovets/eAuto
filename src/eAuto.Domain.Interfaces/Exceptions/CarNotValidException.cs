namespace eAuto.Domain.Interfaces.Exceptions
{
    public class CarNotValidException : Exception
    {
		public CarNotValidException() { }
		public CarNotValidException(string message) : base(message) { }
	}
}