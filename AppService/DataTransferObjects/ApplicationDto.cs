using System.Collections.Generic;

namespace ServiceObject.DataTransferObjects
{
    public class ApplicationDto
    {
        public int AppID { get; set; }

        public string AppName { get; set; }

        public string RouteURL { get; set; }

        public IList<DeviceAvailbilityDto> Devices { get; set; }
    }
}
