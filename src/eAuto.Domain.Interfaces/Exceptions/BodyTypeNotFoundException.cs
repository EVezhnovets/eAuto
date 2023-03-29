namespace eAuto.Domain.Interfaces.Exceptions
{
    public class BodyTypeNotFoundException : Exception 
    {
        public BodyTypeNotFoundException() { }
        public BodyTypeNotFoundException(string message) : base(message) { }
    }
}