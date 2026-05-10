using Microsoft.AspNetCore.Mvc;
using UserInformation.UserModels;

namespace MockCanvasAPI.Controllers
{
    //Test with https://localhost:7192/swagger or whatever  run program generates for local host num

    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>();

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return students;
        }

        [HttpPost]
        public Student Post(Student student)
        {
            students.Add(student);
            return student;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student != null)
            {
                students.Remove(student);
            }
        }
    }
}