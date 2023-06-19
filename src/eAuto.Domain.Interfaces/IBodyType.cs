namespace eAuto.Domain.Interfaces
{
    public interface IBodyType
    {
        int BodyTypeId { get; set; }
        string? Name { get; set; }
        void Save();
        void Delete();
    }
}
