using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp01.Data;
using webapp01.Models;

namespace webapp01.Pages.Courses;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Course> Courses { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Courses = await _context.Courses.ToListAsync();
    }
} 