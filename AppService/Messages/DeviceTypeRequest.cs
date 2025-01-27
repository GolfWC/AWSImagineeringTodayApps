using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages

{
    public class DeviceTypeRequest : RequestBase
    {
        public DeviceTypeCriteria Criteria { get; set; }
        public DeviceTypeDto DeviceType { get; set; }
    }
}
