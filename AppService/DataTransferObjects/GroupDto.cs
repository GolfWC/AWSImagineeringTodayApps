using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class GroupDto
    {
        public int GroupID { get; set; }

        public int CustomerID { get; set; }

        public string GroupName { get; set; }

        public IList<UserDto> Users { get; set; }
    }
}
