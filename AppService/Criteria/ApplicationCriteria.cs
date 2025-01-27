using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.Criteria
{
    public class ApplicationCriteria : Criteria
    {
        public int AppID { get; set; }

        public string AppName { get; set; }
    }
}