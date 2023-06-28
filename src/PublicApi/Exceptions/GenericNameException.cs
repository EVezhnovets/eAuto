namespace PublicApi.Exceptions
{
    [Serializable]
    public class GenericNameException<T> : Exception
    {
        public string? Name { get; set; }
        public GenericNameException() { }

        public GenericNameException(string message) : base(message) { }

        public GenericNameException(string message, Exception inner) : base(message, inner) { }

        public GenericNameException(string message, string name) : this(message)
        {
            Name = name;
        }
    }
}