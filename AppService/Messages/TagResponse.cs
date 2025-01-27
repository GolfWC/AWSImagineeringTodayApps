using System;
using System.Collections.Generic;
using System.Text;
using ServiceObject.MessageBase;
using ServiceObject.DataTransferObjects;
using ServiceObject.Criteria;

namespace ServiceObject.Messages
{
    public class TagResponse : ResponseBase
    {
        public IList<TagDto> Tags { get; set; }
    }
}
