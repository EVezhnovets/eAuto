namespace eAuto.Domain.Interfaces.Exceptions
{
    public class EngineNameNotFoundException : Exception
    {
        public EngineNameNotFoundException(string message) : base(message) { }
    }
}