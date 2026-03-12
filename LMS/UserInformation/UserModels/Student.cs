using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class Student : User
    {
        public Classification Classification { get; set; }

    }
    public enum Classification
    {
        Unknown, Freshman, Sophomore, Junior, Senior
    }
}
