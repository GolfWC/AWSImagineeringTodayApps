using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class TagTypeDaoTest
    {
        private  User _contextUser;

        [TestInitialize]
            public void InitTestVars()
        {
            this._contextUser = new User(1, "John", "Public", 1, "test@gmail.com");
            this._contextUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            Logger.Logger.Instance.Severity = Logger.LogSeverity.Debug;
            Logger.Logger.Instance.Attach(new Logger.ObserverLogToFile("AWSTestDebugging.txt"));
        }

        [TestMethod]
        public void BO_testMethod1()
        {
            Type testType = new Type( 0, "test1", "test Tag1");

            Assert.AreEqual(0, testType.TypeId);
            Assert.AreEqual("test1", testType.TypeName);
            Assert.AreEqual("test Tag1", testType.Description);
            

        }

      

        [TestMethod]
        public void DA_testMethod1()
        {
            
            int typeId;
            string typeName = "name1";
            string description = "This is a test of tagType";
           
            typeId = DataAccess.TagTypeDao.Insert(new Type (0, typeName, description), _contextUser);

            BusinessObjects.Type test = DataAccess.TagTypeDao.GetTagTypeById(typeId);
            Assert.AreEqual(typeName, test.TypeName);


            string typeName2 = "name2";
            string description2 = "This is a test of tagType";

            DataAccess.TagTypeDao.Update(new Type(typeId, typeName2, description2), _contextUser);

            BusinessObjects.Type test2 = DataAccess.TagTypeDao.GetTagTypeById(typeId);
            Assert.AreEqual(typeName2, test2.TypeName);

            DataAccess.TagTypeDao.Delete(typeId, _contextUser);
            BusinessObjects.Type test3 = DataAccess.TagTypeDao.GetTagTypeById(typeId);
            Assert.IsNull(test3);
        }

    }
}
