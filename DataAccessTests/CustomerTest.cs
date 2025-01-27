using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class CustomerTest
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
            Customer testCustomer = new Customer(1, "Test1");

            Assert.AreEqual(1, testCustomer.CustomerId);
            Assert.AreEqual("Test1", testCustomer.CustomerName);
            Assert.IsTrue(testCustomer.Validate());
        }

        [TestMethod]
        public void BO_TestMethod2()
        {
            Customer testCustomer = new Customer(1, "");

            Assert.IsFalse(testCustomer.Validate());

        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            string companyName = "Company1";
            int testCID = 0;

            testCID = DataAccess.CustomerDao.Insert(new Customer(0, companyName), _contextUser);

            Customer test = DataAccess.CustomerDao.GetCustomer(testCID);

            Assert.AreEqual(companyName, test.CustomerName);

            string companyName2 = "AjeckCompany";
            DataAccess.CustomerDao.Update(new Customer(testCID, companyName2), _contextUser);

            Customer test2 = DataAccess.CustomerDao.GetCustomer(testCID);
            Assert.AreEqual(testCID, test2.CustomerId);
            Assert.AreEqual(companyName2, test2.CustomerName);
            DataAccess.CustomerDao.Delete(testCID, _contextUser);

            Customer test3 = DataAccess.CustomerDao.GetCustomer(testCID);
            Assert.IsNull(test3);
        }
        
        [TestMethod]
        public void DA_TestMethod2()
        {
            IList<Customer> list = DataAccess.CustomerDao.GetAllCustomers();
            if( list.Count == 0)
            {
                Assert.Fail("The getAllMethod return 0");
            }

        }
    }
}
