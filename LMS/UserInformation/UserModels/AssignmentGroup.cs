using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class AssignmentGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public List<Assignment> Assignments { get; set; }

        public AssignmentGroup()
        {
            Assignments = new List<Assignment>();
        }
    }
}
