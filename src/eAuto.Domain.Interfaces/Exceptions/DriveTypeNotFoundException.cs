namespace eAuto.Domain.Interfaces.Exceptions
{
    public class DriveTypeNotFoundException : Exception
    {
        public DriveTypeNotFoundException() { }
        public DriveTypeNotFoundException(string message) : base(message) { }
    }
}