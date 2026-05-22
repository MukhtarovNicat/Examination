namespace Exam.Models;

public class Examination
{
    public int Id { get; set; }
    public string LessonCode { get; set; }
    public int StudentNumber { get; set; }
    public DateTime ExamDate { get; set; }
    public byte Grade { get; set; }
}
