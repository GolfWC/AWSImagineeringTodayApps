using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceObject.MessageBase
{
    public abstract class ResponseBase
    {
        public ResponseBase()
        {
            Acknowledge = AcknowledgeType.Failure;
        }
        
        public AcknowledgeType Acknowledge { get; set; }

        public string Message { get; set; }
    }
}