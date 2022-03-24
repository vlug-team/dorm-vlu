using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vlu_dorm.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("MSSV")]
        [Required(AllowEmptyStrings = false)]
        public string StudentCode { get; set; }
        [DisplayName("Họ và tên")]
        [Required(AllowEmptyStrings = false)]
        public string FullName { get; set; }
        public DateTime CreateAt { get; set; }
        [Required, DisplayName("Ngày sinh")]
        public DateTime BirthDay { get; set; }
        [DisplayName("Địa chỉ thường chú")]
        [Required(AllowEmptyStrings = false)]
        public string PermanentAddress { get; set; }
        [DisplayName("Khóa")]
        [Required(AllowEmptyStrings = false)]
        public string Course { get; set; }
        [DisplayName("Khoa")]
        [Required(AllowEmptyStrings = false)]
        public string Department { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Số điện thoại")]
        [Phone]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Số điện thoại phải là 10-11 số")]
        public string PhoneNumeber { get; set; }
        [DisplayName("Email"), DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [DisplayName("Giới tính")]
        public string Gender { get; set; }
        public bool IsConfirm { get; set; }
        public List<RoomExploit> RoomExploits { set; get; }
    }
}
