using eAuto.Data.Context;
using eAuto.Data.Interfaces;
using eAuto.Data.Interfaces.DataModels;

namespace eAuto.Data
{
    public class OrderHeaderRepository : Repository<OrderHeaderDataModel>, IOrderHeaderRepository
    {
        private readonly EAutoContext _db;
        public OrderHeaderRepository(EAutoContext db) : base(db)
        {
            _db = db;
        }
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(i => i.OrderId == id);

            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
            _db.SaveChanges();
        }
        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(i => i.OrderId == id);

            orderFromDb!.PaymentDate = DateTime.Now;
			orderFromDb.SessionId = sessionId;
			orderFromDb.PaymentIntentId = paymentIntentId;
            _db.SaveChanges();
		}
	}
}