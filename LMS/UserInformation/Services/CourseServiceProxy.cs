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
        private CourseServiceProxy()
        {
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

            if (course.Id == 0)
            {
                course.Id = LastKey + 1;
            }

            courses.Add(course);
            return course;
        }
        public Course? CopyCourse(int courseId)
        {
            var original = courses.FirstOrDefault(c => c.Id == courseId);

            if (original == null)
                return null;

            var newCourse = new Course
            {
                Id = LastKey + 1,
                Name = original.Name + " (Copy)",
                Code = original.Code,
                Description = original.Description
            };

            foreach (var module in original.Modules)
            {
                var newModule = new Module
                {
                    Id = module.Id,
                    Name = module.Name
                };

                foreach (var content in module.Contents)
                {
                    if (content is PageItem page)
                    {
                        newModule.Contents.Add(new PageItem
                        {
                            Id = page.Id,
                            Content = page.Content
                        });
                    }
                    else if (content is FileItem file)
                    {
                        newModule.Contents.Add(new FileItem
                        {
                            Id = file.Id,
                            FilePath = file.FilePath
                        });
                    }
                    else if (content is AssignmentItem assignmentItem)
                    {
                        newModule.Contents.Add(new AssignmentItem
                        {
                            Id = assignmentItem.Id,
                            Assignment = assignmentItem.Assignment 
                        });
                    }
                }

                newCourse.Modules.Add(newModule);
            }

            foreach (var group in original.AssignmentGroups)
            {
                var newGroup = new AssignmentGroup
                {
                    Id = group.Id,
                    Name = group.Name,
                    Weight = group.Weight
                };

                foreach (var assignment in group.Assignments)
                {
                    newGroup.Assignments.Add(assignment); 
                }

                newCourse.AssignmentGroups.Add(newGroup);
            }

            courses.Add(newCourse);

            return newCourse;
        }
        
        }

    }

