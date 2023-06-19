namespace eAuto.Domain.Interfaces
{
    public interface IDriveType
    {
        int DriveTypeId { get; set; }
        string? Name { get; set; }

        void Save();
        void Delete();
    }
}