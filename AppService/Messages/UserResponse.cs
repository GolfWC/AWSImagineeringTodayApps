using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;

namespace ServiceObject.Messages
{
    public class UserResponse : ResponseBase
    {
        public IList<UserDto> Users { get; set; }
    }
}
