using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class SubscriptionsTest
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
            int subId = 1;
            char accessType = 'c';
            int accessValue = 1;
            int subDefId = 1;
            string subKey = "key";
            string subValue = "value";

            Subscription testSubscripTion = new Subscription(subId, accessType, accessValue, subDefId, subKey, subValue);
            Assert.AreEqual(subId, testSubscripTion.SubId);
            Assert.AreEqual(accessType, testSubscripTion.AccessType);
            Assert.AreEqual(accessValue, testSubscripTion.AccessValue);
            Assert.AreEqual(subDefId, testSubscripTion.DefinitionId);
            Assert.AreEqual(subKey, testSubscripTion.Key);
            Assert.AreEqual(subValue, testSubscripTion.Value);
            Assert.IsTrue(testSubscripTion.Validate());
        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int subId = 1;
            char accessType = 'c';
            int accessValue = 1;
            int subDefId = 1;
            string subKey = "key";
            string subValue = "value";


            int testSubId = DataAccess.SubscriptionsDao.Insert(new Subscription(subId, accessType, accessValue, subDefId, subKey, subValue), _contextUser);
            
            Subscription test = DataAccess.SubscriptionsDao.GetSubscriptionById(testSubId);
            Assert.AreEqual(testSubId, test.SubId);
            Assert.AreEqual(accessType, test.AccessType);
            Assert.AreEqual(accessValue, test.AccessValue);
            Assert.AreEqual(subDefId, test.DefinitionId);
            Assert.AreEqual(subKey, test.Key);
            Assert.AreEqual(subValue, test.Value);

            
            char accessType2 = 'u';
            int accessValue2 = 2;
            int subDefId2 = 3;
            string subKey2 = "keyUpdate";
            string subValue2 = "valueUpdate";
            DataAccess.SubscriptionsDao.Update(new Subscription(testSubId, accessType2, accessValue2, subDefId2, subKey2, subValue2), _contextUser);

            Subscription test2 = DataAccess.SubscriptionsDao.GetSubscriptionById(testSubId);
            Assert.AreEqual(testSubId, test2.SubId);
            Assert.AreEqual(accessType2, test2.AccessType);
            Assert.AreEqual(accessValue2, test2.AccessValue);
            Assert.AreEqual(subDefId2, test2.DefinitionId);
            Assert.AreEqual(subKey2, test2.Key);
            Assert.AreEqual(subValue2, test2.Value);


            DataAccess.SubscriptionsDao.Delete(testSubId, _contextUser);
            Subscription test3 = DataAccess.SubscriptionsDao.GetSubscriptionById(testSubId);
            Assert.IsNull(test3);
        }

        [TestMethod]
        public void DA_TestMethod2()
        {
            
            char accessType = 'c';
            int accessValue = 1;

           IList<Subscription> test = DataAccess.SubscriptionsDao.GetSubscriptionsByAccessLevelId(accessType, accessValue);
           if(test.Count == 0)
            {
                Assert.Fail("Expecting more than one record");
            }

        }
        [TestMethod]
        public void DA_TestMethod3()
        {

            char accessType = 'c';
            IList<int> accessValue = new List<int>();
            accessValue.Add(1);
            accessValue.Add(2);

            IList<Subscription> test = DataAccess.SubscriptionsDao.GetSubscriptionsByAccessLevelIds(accessType, accessValue);
            if (test.Count == 0)
            {
                Assert.Fail("Expecting more than one record");
            }

        }



    }
    
}
