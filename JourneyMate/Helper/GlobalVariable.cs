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
        public static string GetHotelImage()
        {
            return SecureStorage.GetAsync(SD.ImgUrl.ToString()).GetAwaiter().GetResult();
        }
        public static void SetHotelImage(string imgUrl)
        {
            SecureStorage.SetAsync(SD.ImgUrl.ToString(), imgUrl.ToString());
        }

        public static string GetHotelImage1()
        {
            return SecureStorage.GetAsync(SD.ImgUrl1.ToString()).GetAwaiter().GetResult();
        }
        public static void SetGuideId(int imgUrl1)
        {
            SecureStorage.SetAsync(SD.ImgUrl1.ToString(), imgUrl1.ToString());
        }

        public static int GetGuideId()
        {
            return Convert.ToInt32(SecureStorage.GetAsync(SD.ImgUrl1.ToString()).GetAwaiter().GetResult());
        }
        public static void SetHotelImage2(string imgUrl2)
        {
            SecureStorage.SetAsync(SD.ImgUrl2.ToString(), imgUrl2.ToString());
        }

        public static string GetHotelImage3()
        {
            return SecureStorage.GetAsync(SD.ImgUrl3.ToString()).GetAwaiter().GetResult();
        }
        public static void SetHotelImage3(string imgUrl3)
        {
            SecureStorage.SetAsync(SD.ImgUrl3.ToString(), imgUrl3.ToString());
        }
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
         
        public static void SetUserLogedIn(bool UserLogedIn)
        {
            SecureStorage.Remove(SD.UserLogedIn);
            SecureStorage.SetAsync(SD.UserLogedIn, UserLogedIn.ToString());
        }
        public static bool GetUserLogedIn()
        {
            var tmp = SecureStorage.GetAsync(SD.UserLogedIn.ToString()).GetAwaiter().GetResult();
            return Convert.ToBoolean(tmp);
        }

        // SreMgs Yes No Vlaue GET & SET
        public static void SetUserName(string UserName)
        { 
            SecureStorage.SetAsync(SD.UserName, UserName.ToString());
        }
        public static string GetUserName()
        {
            return SecureStorage.GetAsync(SD.UserName.ToString()).GetAwaiter().GetResult(); 
        }

        // SreMgs Yes No Vlaue GET & SET
        public static void SetUserRole(string UserRole)
        {
            SecureStorage.SetAsync(SD.UserRole, UserRole.ToString());
        }
        public static string GetUserRole()
        {
            return SecureStorage.GetAsync(SD.UserRole.ToString()).GetAwaiter().GetResult();
        }
        // SreMgs Yes No Vlaue GET & SET
        public static void SetUserId(string UserID)
        {
            SecureStorage.SetAsync(SD.UserID, UserID.ToString());
        }
        public static string GetUserId()
        {
            return SecureStorage.GetAsync(SD.UserID.ToString()).GetAwaiter().GetResult();
        }
    }
}
