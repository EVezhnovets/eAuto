using System.Globalization;

namespace eAuto.Web
{
    public static class WebConstants
    {
        public const string CarsImages = @"\images\cars\";
        public const string MotorOilsImages = @"\images\motorOils\";

        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";
        public const string EmployeeRole = "Employee";

        public static CultureInfo CultureInfoEN_US = new CultureInfo("en-US");
        public static CultureInfo CultureInfoBE_BY = new CultureInfo("be-BY");

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusProcessing = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusCompleted = "Completed";
        public const string StatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

        public const string SessionCart = "SessionShoppingCart";
    }
}