using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.Models
{
    public class ApiResponse
    {
        public int statusCode { get; set; }
        public bool isSuccess { get; set; }
        public List<string> errorMessages { get; set; }
        public List<Hotel> Result { get; set; }
        public ResultObject result { get; set; }
    }
    public class ResultObject
    {
        public UserObject user { get; set; }
        public string role { get; set; }
        public string token { get; set; }
    }

    public class UserObject
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string name { get; set; }
    }
}
