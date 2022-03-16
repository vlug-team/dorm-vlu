using FluentValidation;

namespace vlu_dorm.Models.Validation
{
    public class StudentValidation : AbstractValidator<Students>
    {
        public StudentValidation()
        {
         var date =    
            RuleFor(p => p.FullName).NotEmpty().WithMessage("Bạn phải điền tên");
            RuleFor(p => p.PermanentAddress).NotEmpty().WithMessage("Bạn phải điền địa chỉ thường chú");
            RuleFor(p => p.StudentCode).NotNull().WithMessage("Bạn phải điềm mã số sinh viên");
            RuleFor(p => p.BirthDay).NotEmpty().LessThanOrEqualTo(DateTime.Today).WithMessage("Bạn phải điền đúng ngày sinh");
            RuleFor(p => p.Department).NotEmpty().WithMessage("Bạn phải chọn khoa");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Bạn phải nhập mail");
            RuleFor(s => s.PhoneNumeber).Length(10, 11).WithMessage("Số điện thoại phải gồm 10 hoặc 11 số");
        }
    }
}
