namespace eAuto.Domain.Interfaces.Exceptions
{
    public class TransmissionNotFoundException : Exception
    {
        public TransmissionNotFoundException() { }
        public TransmissionNotFoundException(string message) : base(message) { }
    }
}