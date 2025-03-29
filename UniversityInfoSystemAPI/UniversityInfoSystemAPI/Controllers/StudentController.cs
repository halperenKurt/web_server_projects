using Microsoft.AspNetCore.Mvc;
using UniversityInfoSystemAPI.Models;

namespace UniversityInfoSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new()
    {
        new Student { Id = 1, Name = "Emin Talip Demirkıran", Email = "etd@eskisehir.edu.tr", Courses = new List<string> { "BIM308", "BIM349" } },
        new Student { Id = 2, Name = "Ayşe Yılmaz", Email = "ayse@eskisehir.edu.tr", Courses = new List<string> { "BB0102" } }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            students.Add(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();

            students.Remove(student);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedStudent.Name) || string.IsNullOrWhiteSpace(updatedStudent.Email))
            {
                return BadRequest("Student data is invalid.");
            }

            student.Name = updatedStudent.Name;
            student.Email = updatedStudent.Email;
            student.Courses = updatedStudent.Courses;

            return Ok(student);
        }

    }

}
