using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class PolicyTest
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

            Policy policyTest = new Policy(1,1,"entityTypeTest",1,"policyValueTest");
            Assert.AreEqual(1, policyTest.PolicyId);
            Assert.AreEqual(1,policyTest.PolicyDefId);
            Assert.AreEqual("entityTypeTest", policyTest.EntityType);
            Assert.AreEqual(1, policyTest.EntityValue);
            Assert.AreEqual("policyValueTest", policyTest.PolicyValue1);
         
        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int policyId;
            int policyDefId = 2;
            string entityType = "T";
            int entityValue = 99;
            string policyValue1 = "policy value 1";

            policyId = DataAccess.PolicyDao.Insert(new Policy(0,policyDefId,entityType,entityValue,policyValue1), _contextUser);

            Policy test = DataAccess.PolicyDao.GetPolicyById(policyId);
            Assert.AreEqual(policyId, test.PolicyId);
            Assert.AreEqual(policyDefId, test.PolicyDefId);
            Assert.AreEqual(entityType, test.EntityType);
            Assert.AreEqual(entityValue, test.EntityValue);
            Assert.AreEqual(policyValue1, test.PolicyValue1);

            int policyDefId2 = 2;
            string entityType2 = "P";
            int entityValue2 = 99;
            string policyValue1_2 = "policy value 1";

            DataAccess.PolicyDao.Update(new Policy(policyId, policyDefId2, entityType2, entityValue2, policyValue1_2), _contextUser);

            Policy test2 = DataAccess.PolicyDao.GetPolicyById(policyId);
            Assert.AreEqual(policyId, test2.PolicyId);
            Assert.AreEqual(policyDefId2, test2.PolicyDefId);
            Assert.AreEqual(entityType2, test2.EntityType);
            Assert.AreEqual(entityValue2, test2.EntityValue);
            Assert.AreEqual(policyValue1_2, test2.PolicyValue1);


            DataAccess.PolicyDao.Delete(policyId, _contextUser);
            Policy test3 = DataAccess.PolicyDao.GetPolicyById(policyId);
            Assert.IsNull(test3);
        }

        [TestMethod]
        public void DA_TestMethod2()
        {
            int uid = 5;
            int policyDefId = 4;

            IList<Policy> policies = DataAccess.PolicyDao.GetPoliciesByUid(uid);

            if(policies.Count == 0)
            {
                Assert.Fail("No record found");
            }
            else
            {
                bool found = false;
               foreach(Policy p in policies)
                {
                    if(p.PolicyDefId == policyDefId && p.EntityValue == uid)
                    {
                        found = true;
                        break;
                    } 
                }
                Assert.IsTrue(found);
            }

        }

    }
}
