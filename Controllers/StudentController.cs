using Exam.Dtos.LessonDtos;
using Exam.Dtos.StudentDtos;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers;

public class StudentController : Controller
{
    private readonly AppDbContext _context;

    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _context.Students.ToListAsync();
        return View(students);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto student)
    {
        if (ModelState.IsValid)
        {
            bool exists = await _context.Students.AnyAsync(x => x.Number == student.Number);
            if (exists)
            {
                ModelState.AddModelError("Number", "Bu nömrəli tələbə mövcuddur");
                return View(student);
            }

            var newStudent = new Student
            {
                Name = student.Name,
                Number = student.Number,
                Class = student.Class,
                Surname = student.Surname,
            };
            await _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    public async Task<IActionResult> Update(int id)
    {
        UpdateStudentDto? student = await _context.Students.Where(x => x.Id == id).Select(x => new UpdateStudentDto
        {
            Id = x.Id,
            Class = x.Class,
            Name = x.Name,
            Number = x.Number,
            Surname = x.Surname
        }).FirstOrDefaultAsync();

        if (student is null) return NotFound();

        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, UpdateStudentDto student)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var findStudent = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (findStudent is null) return NotFound();

                if (findStudent.Number != student.Number)
                {
                    bool exists = await _context.Students.AnyAsync(x => x.Number == student.Number);
                    if (exists)
                    {
                        ModelState.AddModelError("Number", "Bu nömrəli tələbə mövcuddur");
                        return View(student);
                    }
                }

                findStudent.Number = student.Number;
                findStudent.Name = student.Name;
                findStudent.Surname = student.Surname;
                findStudent.Class = (byte)student.Class;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students
            .Where(x => x.Id == id)
            .Select(x => new GetByIdStudentDto
            {
                Id = x.Id,
                Number = x.Number,
                Name = x.Name,
                Surname = x.Surname,
                Class = x.Class
            }).FirstOrDefaultAsync();

        if (student is null) return NotFound();

        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
