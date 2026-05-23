using Exam.Models;
using FluentValidation;

namespace Exam.Dtos.ExaminationDtos;

public record CreateExaminationDto
{
    public DateTime ExamDate { get; set; }
    public byte Grade { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }
}
public class CreateExaminationDtoValidator : AbstractValidator<CreateExaminationDto>
{
    public CreateExaminationDtoValidator()
    {
        RuleFor(x => x.ExamDate)
            .NotEmpty().WithMessage("Boş ola bilməz");

        RuleFor(x => x.Grade)
            .InclusiveBetween((byte)0, (byte)100).WithMessage("0 dan böyük 100 dən kiçik olmalıdır və ya bərabər");

        RuleFor(x => x.LessonId)
            .NotEmpty().WithMessage("Boş ola bilməz");

        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Boş ola bilməz");
    }
}