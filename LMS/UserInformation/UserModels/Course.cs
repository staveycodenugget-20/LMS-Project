using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public List<Student> Roster { get; set; }
        public List<Module> Modules { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }
        public string Semester { get; set; } = "";
        public string Section { get; set; } = "";
        public List<Announcement> Announcements { get; set; } = new List<Announcement>();

        public Course() { 
            Roster = new List<Student>();
            Modules = new List<Module>();
            Assignments = new List<Assignment>();
            AssignmentGroups = new List<AssignmentGroup>();
        }

    }
}
