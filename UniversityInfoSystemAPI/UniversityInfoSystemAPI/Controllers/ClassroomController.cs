using Microsoft.AspNetCore.Mvc;
using UniversityInfoSystemAPI.Models;

namespace UniversityInfoSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private static List<Classroom> classrooms = new()
    {
        new Classroom { Id = "B6", Description = "Computer Engineering Ground Floor", Capacity = 60 },
        new Classroom { Id = "B2", Description = "Software Engineering Lab", Capacity = 45 }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Classroom>> GetClassrooms()
        {
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public ActionResult<Classroom> GetClassroom(string id)
        {
            var classroom = classrooms.FirstOrDefault(c => c.Id == id);
            if (classroom == null) return NotFound();
            return Ok(classroom);
        }
    }

}
