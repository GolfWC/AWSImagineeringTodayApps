using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class PolicyRequest : RequestBase
    {
        public PolicyCriteria Criteria { get; set; }
        public PolicyDto policy { get; set; }
    }
}
