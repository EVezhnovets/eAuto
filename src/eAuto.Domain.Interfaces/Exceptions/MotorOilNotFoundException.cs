namespace eAuto.Domain.Interfaces.Exceptions
{
    public class MotorOilNotFoundException : Exception 
    {
        public MotorOilNotFoundException() { }
        public MotorOilNotFoundException(string message) : base(message) { }
    }
}