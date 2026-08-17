using System;
using System.Collections.Generic;
using System.Text;
using UserInformation.UserModels;

namespace UserInformation.Services
{
    public class SemesterService
    {
        public static SemesterService Current { get; } = new SemesterService();

        public Semester CurrentSemester { get; set; } = new Semester
        {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today.AddMonths(4)
        };
    }
}
