namespace eAuto.Domain.Interfaces.Exceptions
{
    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException() { }
        public ModelNotFoundException(string message) : base(message) { }
    }
}