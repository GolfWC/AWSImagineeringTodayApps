using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class PolicyDefinitionTest
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
            string typeName = "policyDefinition";
            string description = "This is a test of the type poco object";

            BusinessObjects.Type policyDefinition = new BusinessObjects.Type(typeId, typeName, description);


            Assert.AreEqual(typeId, policyDefinition.TypeId);
            Assert.AreEqual(typeName, policyDefinition.TypeName);
            Assert.AreEqual(description, policyDefinition.Description);

        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int typeId;
            string typeName = "nametest";
            string description = "platformSubscription description";

            typeId = DataAccess.PolicyDefinitionDao.Insert(new Type(0, typeName, description), _contextUser);

            BusinessObjects.Type test = DataAccess.PolicyDefinitionDao.GetPolicyDefById(typeId);
            Assert.AreEqual(typeName, test.TypeName);



            string typeName2 = "nametest2";
            string description2 = "This is a test of description2";

            DataAccess.PolicyDefinitionDao.Update(new Type(typeId, typeName2, description2), _contextUser);

            BusinessObjects.Type test2 = DataAccess.PolicyDefinitionDao.GetPolicyDefById(typeId);
            Assert.AreEqual(typeName2, test2.TypeName);

            DataAccess.PolicyDefinitionDao.Delete(typeId, _contextUser);
            BusinessObjects.Type test3 = DataAccess.PolicyDefinitionDao.GetPolicyDefById(typeId);
            Assert.IsNull(test3);
        }

    }
}
