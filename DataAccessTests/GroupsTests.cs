using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class GroupsTests
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
        public void BO_Constructor()
        {
            int groupId = 1;
            int customerId = 2;
            string GroupName = "My Group Test";

            Group testGroup = new Group(groupId, customerId, GroupName);

            Assert.AreEqual(testGroup.GroupID, groupId);
            Assert.AreEqual(testGroup.CustomerID, customerId);
            Assert.AreEqual(testGroup.GroupName, GroupName);
        }

        [TestMethod]
        public void BO_ValidationIdRulePass()
        {
            int groupId = 1;
            int customerId = 2;
            string GroupName = "My Group Test";

            Group testGroup = new Group(groupId, customerId, GroupName);

            Assert.IsTrue(testGroup.Validate());
        }

        [TestMethod]
        public void BO_ValidationIdRuleFail_1()
        {
            int groupId = 1;
            int customerId = 0;
            string GroupName = "My Group Test";

            Group testGroup = new Group(groupId, customerId, GroupName);

            Assert.IsFalse(testGroup.Validate());
        }

        [TestMethod]
        public void BO_ValidationRequieredRulePass()
        {
            int groupId = 1;
            int customerId = 2;
            string GroupName = "My Group Test";

            Group testGroup = new Group(groupId, customerId, GroupName);

            Assert.IsTrue(testGroup.Validate());
        }

        [TestMethod]
        public void BO_ValidationRequieredRuleFail()
        {
            int groupId = 1;
            int customerId = 2;
            string GroupName = "";

            Group testGroup = new Group(groupId, customerId, GroupName);

            Assert.IsFalse(testGroup.Validate());
        }

        [TestMethod]
        public void DA_Method()
        {
            int companyId = 1;
            string grpupName = "Test Group Sam";
            int new_groupId;

            //1 insert
            new_groupId = DataAccess.GroupsDao.Insert(new Group(0, companyId, grpupName), this._contextUser);
            //2 select
            Group test = DataAccess.GroupsDao.GetGroup(new_groupId);
            Assert.IsNotNull(test);
            Assert.AreEqual(new_groupId, test.GroupID);
            Assert.AreEqual(companyId, test.CustomerID);
            Assert.AreEqual(grpupName, test.GroupName);

            //3 update
            DataAccess.GroupsDao.Update(new Group(new_groupId, companyId, "Test Group Lamda"), this._contextUser);
            //4 select
            Group test1 = DataAccess.GroupsDao.GetGroup(new_groupId);
            Assert.IsNotNull(test1);
            Assert.AreEqual(new_groupId, test1.GroupID);
            Assert.AreEqual(companyId, test1.CustomerID);
            Assert.AreEqual("Test Group Lamda", test1.GroupName);

            //5 delete
            DataAccess.GroupsDao.Delete(new_groupId, this._contextUser);
            //6 select
            Group test2 = DataAccess.GroupsDao.GetGroup(new_groupId);
            Assert.IsNull(test2);
        }

        [TestMethod]
        public void DA_Method2()
        {
            int gpId = 1;
            int uid = 4;
            int cid = 1;

            DataAccess.GroupsDao.AddUserToGroup(uid, gpId, _contextUser);
            IList<Group> test = DataAccess.GroupsDao.GetGroupsByUserId(uid);
            if (test.Count == 0) Assert.Fail("no records returned");

            bool found = false;
            foreach(Group g in test)
            {
                if(g.GroupID == gpId) { found = true; break; }
            }

            Assert.IsTrue(found);

            IList<Group> test2 = DataAccess.GroupsDao.GetGroupsByCustomerId(cid);
            if (test2.Count == 0) Assert.Fail("no records returned");

            DataAccess.GroupsDao.RemoveUserFromGroup(uid, gpId, _contextUser);

            IList<Group> test3 = DataAccess.GroupsDao.GetGroupsByUserId(uid);

            found = false;
            foreach (Group g in test3)
            {
                if (g.GroupID == gpId) { found = true; break; }
            }

            Assert.IsFalse(found);
        }

        [TestMethod]
        public void DA_Method3()
        {
            int uidId = 1;

            IList<Group> test = DataAccess.GroupsDao.GetGroupsByUserId(uidId);
            if (test.Count <= 0) Assert.Fail("No Groups Found for this user id");

        }

        [TestMethod]
        public void DA_Method4()
        {
            int customerId = 1;

            IList<Group> test = DataAccess.GroupsDao.GetGroupsByCustomerId(customerId);
            if (test.Count <= 0) Assert.Fail("No Groups Found for this user id");
        }

        [TestMethod]
        public void DA_Method5()
        {
            IList<Group> test = DataAccess.GroupsDao.GetAllGroups();
            if (test.Count <= 0) Assert.Fail("No Groups Found in database");
        }
    }
}