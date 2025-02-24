using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp01.Data;
using webapp01.Models;

namespace webapp01.Pages.Grades;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public StudentCourse StudentCourse { get; set; } = default!;
    
    public SelectList StudentList { get; set; } = default!;
    public SelectList CourseList { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var students = await _context.Students
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .Select(s => new { Id = s.Id, FullName = $"{s.LastName} {s.FirstName}" })
            .ToListAsync();
            
        StudentList = new SelectList(students, "Id", "FullName");
            
        var courses = await _context.Courses
            .OrderBy(c => c.Name)
            .ToListAsync();
            
        CourseList = new SelectList(courses, "Id", "Name");
            
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // Временно отключим валидацию модели для отладки
        ModelState.Clear();

        if (StudentCourse.StudentId == 0 || StudentCourse.CourseId == 0)
        {
            ModelState.AddModelError(string.Empty, "Выберите студента и курс");
            await LoadSelectLists();
            return Page();
        }

        // Проверяем, не существует ли уже такая запись
        var exists = await _context.StudentCourses
            .AnyAsync(sc => sc.StudentId == StudentCourse.StudentId 
                        && sc.CourseId == StudentCourse.CourseId);
                        
        if (exists)
        {
            ModelState.AddModelError(string.Empty, 
                "Этот студент уже записан на данный курс");
            await LoadSelectLists();
            return Page();
        }

        // Добавим отладочный вывод
        System.Diagnostics.Debug.WriteLine($"StudentId: {StudentCourse.StudentId}");
        System.Diagnostics.Debug.WriteLine($"CourseId: {StudentCourse.CourseId}");
        System.Diagnostics.Debug.WriteLine($"Grade: {StudentCourse.Grade}");

        _context.StudentCourses.Add(StudentCourse);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    // Выделим загрузку списков в отдельный метод
    private async Task LoadSelectLists()
    {
        var students = await _context.Students
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .Select(s => new { Id = s.Id, FullName = $"{s.LastName} {s.FirstName}" })
            .ToListAsync();
            
        StudentList = new SelectList(students, "Id", "FullName", StudentCourse.StudentId);
            
        var courses = await _context.Courses
            .OrderBy(c => c.Name)
            .ToListAsync();
            
        CourseList = new SelectList(courses, "Id", "Name", StudentCourse.CourseId);
    }
} 