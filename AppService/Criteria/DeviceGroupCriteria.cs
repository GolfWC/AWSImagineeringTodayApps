using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.Criteria
{
    public class DeviceGroupCriteria : Criteria
    {
        public int DeviceGorupId { get; set; }

        public int DeviceId { get; set; }

        public int CustomerId { get; set; }

        public int ApplicationId { get; set; }

        public int DeviceTypeId { get; set; }
    }
}