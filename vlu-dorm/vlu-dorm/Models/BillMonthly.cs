using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace vlu_dorm.Models
{
    public class BillMonthly
    {
        public int Id { get; set; }

        [DisplayName("Số điện")]
        [Required(AllowEmptyStrings = false)]
        public double ElectricNumber { get; set; }

        [DisplayName("Số nước")]
        [Required(AllowEmptyStrings = false)]
        public double WaterNumber { get; set; }
        public DateTime BillMonth { get; set; }

        [DisplayName("Số lượng xe")]
        [Required(AllowEmptyStrings = false)]
        public int BikeNumber { get; set; }
        public virtual ICollection<Room> RoomsNavgation { get; set; }
    }
}
