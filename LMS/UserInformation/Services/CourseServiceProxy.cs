using System;
using System.Collections.Generic;
using System.Text;
using UserInformation.UserModels;

namespace UserInformation.Services
{
    public class CourseServiceProxy
    {
        private static CourseServiceProxy instance;
        private static object objectLock = new object();

        private List<Course> courses;
        public List<Course> Courses => courses;
        private CourseServiceProxy() { 
            courses = new List<Course>();
        }

        public static CourseServiceProxy Current
        {
            get
            {
                lock (objectLock)
                {
                    if (instance == null)
                    {
                        instance = new CourseServiceProxy();
                    }
                }
                return instance;
            }
        }

        private int LastKey => Courses.Any() ? Courses.Select(c => c.Id).Max() : 0;
        public Course? Add(Course? course) 
        {
            if (course == null) 
            {
                return null;
            }

            if(course.Id == 0)
            {
                course.Id = LastKey + 1;
            }
            
            courses.Add(course);
            return course;
        }
    }
}
