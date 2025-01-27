using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class SubscriptionRequest: RequestBase
    {
        public SubscriptionCriteria Criteria { get; set; }
        public SubscriptionDto Subscription { get; set; }
    }
}
