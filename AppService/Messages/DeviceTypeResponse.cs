using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;

namespace ServiceObject.Messages
{
    public class DeviceTypeResponse : ResponseBase
    {
        public IList<DeviceTypeDto> Types { get; set; }
    }
}
