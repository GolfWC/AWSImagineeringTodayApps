using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;


namespace ServiceObject.Messages
{
    public class GroupsRequest : RequestBase
    {
        public GroupCriteria Criteria { get; set; }

        public GroupDto Group { get; set; }
    }
}
