using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.Criteria
{
    public class UserCriteria : Criteria
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public int CustomerId { get; set; }
    }
}
