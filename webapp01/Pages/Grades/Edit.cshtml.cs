using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp01.Data;
using webapp01.Models;

namespace webapp01.Pages.Grades;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public StudentCourse StudentCourse { get; set; } = default!;
    
    public string StudentName { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int? studentId, int? courseId)
    {
        if (studentId == null || courseId == null)
        {
            return NotFound();
        }

        var studentCourse = await _context.StudentCourses
            .Include(sc => sc.Student)
            .Include(sc => sc.Course)
            .FirstOrDefaultAsync(m => m.StudentId == studentId && m.CourseId == courseId);
            
        if (studentCourse == null)
        {
            return NotFound();
        }

        StudentCourse = studentCourse;
        StudentName = $"{studentCourse.Student.LastName} {studentCourse.Student.FirstName}";
        CourseName = studentCourse.Course.Name;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Временно отключим валидацию модели для отладки
        ModelState.Clear();

        var studentCourse = await _context.StudentCourses
            .FirstOrDefaultAsync(m => m.StudentId == StudentCourse.StudentId 
                && m.CourseId == StudentCourse.CourseId);

        if (studentCourse == null)
        {
            return NotFound();
        }

        // Обновляем только оценку
        studentCourse.Grade = StudentCourse.Grade;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return RedirectToPage("./Index");
    }
} 