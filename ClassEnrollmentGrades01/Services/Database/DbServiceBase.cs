using Microsoft.EntityFrameworkCore;
using ClassEnrollmentGrades01.Data;

namespace ClassEnrollmentGrades01.Services.Database
{
    public abstract class DbServiceBase : IDisposable
    {
        protected readonly ApplicationContext _context;

        protected DbServiceBase()
        {
            _context = new ApplicationContext();
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
} 