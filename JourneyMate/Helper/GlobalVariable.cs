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
    }
}
