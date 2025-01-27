using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class TagRequest : RequestBase
    {
        public TagDto Tag { get; set; }
        public TagCloudCriteria Criteria { get; set; }
    }

}
