namespace eAuto.Domain.Interfaces.Exceptions
{
    public class TransmissionNotFoundException : Exception
    {
        public TransmissionNotFoundException(string message) : base(message) { }
    }
}