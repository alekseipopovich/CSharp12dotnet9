using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppASPNETCore01.Models;

namespace WebAppASPNETCore01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        static List<Student> _students = new List<Student>();

        public static void InitializeData(List<Student> students)
        {
            _students = students;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            return Ok(_students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetById(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }
    }
}
