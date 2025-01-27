using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class DeviceAvailbilityDto
    {
        public int DeviceID { get; set; }

        public string DeviceSerialNumber { get; set; }

        public int DeviceTypeID { get; set; }

        public bool IsProvisioned { get; set; }

        public IList<TagDto> Tags { get; set; }

       
    }
}