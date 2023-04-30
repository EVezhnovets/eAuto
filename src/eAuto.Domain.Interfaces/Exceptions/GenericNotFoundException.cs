namespace eAuto.Domain.Interfaces.Exceptions
{
    public class GenericNotFoundException<T> : Exception where T : class
    {
        public GenericNotFoundException() { }
        public GenericNotFoundException(string message) : base(message) { }
    }
}