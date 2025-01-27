using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;

namespace ServiceObject.Messages
{
    public class GroupsResponse : ResponseBase
    {
        public IList<GroupDto> Groups { get; set; }
    }
}