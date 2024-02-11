using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.Models
{
    public class RestaurantModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public string IsActive { get; set; }
        public string Reason { get; set; }
        public string ImageURl { get; set; }
        public string Description { get; set; }
        public string HeaderName { get; set; }
        public string SubHeaderName { get; set; }
        public string Price { get; set; }
        public string HasMoreInfo { get; set; }
    }
}
