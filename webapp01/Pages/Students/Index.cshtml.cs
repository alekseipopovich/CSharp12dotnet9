using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp01.Data;
using webapp01.Models;

namespace webapp01.Pages.Students;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Student> Students { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Students = await _context.Students.ToListAsync();
    }
} 