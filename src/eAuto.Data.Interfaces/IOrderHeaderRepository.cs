using eAuto.Data.Interfaces.DataModels;

namespace eAuto.Data.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeaderDataModel>
    {
		void Update(OrderHeaderDataModel obj);
		void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
		void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
	}
}