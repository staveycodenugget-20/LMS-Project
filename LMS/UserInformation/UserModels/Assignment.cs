using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class Assignment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailablePoints { get; set; }
        public DateTime DueDate { get; set; }
        public List<Submission> Submissions { get; set; }
    }
}
