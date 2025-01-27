using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.Criteria
{
    public class TagCloudCriteria : Criteria
    {
        public int TagId { get; set; }
        public int EntityId { get; set; }
        public int MappedToId { get; set; }
        public int CustomerId { get; set; }
        
    }
}
