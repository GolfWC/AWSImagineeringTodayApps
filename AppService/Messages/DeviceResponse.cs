using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class DeviceResponse : ResponseBase
    {
        public IList<DeviceAvailbilityDto> Devices { get; set; }
    }
}
