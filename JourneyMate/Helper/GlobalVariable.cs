using JourneyMate.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.Helper
{
    public static class GlobalVariable
    {
        // Token_Valid GET & SET
        public static string GetHotelImage()
        {
            return SecureStorage.GetAsync(SD.ImgUrl.ToString()).GetAwaiter().GetResult();
        }
        public static void SetHotelImage(string imgUrl)
        {
            SecureStorage.SetAsync(SD.ImgUrl.ToString(), imgUrl.ToString());
        }

        // SreMgs Yes No Vlaue GET & SET
        public static void SetSureMgsVlaue(bool SureMgsVlaue)
        {
            SecureStorage.Remove(SD.SureMgsVlaue);
            SecureStorage.SetAsync(SD.SureMgsVlaue, SureMgsVlaue.ToString());
        }
        public static bool GetSureMgsVlaue()
        {
            var tmp = SecureStorage.GetAsync(SD.SureMgsVlaue.ToString()).GetAwaiter().GetResult();
            return Convert.ToBoolean(tmp);
        }
    }
}
