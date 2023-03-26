namespace eAuto.Domain.Interfaces.Exceptions
{
    public class GenerationNotFoundException : Exception
    {
        public GenerationNotFoundException(string message) : base(message) { }
    }
}