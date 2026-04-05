using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class Submission
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public string? Content { get; set; }
        public DateTime SubmissionDate { get; set; }
        public double Grade { get; set; }
    }
}
