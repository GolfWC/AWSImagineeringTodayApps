using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class DeviceTypeTest
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
            string typeName = "Test Device";
            string description = "This is a test of the type poco object";
            string prefix = "TEST";
            
            DeviceType deviceType = new DeviceType(typeId, typeName, description, prefix);

            Assert.AreEqual(typeId, deviceType.TypeId);
            Assert.AreEqual(typeName, deviceType.TypeName);
            Assert.AreEqual(description, deviceType.Description);
            Assert.AreEqual(prefix, deviceType.Prefix);
        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            int typeId;
            string typeName = "Test Device";
            string description = "This is a test of the type poco object";
            string prefix = "TEST";

            typeId = DataAccess.DeviceTypeDao.Insert(new DeviceType(0, typeName, description,prefix), _contextUser);

            DeviceType test = DataAccess.DeviceTypeDao.GetTypesById(typeId);
            Assert.AreEqual(typeId, test.TypeId);
            Assert.AreEqual(typeName, test.TypeName);
            Assert.AreEqual(description, test.Description);
            Assert.AreEqual(prefix, test.Prefix);

            string typeName2 = "Test Device 22";
            string description2 = "This is a test of the type poco object 33";
            string prefix2 = "TEST2";

            DataAccess.DeviceTypeDao.Update(new DeviceType(typeId, typeName2, description2, prefix2), _contextUser);

            DeviceType test2 = DataAccess.DeviceTypeDao.GetTypesById(typeId);
            Assert.AreEqual(typeId, test2.TypeId);
            Assert.AreEqual(typeName2, test2.TypeName);
            Assert.AreEqual(description2, test2.Description);
            Assert.AreEqual(prefix2, test2.Prefix);

            DataAccess.DeviceTypeDao.Delete(typeId, _contextUser);
            DeviceType test3 = DataAccess.DeviceTypeDao.GetTypesById(typeId);
            Assert.IsNull(test3);
        }

        [TestMethod]
        public void DA_TestMethod2()
        {
            IList<DeviceType> test = DataAccess.DeviceTypeDao.GetAllDeviceTypesByAppId(1);

            if(test.Count == 0)
            {
                Assert.Fail("No device types found for this app id");
            }
        }

        [TestMethod]
        public void DA_TestMethod3()
        {
            IList<DeviceType> test = DataAccess.DeviceTypeDao.GetAllTypes();

            if (test.Count == 0)
            {
                Assert.Fail("No device types found in the device table");
            }
        }
    }
}