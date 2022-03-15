using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vlu_dorm.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? BirthDay { get; set; }
        public string PermanentAddress { get; set; }
        public string Course { get; set; }
    }
}
