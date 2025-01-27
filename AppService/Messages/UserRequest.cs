using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class UserRequest : RequestBase
    {
        public UserCriteria Criteria { get; set; }

        public UserDto User { get; set; }
    }
}
