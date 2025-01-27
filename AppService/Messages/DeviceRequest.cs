using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class DeviceRequest : RequestBase
    {
        public DeviceCriteria Criteria { get; set; }

        public DeviceAvailbilityDto Device { get; set; }
    }
}