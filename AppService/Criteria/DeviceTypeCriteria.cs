using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.Criteria
{
    public class DeviceTypeCriteria : Criteria
    {
        public int AppId { get; set; }
        public int DeviceTypeId { get; set; }
    }
}
