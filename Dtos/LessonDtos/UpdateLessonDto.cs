using FluentValidation;

namespace Exam.Dtos.LessonDtos;

public record UpdateLessonDto
{
    public string? LessonCode { get; set; }
    public string? LessonName { get; set; }
    public byte? ClassLevel { get; set; }
    public string? TeacherName { get; set; }
    public string? TeacherSurname { get; set; }
}
public class UpdateLessonDtoValidator : AbstractValidator<UpdateLessonDto>
{
    public UpdateLessonDtoValidator()
    {
        RuleFor(x => x.LessonCode)
            .NotEmpty().WithMessage("Boş ola bilməz")
            .MaximumLength(10).WithMessage("Uzunluq maksimum 10 xarakter ola bilər");

        RuleFor(x => x.LessonName)
            .NotEmpty().WithMessage("Boş ola bilməz")
            .MaximumLength(30).WithMessage("Uzunluq maksimum 30 xarakter ola bilər");

        RuleFor(x => x.ClassLevel)
            .NotEmpty().WithMessage("Boş ola bilməz");

        RuleFor(x => x.TeacherName)
            .NotEmpty().WithMessage("Boş ola bilməz")
            .MaximumLength(20).WithMessage("Uzunluq maksimum 20 xarakter ola bilər");

        RuleFor(x => x.TeacherSurname)
            .NotEmpty().WithMessage("Boş ola bilməz")
            .MaximumLength(20).WithMessage("Uzunluq maksimum 20 xarakter ola bilər");
    }
}