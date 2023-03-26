namespace eAuto.Domain.Interfaces.Exceptions
{
    public class DriveTypeNotFoundException : Exception
    {
        public DriveTypeNotFoundException(string message) : base(message) { }
    }
}