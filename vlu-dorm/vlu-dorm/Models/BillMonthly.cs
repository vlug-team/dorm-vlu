using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlu_dorm.Models
{
    public class BillMonthly
    {
        public int Id { get; set; }
        [DisplayName("Số điện")]
        [Required(AllowEmptyStrings = false)]
        public int ElectricNumber { get; set; }
        [DisplayName("Số nước")]
        [Required(AllowEmptyStrings = false)]
        public int WaterNumber { get; set; }
        public DateTime CreateAt { get; set; }
        public List<RoomExploit> RoomExploits { get; set; }
    }
}
