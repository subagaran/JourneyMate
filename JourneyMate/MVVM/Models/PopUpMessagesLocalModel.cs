using SQLite;

namespace JourneyMate.MVVM.Models
{
    public class PopUpMessagesLocalModel
    {
        public string Tile { get; set; }
        public string Image { get; set; } = "";
        public string Mgs { get; set; }
        public string Mgs2 { get; set; } = "";
        public string Type { get; set; } = "";
        public string Status { get; set; } = "";
        public string BtnName { get; set; } = "Ok";
        public bool IsSure { get; set; } = false;
    }
}