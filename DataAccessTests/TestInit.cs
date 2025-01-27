using System;
using System.Collections.Generic;
using System.Text;

using BusinessObjects;

namespace DataAccessTests
{
    public class TestInit
    {
        private User _contextUser;

        public TestInit()
        {
            this._contextUser = new User(1, "John", "Public", 1, "test@gmail.com");
            this._contextUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            Logger.Logger.Instance.Severity = Logger.LogSeverity.Debug;
            Logger.Logger.Instance.Attach(new Logger.ObserverLogToFile("AWSTestDebugging.txt"));
        }

        public User ContextUser
        {
            get { return this._contextUser; }
        }
    }
}
