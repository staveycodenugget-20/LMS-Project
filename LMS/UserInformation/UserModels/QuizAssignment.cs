using System;
using System.Collections.Generic;
using System.Text;
using UserInformation.UserModels;

namespace UserInformation.UserModels
{
    public class QuizAssignment : Assignment
    {
        public string Question { get; set; } = "";
    }
}
