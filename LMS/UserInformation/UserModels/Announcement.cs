using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Message { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
