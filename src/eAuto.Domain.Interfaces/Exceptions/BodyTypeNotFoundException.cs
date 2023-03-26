namespace eAuto.Domain.Interfaces.Exceptions
{
    public class BodyTypeNotFoundException : Exception 
    {
        public BodyTypeNotFoundException(string message) : base(message) 
        {       
        }
    }
}