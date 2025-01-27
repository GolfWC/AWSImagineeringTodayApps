using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;


namespace DataAccessTests
{
    [TestClass]
    public class TagCloudDaoTest
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
            Tag testTag = new Tag(0, "test1", "test Tag1", 1);

            Assert.AreEqual(0, testTag.TagId);
            Assert.AreEqual("test1", testTag.TagName);
            Assert.AreEqual("test Tag1", testTag.Description);
            Assert.AreEqual(1, testTag.CustomerId);
        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int tagId;
            string tagName = "name1";
            string description = "This is a test of tagType";
            int customerId = 1;

            tagId = DataAccess.TagCloudDao.Insert(new Tag(0, tagName, description, customerId), _contextUser);

            BusinessObjects.Tag test = DataAccess.TagCloudDao.GetTagByTagId(tagId);
            Assert.AreEqual(tagId, test.TagId);
            Assert.AreEqual(tagName, test.TagName);
            Assert.AreEqual(description, test.Description);
            Assert.AreEqual(customerId, test.CustomerId);

            string tagName2 = "name 2";
            string description2 = "This is a test of tagType 2";
            int customerId2 = 2;

            DataAccess.TagCloudDao.Update(new Tag(tagId, tagName2, description2, customerId2), _contextUser);

            BusinessObjects.Tag test2 = DataAccess.TagCloudDao.GetTagByTagId(tagId);
            Assert.AreEqual(tagName2, test2.TagName);
            Assert.AreEqual(description2, test2.Description);
            Assert.AreEqual(customerId2, test2.CustomerId);

            DataAccess.TagCloudDao.Delete(tagId, _contextUser);
            BusinessObjects.Tag test3 = DataAccess.TagCloudDao.GetTagByTagId(tagId);
            Assert.IsNull(test3);
        }
        
        [TestMethod]
        public void DA_TestMethod2()
        {
            int tagId = 1;
            int entityId = 999;
            int mappedToId = 999;

            DataAccess.TagCloudDao.AddMapTag(tagId, entityId, mappedToId, _contextUser);
            IList<Tag> testList = DataAccess.TagCloudDao.GetTagByMappedToId(mappedToId);

            if (testList.Count == 0)
            {
                Assert.Fail("No reccord for add mapTag function found");
            }
            bool found = false;
            foreach (Tag t in testList)
            {
                if (t.TagId == tagId)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found, " Test reccord not found");

            DataAccess.TagCloudDao.RemoveMapTag(tagId, entityId, mappedToId, _contextUser);
            IList<Tag> testList2 = DataAccess.TagCloudDao.GetTagByMappedToId(mappedToId);
            if (testList2.Count > 0)
            {
                Assert.Fail(" Reccord is not removed");
            }
        }

        [TestMethod]
         public void DA_TestMethod3()
        {
            int entityId = 1;
            int customerId = 1;

            IList<Tag> test = DataAccess.TagCloudDao.GetAllTagsForEntity(entityId, customerId);

            if (test.Count == 0)
            {
                Assert.Fail("No reccord Found for entityId");
            }

        }
    
        [TestMethod]
        public void DA_TestMethod4()
        {
            int custumerId = 1;
            IList<Tag> test = DataAccess.TagCloudDao.GetTagSByCustomerId(custumerId);

            if(test.Count == 0)
            {
                Assert.Fail("No reccord Found in the custumer id");
            }

            int count = 0;
            foreach(Tag t in test)
            {
                if(t.CustomerId == custumerId)
                {
                    count++;
                }
            }
            Assert.AreEqual(test.Count, count);
        }

        [TestMethod]
        public void DA_TestMethod5()
        {
            int tagId = 1;
            int entityId = 999;
            int mappedToId = 999;

            bool test = DataAccess.TagCloudDao.mapExist(tagId, entityId, mappedToId);

            Assert.IsFalse(test, "This map was not supposed to be in the database");
        }

        [TestMethod]
        public void DA_TestMethod6()
        {
            int tagId = 1;
            int entityId = 1;
            int mappedToId = 1;

            bool test = DataAccess.TagCloudDao.mapExist(tagId, entityId, mappedToId);

            Assert.IsTrue(test, "Is suppoed to be in the database");
        }
    }   
}