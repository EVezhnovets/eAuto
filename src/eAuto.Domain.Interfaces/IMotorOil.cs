namespace eAuto.Domain.Interfaces
{
    public interface IMotorOil
	{
        int MotorOilId { get; set; }
        string Name { get; set; }
        void Save();
        void Delete();
    }
}