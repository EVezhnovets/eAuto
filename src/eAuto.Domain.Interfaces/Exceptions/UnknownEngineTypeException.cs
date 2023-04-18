namespace eAuto.Domain.Interfaces.Exceptions
{
    public class UnknownEngineTypeException : Exception
    {
        public UnknownEngineTypeException() { }
        public UnknownEngineTypeException(string message) : base(message) { }
    }
}