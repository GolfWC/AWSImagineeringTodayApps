using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class DeviceGroupDto
    {
        public int DeviceGroupID { get; set; } 
        
        public int AppID { get; set; }
        
        public int CustomerId { get; set; }
        
        public string DeviceGroupName { get; set; }

        public IList<DeviceAvailbilityDto> Devices { get; set; }
    }
}