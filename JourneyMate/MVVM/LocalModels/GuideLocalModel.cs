using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.LocalModels
{
    public class GuideLocalModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? TpNo { get; set; }
        public string? Image { get; set; }
        public string? Descriptiohn { get; set; }
        public string? IsActive { get; set; }
        public string? Language { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<ImageSource>? imageSources { get; set; }
    }
}
