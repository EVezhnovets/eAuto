using System.Globalization;

namespace eAuto.Web
{
    public static class WebConstants
    {
        public const string CarsImages = @"\images\cars\";
        public static string AdminRole = "Admin";
        public static string CustomerRole = "Customer";
        public static string EmployeeRole = "Employee";
        public static CultureInfo CultureInfoEN_US = new CultureInfo("en-US");
        public static CultureInfo CultureInfoBE_BY = new CultureInfo("be-BY");
    }
}