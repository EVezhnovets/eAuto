namespace eAuto.Domain.Interfaces
{
    public interface ITransmission
    {
        int TransmissionId { get; set; }
        string? Name { get; set; }

        void Save();
        void Delete();
    }
}