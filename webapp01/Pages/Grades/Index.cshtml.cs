using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp01.Data;
using webapp01.Models;

namespace webapp01.Pages.Grades;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<StudentCourse> Grades { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Grades = await _context.StudentCourses
            .Include(sc => sc.Student)
            .Include(sc => sc.Course)
            .ToListAsync();
    }
} 