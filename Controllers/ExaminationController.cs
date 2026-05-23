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
        ViewBag.Lessons = new SelectList(_context.Lessons, "Id", "LessonName");

        var students = _context.Students.Select(s => new
        {
            Id = s.Id,
            FullName = s.Number + " - " + s.Name + " " + s.Surname
        }).ToList();
        ViewBag.Students = new SelectList(students, "Id", "FullName");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExaminationDto dto)
    {
        if (ModelState.IsValid)
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

        ViewBag.Lessons = new SelectList(_context.Lessons, "Id", "LessonName", dto.LessonId);
        ViewBag.Students = new SelectList(_context.Students, "Id", "Name", dto.StudentId);
        return View(dto);
    }
}
