using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlu_dorm.Models
{
    public class Room
    {
        public int Id { get; set; }
        [DisplayName("Số phòng")]
        [Required(AllowEmptyStrings = false)]
        public int RoomNumber { get; set; }
        [DisplayName("Số Lượng Người Tối Đa")]
        [Required(AllowEmptyStrings = false)]
        public int Capacity { get; set; }
        [DisplayName("Tiền phòng")]
        [Required(AllowEmptyStrings = false)]
        public double RoomPrice { get; set; }
        [DisplayName("Tiền điện / Kwh")]
        [Required(AllowEmptyStrings = false)]
        public double ElectricPrice { get; set; }
        [DisplayName("Tiền Nước / m3")]
        [Required(AllowEmptyStrings = false)]
        public double WaterPrice { get; set; }

    }
}
