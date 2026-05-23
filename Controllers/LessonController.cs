using Exam.Dtos.LessonDtos;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers;

public class LessonController : Controller
{
    private readonly AppDbContext _context;

    public LessonController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var lessons = await _context.Lessons.ToListAsync();
        return View(lessons);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLessonDto lesson)
    {
        if (ModelState.IsValid)
        {
            Lesson newLesson = new Lesson
            {
                LessonCode = lesson.LessonCode,
                LessonName = lesson.LessonName,
                ClassLevel = lesson.ClassLevel,
                TeacherName = lesson.TeacherName,
                TeacherSurname = lesson.TeacherSurname
            };
            _context.Lessons.Add(newLesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(lesson);
    }

    public async Task<IActionResult> Update(int id)
    {
        GetByIdLessonDto? lesson = await _context.Lessons.Where(x => x.Id == id).Select(x => new GetByIdLessonDto
        {
            Id = x.Id,
            ClassLevel = x.ClassLevel,
            LessonCode = x.LessonCode,
            LessonName = x.LessonName,
            TeacherName = x.TeacherName,
            TeacherSurname = x.TeacherSurname
        }).FirstOrDefaultAsync();

        if (lesson is null) return NotFound();

        return View(lesson);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, UpdateLessonDto lesson)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var findLesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
                findLesson.LessonName = lesson.LessonName;
                findLesson.ClassLevel = (byte)lesson.ClassLevel;
                findLesson.TeacherName = lesson.TeacherName;
                findLesson.TeacherSurname = lesson.TeacherSurname;
                findLesson.LessonCode = lesson.LessonCode;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Lessons.Any(x => x.LessonCode == lesson.LessonCode))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(lesson);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var lesson = await _context.Lessons
            .Where(x => x.Id == id)
            .Select(x => new GetByIdLessonDto
            {
                Id = x.Id,
                ClassLevel = x.ClassLevel,
                LessonCode = x.LessonCode,
                LessonName = x.LessonName,
                TeacherName = x.TeacherName,
                TeacherSurname = x.TeacherSurname
            }).FirstOrDefaultAsync();

        if (lesson == null) return NotFound();

        return View(lesson);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);

        if (lesson != null)
        {
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
