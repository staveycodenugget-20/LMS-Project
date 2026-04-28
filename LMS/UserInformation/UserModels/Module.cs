using System;
using System.Collections.Generic;
using System.Text;

namespace UserInformation.UserModels
{
    public class Module
    {

        public int Id { get; set; }
        public List<ModuleContent> Contents { get; set; }
        public string Name { get; set; }

        public Module()
        {
            Contents = new List<ModuleContent>();
        }
    }
}
