using Exam.Dtos.ExaminationDtos;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers;

public class ExaminationController : Controller
{
    private readonly AppDbContext _context;

    public ExaminationController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var examinations = await _context.Examinations
                .Include(x => x.Lesson)
                .Include(x => x.Student)
                .ToListAsync();

        return View(examinations);
    }

    public IActionResult Create()
    {
        var lessons = _context.Lessons.Select(s => new
        {
            Id = s.Id,
            DisplayText = s.LessonName + " (Sinif: " + s.ClassLevel + ")"
        }).ToList();
        ViewBag.Lessons = new SelectList(lessons, "Id", "DisplayText");

        var students = _context.Students.Select(s => new
        {
            Id = s.Id,
            DisplayText = s.Number + " - " + s.Name + " " + s.Surname + " (Sinif: " + s.Class + ")" 
        }).ToList();
        ViewBag.Students = new SelectList(students, "Id", "DisplayText");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExaminationDto dto)
    {
        if (ModelState.IsValid)
        {
            if (dto.LessonId != null && dto.StudentId != null)
            {
                if (await CheckLessonAndStudentClass(dto.LessonId, dto.StudentId) == false)
                {
                    ModelState.AddModelError("", "Şagird və dərs eyni sinifə aid deyil!");
                }
                else
                {
                    var examination = new Examination
                    {
                        LessonId = dto.LessonId,
                        StudentId = dto.StudentId,
                        ExamDate = dto.ExamDate,
                        Grade = dto.Grade
                    };

                    await _context.Examinations.AddAsync(examination);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        var lessonList = _context.Lessons.Select(l => new {
            Id = l.Id,
            DisplayText = l.LessonName + " (Sinif: " + l.ClassLevel + ")"
        }).ToList();
        ViewBag.Lessons = new SelectList(lessonList, "Id", "DisplayText", dto.LessonId);

        var studentList = _context.Students.Select(s => new {
            Id = s.Id,
            DisplayText = s.Number + " - " + s.Name + " " + s.Surname + " (Sinif: " + s.Class + ")"
        }).ToList();
        ViewBag.Students = new SelectList(studentList, "Id", "DisplayText", dto.StudentId);

        return View(dto);
    }

    private async Task<bool> CheckLessonAndStudentClass(int lessonId, int studentId)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId);
        var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == lessonId);

        if (student.Class == lesson.ClassLevel)
            return true;

        return false;
    }
}
