using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.Criteria
{
    public class DeviceCriteria : Criteria
    {
        public int DeviceId { get; set; }
        
        public string SerialNumber { get; set; }

        public int DeviceGroupId { get; set; }

        public int DeviceTypeId { get; set; }

        public int AppId { get; set; }
    }
}