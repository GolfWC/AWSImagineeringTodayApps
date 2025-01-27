using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class SubscriptionDefsTest
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

            BusinessObjects.Type deviceType = new BusinessObjects.Type(typeId, typeName, description);

            Assert.AreEqual(typeId, deviceType.TypeId);
            Assert.AreEqual(typeName, deviceType.TypeName);
            Assert.AreEqual(description, deviceType.Description);
        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int typeId;
            string typeName = "Subscription 1";
            string description = "This is a test of the type poco object";

            typeId = DataAccess.SubscriptionDefinitionDao.Insert(new Type(0, typeName, description), _contextUser);

            BusinessObjects.Type test = DataAccess.SubscriptionDefinitionDao.GetSubscriptionById(typeId);
            Assert.AreEqual(typeId, test.TypeId);
            Assert.AreEqual(typeName, test.TypeName);
            Assert.AreEqual(description, test.Description);

            string typeName2 = "Subscription 22";
            string description2 = "This is a test of the type poco object 33";

            DataAccess.SubscriptionDefinitionDao.Update(new Type(typeId, typeName2, description2), _contextUser);

            BusinessObjects.Type test2 = DataAccess.SubscriptionDefinitionDao.GetSubscriptionById(typeId);
            Assert.AreEqual(typeId, test2.TypeId);
            Assert.AreEqual(typeName2, test2.TypeName);
            Assert.AreEqual(description2, test2.Description);

            DataAccess.SubscriptionDefinitionDao.Delete(typeId, _contextUser);
            BusinessObjects.Type test3 = DataAccess.SubscriptionDefinitionDao.GetSubscriptionById(typeId);
            Assert.IsNull(test3);
        }
    }
}
