namespace Exam.Dtos.StudentDtos;

public record GetByIdStudentDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte Class { get; set; }
}
