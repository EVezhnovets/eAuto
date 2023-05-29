using eAuto.Data.Context;
using eAuto.Data.Interfaces;

namespace eAuto.Data
{
    public class OrderHeaderRepository : Repository<OrderHeaderRepository>, IOrderHeaderRepository
    {
        private readonly EAutoContext _db;
        public OrderHeaderRepository(EAutoContext db) : base(db)
        {
            _db = db;
        }
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(i => i.Id == id);

            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}