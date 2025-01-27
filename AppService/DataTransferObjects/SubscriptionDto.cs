using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string EntityType { get; set; }
        public int EntityValue { get; set; }
        public int SubDefId { get; set; }
        public string SubValue { get; set; }

    }
}
