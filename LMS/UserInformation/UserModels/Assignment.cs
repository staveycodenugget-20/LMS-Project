using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class Assignment
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int AvailablePoints { get; set; }
        public DateTime DueDate { get; set; }
        public List<Submission>? Submissions { get; set; }
        //Fixed a null error when trying to add a submission to an assignment.
        public Assignment()
        {
            Submissions = new List<Submission>();
        }
    }
}
