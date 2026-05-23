using FluentValidation;

namespace Exam.Dtos.StudentDtos;

public record CreateStudentDto
{
    public int Number { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte Class { get; set; }
}
public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Boş ola bilməz");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Boş ola bilməz")
            .MaximumLength(30).WithMessage("Uzunluq maksimum 30 xarakter ola bilər");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Boş ola bilməz")
            .MaximumLength(30).WithMessage("Uzunluq maksimum 30 xarakter ola bilər");

        RuleFor(x => x.Class)
            .NotEmpty().WithMessage("Boş ola bilməz");
    }
}
