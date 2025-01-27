using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceObject.DataTransferObjects
{
    public class UserDto
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CustomerID { get; set; }
        
        public string EmailAddress { get; set; }

        public IList<GroupDto> Groups { get; set; }

        public CustomerDto Customer { get; set; }

        public IList<ApplicationDto> Applications { get; set; }

        public string Profilephoto { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsWrite { get; set; }

        public bool IsRead { get; set; }
    }
}
