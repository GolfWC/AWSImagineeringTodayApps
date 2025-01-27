using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class DeviceGroupRequest : RequestBase
    {
        public DeviceGroupCriteria Criteria { get; set; }

        public DeviceGroupDto DeviceGroup { get; set; }
    }
}
