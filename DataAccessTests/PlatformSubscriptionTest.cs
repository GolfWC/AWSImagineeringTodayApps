using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class PlatformSubscriptionTest
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

            PlatformSubscription testPlatformSubscription = new PlatformSubscription(1, "entity_Type1", 1, 99,"val");

            Assert.AreEqual(1, testPlatformSubscription.SubId);
            Assert.AreEqual("entity_Type1", testPlatformSubscription.EntityType);
            Assert.AreEqual(1, testPlatformSubscription.EntityValue);
            Assert.AreEqual(99, testPlatformSubscription.SubdefId);
            Assert.AreEqual("val", testPlatformSubscription.SubValue);


        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int subId;
            string entityType = "T";
            int entityValue = 1;
            int subDefId = 99;
            string subValue = "sub";


            subId = DataAccess.PlatformSubscriptionsDao.Insert(new PlatformSubscription(0,entityType,entityValue,subDefId,subValue), _contextUser);

            PlatformSubscription test = DataAccess.PlatformSubscriptionsDao.GetSubscriptionById(subId);
            Assert.AreEqual(subId, test.SubId);
            Assert.AreEqual(entityType, test.EntityType);
            Assert.AreEqual(entityValue, test.EntityValue);
            Assert.AreEqual(subDefId, test.SubdefId);


            string entityType2 = "P";
            int entityValue2 = 2;
            int subDefId2 = 88;
            string subValue2 = "sub2";


            DataAccess.PlatformSubscriptionsDao.Update(new PlatformSubscription(subId, entityType2, entityValue2, subDefId2, subValue2), _contextUser);

            PlatformSubscription test2 = DataAccess.PlatformSubscriptionsDao.GetSubscriptionById(subId);
            Assert.AreEqual(subId, test2.SubId);
            Assert.AreEqual(entityType2, test2.EntityType);
            Assert.AreEqual(entityValue2, test2.EntityValue);
            Assert.AreEqual(subDefId2, test2.SubdefId);
            Assert.AreEqual(subValue2, test2.SubValue);

            DataAccess.PlatformSubscriptionsDao.Delete(subId, _contextUser);
            PlatformSubscription test3 = DataAccess.PlatformSubscriptionsDao.GetSubscriptionById(subId);
            Assert.IsNull(test3);
        }

        [TestMethod]
        public void DA_TestMethod2()
        {
            IList<PlatformSubscription> subscriptions = DataAccess.PlatformSubscriptionsDao.GetAllSubscriptions();
            Assert.IsNotNull(subscriptions);

            if(subscriptions.Count < 1)
            {
                Assert.Fail("No reccord found in database");
            }

        }


    }
}
