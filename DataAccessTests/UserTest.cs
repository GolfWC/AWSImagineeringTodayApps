using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class UserTest
    {
        private User _contextUser;

       [TestInitialize] 
        public void InitTestVars()
        {
            this._contextUser = new User(1, "John", "Public", 1, "jhon@gmail.com");
            this._contextUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            Logger.Logger.Instance.Severity = Logger.LogSeverity.Debug;
            Logger.Logger.Instance.Attach(new Logger.ObserverLogToFile("AWSTestDebugging.txt"));
        }

        [TestMethod]
        public void BO_TestMethod1()
        {
            User testUser = new User(1, "First", "Last", 2, "First@gmail.com");
            testUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            testUser.Groups = new List<Group>();

            Assert.AreEqual(1, testUser.UserId);
            Assert.AreEqual("First", testUser.FirstName);
            Assert.AreEqual("Last", testUser.LastName);
            Assert.AreEqual("fwFUrEorXDNTV3dRQ9fcZSyZl062", testUser.CurrentIdentityUid);
            Assert.AreEqual(2, testUser.CustomerId);
            Assert.AreEqual(0, testUser.Groups.Count);
            Assert.AreEqual("first@gmail.com", testUser.EmailAddress);
            Assert.IsTrue(testUser.Validate());
        }

        [TestMethod]
        public void BO_TestMethod2()
        {
            User testUser = new User(1, "", "Last", 2, "test@gmail.com");
            testUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            testUser.Groups = new List<Group>();

            Assert.IsFalse(testUser.Validate());

            testUser.FirstName = "t";
            Assert.IsFalse(testUser.Validate());

            testUser.FirstName = "Tom";
            Assert.IsTrue(testUser.Validate());

            testUser.LastName = "T";
            Assert.IsFalse(testUser.Validate());

            testUser.LastName = "Williams";
            Assert.IsTrue(testUser.Validate());

            testUser.EmailAddress = "";
            Assert.IsFalse(testUser.Validate());

            testUser.EmailAddress = "test@gmail.com";
            Assert.IsTrue(testUser.Validate());

        }

        [TestMethod]
        public void BO_TestMethod3()
        {
            User testUser = new User(1, "", "Last", 0, "");
            testUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            testUser.Groups = new List<Group>();

            Assert.IsFalse(testUser.Validate());
        }

        [TestMethod]
        public void DA_Method1()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int companyId = 1;
            string first = "Sam";
            string last = "Unit Test";
            int test_uid = 0;
            string idtyUid = "66-fwFUrEorXDNTV3dRQ9fcZSyZl062" + rand.ToString();
            string email = "test1@gmail.com";
            string photo = "TestPhoto.jpg";

            //1 insert
            User newUser = new User(0, first, last, companyId, email);
            newUser.CurrentIdentityUid = idtyUid;
            test_uid = DataAccess.UserDao.Insert(newUser, this._contextUser);
            //2 select
            User test = DataAccess.UserDao.GetUserById(test_uid);
            Assert.IsNotNull(test);
            Assert.AreEqual(test_uid, test.UserId);
            Assert.AreEqual(companyId, test.CustomerId);
            Assert.AreEqual(first, test.FirstName);
            Assert.AreEqual(last, test.LastName);
            Assert.AreEqual(email, test.EmailAddress);

            //3 update
            first = "Bill";
            last = "O'Public";
            email = "test2@gmail.com";


            User newUser1 = new User(test_uid, first, last, companyId, email);
            newUser1.CurrentIdentityUid = idtyUid;
            newUser1.Photo = photo;
            DataAccess.UserDao.Update(newUser1, this._contextUser);

            
            //4 select
            User test1 = DataAccess.UserDao.GetUserById(test_uid);
            Assert.IsNotNull(test1);
            Assert.AreEqual(test_uid, test1.UserId);
            Assert.AreEqual(companyId, test1.CustomerId);
            Assert.AreEqual(first, test1.FirstName);
            Assert.AreEqual(last, test1.LastName);
            Assert.AreEqual(email, test1.EmailAddress);
            Assert.AreEqual(photo, test1.Photo);

            //5 delete
            DataAccess.UserDao.Delete(test_uid, this._contextUser);
            //6 select
            User test2 = DataAccess.UserDao.GetUserById(test_uid);
            Assert.IsNull(test2);
        }

        [TestMethod]
        public void DA_Method2()
        {
            int gid = 1; //TODO get a current ID that is in the table

            IList<User> test = DataAccess.UserDao.GetAllUsersByGroupId(gid);

            if(test.Count <= 0) { Assert.Fail("Failed to find users linked to the group id"); }
        }

        [TestMethod]
        public void DA_Method3()
        {
            int cid = 1;

            IList<User> test = DataAccess.UserDao.GetAllUsersByCustomerId(cid);

            if (test.Count <= 0) { Assert.Fail("Failed to find users linked to the customer id"); }
        }

        [TestMethod]
        public void DA_Method4()
        {
            IList<User> test = DataAccess.UserDao.GetAllUsers();
            if (test.Count <= 0) { Assert.Fail("Failed to find users linked to the customer id"); }
        }

        [TestMethod]
        public void DA_Method5()
        {
            string idtyUid = "fGZHGvS7GsYCr0pH78rNcE0PIPD2";
            string idtyProvider = "firebase";

            User test = DataAccess.UserDao.GetUserByIdentifier(idtyUid, idtyProvider);

            Assert.IsNotNull(test);
            Assert.AreEqual(idtyUid, test.CurrentIdentityUid);
        }
    }
}