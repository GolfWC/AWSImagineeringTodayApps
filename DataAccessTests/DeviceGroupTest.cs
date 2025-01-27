using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using BusinessLogic;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class DeviceGroupTest
    {
        private User _contextUser;

        [TestInitialize]
        public void InitTestVars()
        {
            this._contextUser = new User(4, "John", "Public", 1, "test@gmail.com");
            this._contextUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            UserAdministration.Fill_UserPolicys(this._contextUser);
            Logger.Logger.Instance.Severity = Logger.LogSeverity.Debug;
            Logger.Logger.Instance.Attach(new Logger.ObserverLogToFile("AWSTestDebugging.txt"));
        }

        [TestMethod]
        public void BO_TestMethod1()
        {
            int deviceGroupID = 1;
            int appID = 2;
            int companyID =4;
            string deviceGroupName = "GroupTest";

            DeviceGroup testDeviceGroup = new DeviceGroup(deviceGroupID, appID, companyID, deviceGroupName);
            Assert.AreEqual(deviceGroupID, testDeviceGroup.DeviceGroupID);
            Assert.AreEqual(appID, testDeviceGroup.AppID);
            Assert.AreEqual(companyID, testDeviceGroup.CompanyID);
            Assert.AreEqual(deviceGroupName, testDeviceGroup.DeviceGroupName);
            Assert.IsTrue(testDeviceGroup.Validate());
        }
        
        [TestMethod]
        public void BO_TestMethod2()
        {
            int deviceGroupID = 1;
            int appID = 2;
            int companyID = 4;
            string deviceGroupName = "G";

            DeviceGroup testDeviceGroup = new DeviceGroup(deviceGroupID, appID, companyID, deviceGroupName);
            Assert.AreEqual(deviceGroupID, testDeviceGroup.DeviceGroupID);
            Assert.AreEqual(appID, testDeviceGroup.AppID);
            Assert.AreEqual(companyID, testDeviceGroup.CompanyID);
            Assert.AreEqual(deviceGroupName, testDeviceGroup.DeviceGroupName);
            Assert.IsFalse(testDeviceGroup.Validate());
        }

        [TestMethod]
        public void DA_TestMethod1()
        {

            int appID = 2;
            int companyID = 4;
            string deviceGroupName = "DeviceGroup!";
            int testDeviceGroupID = 0;

            testDeviceGroupID = DataAccess.DeviceGroupDao.Insert(new DeviceGroup(0, appID, companyID, deviceGroupName), _contextUser);

            DeviceGroup test = DataAccess.DeviceGroupDao.GetDeviceGroupById(testDeviceGroupID, companyID);
            Assert.AreEqual(appID, test.AppID);
            Assert.AreEqual(companyID, test.CompanyID);
            Assert.AreEqual(deviceGroupName, test.DeviceGroupName);

            int appID2 = 22;
            int companyID2 = 44;
            string deviceGroupName2 = "DeviceGroup2";
            DataAccess.DeviceGroupDao.Update(new DeviceGroup(testDeviceGroupID, appID2, companyID2, deviceGroupName2),_contextUser);
            DeviceGroup test2 = DataAccess.DeviceGroupDao.GetDeviceGroupById(testDeviceGroupID, companyID2);
            Assert.AreEqual(appID2, test2.AppID);
            Assert.AreEqual(companyID2, test2.CompanyID);
            Assert.AreEqual(deviceGroupName2, test2.DeviceGroupName);

            DataAccess.DeviceGroupDao.Delete(testDeviceGroupID, _contextUser);
            DeviceGroup test3 = DataAccess.DeviceGroupDao.GetDeviceGroupById(testDeviceGroupID, companyID2);
            Assert.IsNull(test3);
        }  
        
        [TestMethod]
        public void DA_TestMethod2()
        {
            int appID = 1;
            IList<DeviceGroup> test = DataAccess.DeviceGroupDao.GetDeviceGroupsByAppId(appID, this._contextUser.CustomerId);
            if (test.Count == 0) Assert.Fail("No devicegroup found for this app ID");  
        }

        [TestMethod]
        public void DA_TestMedthod3()
        {
            int companyID = 1;
            IList<DeviceGroup> test = DataAccess.DeviceGroupDao.GetDeviceGroupsByCustomerId(companyID);
            if (test.Count == 0) Assert.Fail("No devicegroup found for this company ID");
        }
        
        [TestMethod]
        public void DA_TestMethod4()
        {
            IList<DeviceGroup> test = DataAccess.DeviceGroupDao.GetAllDeviceGroups();
            if (test.Count == 0) Assert.Fail("No group found in database");
        }

        [TestMethod]
        public void DA_TestMethod5()
        {
            int gpId = 1;
            int devId = 999;

            DataAccess.DeviceGroupDao.AddDeviceToGroup(gpId, devId, _contextUser);
            IList<DeviceGroup> test = DataAccess.DeviceGroupDao.GetAllDeviceGroupsByDeviceId(devId, this._contextUser.CustomerId);
            if (test.Count == 0) Assert.Fail("no records returned");

            bool found = false;
            foreach (DeviceGroup g in test)
            {
                if (g.DeviceGroupID == gpId) { found = true; break; }
            }

            Assert.IsTrue(found);

            DataAccess.DeviceGroupDao.RemoveDeviceFromGroup(gpId, devId, _contextUser);

            IList<DeviceGroup> test2 = DataAccess.DeviceGroupDao.GetAllDeviceGroupsByDeviceId(devId, this._contextUser.CustomerId);
            if (test2.Count > 0) Assert.Fail("Device was not removed from the group");
        }

        [TestMethod]
        public void DA_TestMethod6()
        {
            int gpId = 1;
            int devId = 999;

            bool test = DataAccess.DeviceGroupDao.mapExist(gpId, devId);

            Assert.IsFalse(test, "Record should not exist");
        }

        [TestMethod]
        public void DA_TestMethod7()
        {
            int gpId = 1;
            int devId = 1;

            bool test = DataAccess.DeviceGroupDao.mapExist(gpId, devId);

            Assert.IsTrue(test, "Record should exist");
        }

        [TestMethod]
        public void DA_TestMethod8()
        {
            int deviceId = 1;
            IList<DeviceGroup> groups = DataAccess.DeviceGroupDao.GetDeviceGroupsByTypeId(deviceId, _contextUser.CustomerId);
            if(groups.Count < 1)
            {
                Assert.Fail("No any device found ");
            }
        }

        [TestMethod]
        public void DA_TestMethod9()
        {
            int deviceId = 1;
            IList<DeviceGroup> groups = DataAccess.DeviceGroupDao.GetDeviceGroupsByTypeId(deviceId, 999);
            if(groups.Count > 0)
            {
                Assert.Fail("Customer id is not respected to the quary");
            }
        }
    }
}