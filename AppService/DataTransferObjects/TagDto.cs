using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class TagDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
    }
}