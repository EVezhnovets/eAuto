namespace eAuto.Data.Interfaces
{
    public interface IOrderHeaderRepository
    {
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
    }
}