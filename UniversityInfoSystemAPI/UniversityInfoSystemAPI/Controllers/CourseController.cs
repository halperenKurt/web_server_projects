using Microsoft.AspNetCore.Mvc;
using UniversityInfoSystemAPI.Models;

namespace UniversityInfoSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private static List<Course> courses = new()
    {
        new Course { Id = "BIM308", Title = "Web Server Programming", Classroom = "B6" },
        new Course { Id = "BB0102", Title = "Data Structures", Classroom = "B2" }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(string id)
        {
            var course = courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public ActionResult<Course> PostCourse(Course course)
        {
            courses.Add(course);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(string id)
        {
            var course = courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();

            courses.Remove(course);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(string id, Course updatedCourse)
        {
            var course = courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found.");
            }

            if (string.IsNullOrWhiteSpace(updatedCourse.Title) || string.IsNullOrWhiteSpace(updatedCourse.Classroom))
            {
                return BadRequest("Course data is invalid.");
            }

            course.Title = updatedCourse.Title;
            course.Classroom = updatedCourse.Classroom;

            return Ok(course);
        }
    }

}
