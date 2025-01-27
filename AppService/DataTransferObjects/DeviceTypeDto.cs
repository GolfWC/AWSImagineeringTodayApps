using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class DeviceTypeDto
    {
        public int TypeId { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string Prefix { get; set; }

    }
}
