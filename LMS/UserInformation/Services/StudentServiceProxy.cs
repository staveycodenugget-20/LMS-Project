using UserInformation.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLI.LMS.Services
{
    public class StudentServiceProxy
    {
        private static StudentServiceProxy? instance;
        private static object instanceLock = new object();

        public static StudentServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StudentServiceProxy();
                    }
                }
                return instance;
            }
        }

        private List<Student> students;

        public List<Student> Students => students;
        private StudentServiceProxy() { 
            students = new List<Student>();
        }

        public int LastKey => Students.Any() ? Students.Select(s => s.Id).Max() : 0;

        public Student? Add(Student? student)
        {
            if(student == null)
            {
                return student;
            }

            if(student.Id == 0)
            {
                student.Id = LastKey + 1;
            }

            students.Add(student);
            return student;
        }
        public void Remove(Student student)
        {
            if (student == null) return;

            students.Remove(student);
        }
        public void Update(Student student)
        {
            var existing = students.FirstOrDefault(s => s.Id == student.Id);

            if (existing == null) return;

            existing.Name = student.Name;
        }
    }
}
