namespace eAuto.Domain.Interfaces.Exceptions
{
    public class EngineTypeNotFoundException : Exception
    {
        public EngineTypeNotFoundException() { }
        public EngineTypeNotFoundException(string message) : base(message) { }
    }
}