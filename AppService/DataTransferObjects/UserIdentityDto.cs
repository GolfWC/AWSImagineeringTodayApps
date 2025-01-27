using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class UserIdentityDto
    {
        public int UserId { get; set; }

        public string Provider { get; set; }

        public string Identifier { get; set; }
        public IList<UserDto> Users { get; set; }
    }
}
