using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class UserIdentityTest
    {
        private User _contextUser;

        [TestInitialize]
        public void InitTestVars()
        {
            this._contextUser = new User(1, "John", "Public", 1, "test@gmail.com");
            this._contextUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            Logger.Logger.Instance.Severity = Logger.LogSeverity.Debug;
            Logger.Logger.Instance.Attach(new Logger.ObserverLogToFile("AWSServiceHostTestDebugging.txt"));
        }

        [TestMethod]
        public void BO_TestMethod1()
        {
            UserIdentity newUserIdentity = new UserIdentity(1, "Amezon", "AAAAAAA");

            Assert.AreEqual(1, newUserIdentity.UserId);
            Assert.AreEqual("Amezon", newUserIdentity.Provider);
            Assert.AreEqual("AAAAAAA", newUserIdentity.Identifier);
            Assert.IsTrue(newUserIdentity.Validate());

        }

        [TestMethod]
        public void BO_TestMethod2()
        {
            UserIdentity newUserIdentity = new UserIdentity(1, "", "AAAAAAA");

            Assert.IsFalse(newUserIdentity.Validate());

            newUserIdentity.Provider = "P";
            Assert.IsFalse(newUserIdentity.Validate());

            newUserIdentity.Provider = "Provider";
            Assert.IsTrue(newUserIdentity.Validate());
     
            newUserIdentity.Provider = "#@!A";
            Assert.IsFalse(newUserIdentity.Validate());

            newUserIdentity.Identifier = "";
            Assert.IsFalse(newUserIdentity.Validate());

            newUserIdentity.Identifier = "qwe";
            Assert.IsFalse(newUserIdentity.Validate());

        }
        [TestMethod]
        public void DA_Method1()
        {

            string identity = "identity1";
            string provider = "provider1";
            int uId = 999;
            int test_Identity = 0;

            test_Identity = DataAccess.UserIdentityDao.Insert(uId, identity, provider, this._contextUser);

            UserIdentity test = DataAccess.UserIdentityDao.GetUserIdentity(uId, provider);
            Assert.IsNotNull(test);
            Assert.AreEqual(identity, test.Identifier);
            Assert.AreEqual(provider, test.Provider);

            identity = "identity2";
            DataAccess.UserIdentityDao.Update(uId, identity, provider, this._contextUser);
            UserIdentity test2 = DataAccess.UserIdentityDao.GetUserIdentity(uId, provider);
            Assert.IsNotNull(test2);
            Assert.AreEqual(identity, test2.Identifier);
            Assert.AreEqual(provider, test2.Provider);

            DataAccess.UserIdentityDao.Delete(uId, provider, this._contextUser);
            UserIdentity test3 = DataAccess.UserIdentityDao.GetUserIdentity(uId, provider);
            Assert.IsNull(test3);


        }

        [TestMethod]
        public void DA_Method2()
        {
            int id = 1; 

            IList<UserIdentity> test = DataAccess.UserIdentityDao.GetUserIdentitiesByUserId(id);

            if (test.Count <= 0) { Assert.Fail("Failed to find users linked to the user identity"); }
        }

        [TestMethod]
        public void DA_Method3()
        {

            IList<UserIdentity> test = DataAccess.UserIdentityDao.GetAllUserIdentities();

            if (test.Count <= 0) { Assert.Fail("Failed to find users linked to the user identity"); }
        }
    }
}
