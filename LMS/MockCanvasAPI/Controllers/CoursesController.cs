using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserInformation.UserModels;

namespace MockCanvasAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private static List<Course> courses = new List<Course>();

        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return courses;
        }

        [HttpPost]
        public Course Post(Course course)
        {
            courses.Add(course);
            return course;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var course = courses.FirstOrDefault(c => c.Id == id);

            if (course != null)
            {
                courses.Remove(course);
            }
        }
    }
}
