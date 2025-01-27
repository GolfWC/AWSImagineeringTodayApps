using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.Criteria
{
    public class GroupCriteria : Criteria
    {
        public int GroupId { get; set; }

        public int UserId { get; set; }

        public int CustomerId { get; set; }
    }
}