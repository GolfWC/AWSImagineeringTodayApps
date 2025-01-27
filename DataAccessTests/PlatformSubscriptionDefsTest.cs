using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
   public class PlatformSubscriptionDefsTest
    {
        private User _contextUser;

        [TestInitialize]
        public void InitTestVars()
        {
            this._contextUser = new User(1, "John", "Public", 1, "test@gmail.com");
            this._contextUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            Logger.Logger.Instance.Severity = Logger.LogSeverity.Debug;
            Logger.Logger.Instance.Attach(new Logger.ObserverLogToFile("AWSTestDebugging.txt"));
        }
        [TestMethod]
        public void BO_TestMethod1()
        {
            int typeId = 1;
            string typeName = "Subscription1";
            string description = "This is a test of the type poco object";

            BusinessObjects.Type platformSubscriptionDef = new BusinessObjects.Type(typeId,typeName, description);


            Assert.AreEqual(typeId, platformSubscriptionDef.TypeId);
            Assert.AreEqual(typeName, platformSubscriptionDef.TypeName);
            Assert.AreEqual(description, platformSubscriptionDef.Description);

        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int typeId;
            string typeName = "nametest";
            string description = "platformSubscription description";

            typeId = DataAccess.PlatformSubscriptionDefsDao.Insert(new Type(0, typeName, description), _contextUser);

            BusinessObjects.Type test = DataAccess.PlatformSubscriptionDefsDao.GetPlatformSubDef(typeId);
            Assert.AreEqual(typeName, test.TypeName);



            string typeName2 = "nametest2";
            string description2 = "This is a test of description2";

            DataAccess.PlatformSubscriptionDefsDao.Update(new Type(typeId, typeName2, description2), _contextUser);

            BusinessObjects.Type test2 = DataAccess.PlatformSubscriptionDefsDao.GetPlatformSubDef(typeId);
            Assert.AreEqual(typeName2, test2.TypeName);

            DataAccess.PlatformSubscriptionDefsDao.Delete(typeId, _contextUser);
            BusinessObjects.Type test3 = DataAccess.PlatformSubscriptionDefsDao.GetPlatformSubDef(typeId);
            Assert.IsNull(test3);
        }

    }
}
