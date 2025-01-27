using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class PolicyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EntityType { get; set; }
        public int EntityValue { get; set; }
        public string PolicyValue1 { get; set; }
        public int PolicDefId { get; set; }
    }
}
