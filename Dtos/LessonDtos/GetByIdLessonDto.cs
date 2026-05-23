namespace Exam.Dtos.LessonDtos;

public record GetByIdLessonDto
{
    public int Id { get; set; }
    public string LessonCode { get; set; }
    public string LessonName { get; set; }
    public byte ClassLevel { get; set; }
    public string TeacherName { get; set; }
    public string TeacherSurname { get; set; }
}
