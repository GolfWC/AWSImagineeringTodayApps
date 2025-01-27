using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceObject.MessageBase
{
    public abstract class RequestBase
    {
        public string Action { get; set; }

        public string[] LoadOptions { get; set; }

        public string IdToken { get; set; }
    }
}