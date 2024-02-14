using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.Models
{
    public class ApiResponse<T>
    {
        public int statusCode { get; set; }
        public bool isSuccess { get; set; }
        public List<string> rrrorMessages { get; set; }
        public T result { get; set; }
    }
}
