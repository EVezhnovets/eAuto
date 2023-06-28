namespace PublicApi.Exceptions
{
    [Serializable]
    public class GenericNotFoundException<T> : Exception
    {
        public GenericNotFoundException() { }

        public GenericNotFoundException(string message) : base(message) { }

        public GenericNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}