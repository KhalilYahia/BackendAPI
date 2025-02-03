using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Common
{
    public static class Utils
    {
        public static DateTime ServerNow
        {
            get
            {
                DateTime date1 = DateTime.UtcNow;

                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Syria Standard Time");

                DateTime date2 = TimeZoneInfo.ConvertTime(date1, tz);
                // return DateTime.Now.AddHours(11);
                return date2;
            }
        }
        public static string API_PATH = "http://localhost:40008/";

        public static string PhysicalImageCategory = "/AllImages/CategoryImg/";
       // public static string ImageCategoryURL = API_PATH + "/AllImages/CategoryImg/";

        public static string PhysicalImageAdvertisment = "/AllImages/Homes/";
       // public static string ImageAdvertismentURL = API_PATH + "/AllImages/Homes/";

        public static string PhysicalLLAMA_Model = "/LLAMA_Model/";

        public static string ImageDefaultName = "index.png";

        public enum DirectionType
        {
            In,
            Out
        }
    }

    /// <summary>
    /// تحديد من سوف يظهر أولا
    /// </summary>
    public enum ClassOfAdvertisment { A = 0, B = 1, C = 2, D = 3};
    public enum StatusOfAdvertisment { STOPPED = 0, ACTIVE = 1 };
    /// <summary>
    /// بيع أو أجار
    /// </summary>
    public enum TypeOfAdvertisment { SELLING = 0, RENT = 1 };
    /// <summary>
    /// type of the property,
    /// 0--> int, 1--> double, 2-->decimal, 3--> bool, 4--> DateTime, 5--> string, 6-->Guid 
    /// </summary>
    public enum TypeOfProperty {INT=0, DOUBLE=1, DECIMAL=2, BOOL=3, DATETIME=4,STRING=5,GUID=6 };
    /// <summary>
    /// Type show for user display
    /// 0--> checkbox, 1--> dropdownlist, 2--> TextBox, 3--> Calender
    /// </summary>
    public enum TypeShowOfProperty { checkbox = 0, dropdownlist = 1, TextBox = 2, Calender = 3};

    public enum LanguageHelper { ARABIC = 1, ENGLISH = 2 };
    public static class Roles
    {
        public static string DeveloperRole = "Developer";
        public static string AdminRole = "Admin";
        public static string AdvertiserUserRole = "Advertiser";
        public static string NormalUserRole = "NormalUser";
        public static string BannedUserRole = "BannedUser";

    }
}
