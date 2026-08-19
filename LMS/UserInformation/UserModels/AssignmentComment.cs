using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class AssignmentComment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int AssignmentId { get; set; }

        public string AuthorName { get; set; } = "";

        public string Message { get; set; } = "";

        public DateTime DatePosted { get; set; }
    }
}
