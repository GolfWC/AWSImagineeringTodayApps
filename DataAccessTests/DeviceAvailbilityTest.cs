using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using BusinessLogic;
using DataObjects;

namespace DataAccessTests
{
    [TestClass]
    public class DeviceAvailbilityTest
    {
        private User _contextUser;

        [TestInitialize]
        public void InitTestVars()
        {
            this._contextUser = new User(4, "John", "Public", 1, "test@gmail.com");
            this._contextUser.CurrentIdentityUid = "fwFUrEorXDNTV3dRQ9fcZSyZl062";
            UserAdministration.Fill_UserPolicys(this._contextUser);
            //this._contextUser.Subscriptions.Add(new Subscription(0, 'c', 0, 1, "AppID", "1"));
            Logger.Logger.Instance.Severity = Logger.LogSeverity.Debug;
            Logger.Logger.Instance.Attach(new Logger.ObserverLogToFile("AWSTestDebugging.txt"));

            Logger.Logger.Instance.Debug("Test Logger");
        }
        
        [TestMethod]
        public void BO_TestMethod1()
        {
            int deviceID = 1;
            string serialNumber = "number01number01number01number01";
            int deviceTypeID= 1;
            bool isProvision= true;
            DeviceAvailbility testDevicAvailbility = new DeviceAvailbility( deviceID,  serialNumber, deviceTypeID, isProvision);

            Assert.AreEqual(deviceID, testDevicAvailbility.DeviceId);
            Assert.AreEqual(serialNumber, testDevicAvailbility.DeviceSerialNumber);
            Assert.AreEqual(deviceTypeID, testDevicAvailbility.DeviceTypeID);
            Assert.AreEqual(isProvision, testDevicAvailbility.IsProvisioned);
            Assert.IsTrue(testDevicAvailbility.Validate());
        }
        
        [TestMethod]
        public void BO_TestMethod2()
        {
            int deviceID = 22;
            string serialNumber = "n";
            int deviceTypeID = 2;
            bool isProvision = true;
            DeviceAvailbility testDevicAvailbility = new DeviceAvailbility(deviceID, serialNumber, deviceTypeID, isProvision);

            Assert.AreEqual(deviceID, testDevicAvailbility.DeviceId);
            Assert.AreEqual(serialNumber, testDevicAvailbility.DeviceSerialNumber);
            Assert.AreEqual(deviceTypeID, testDevicAvailbility.DeviceTypeID);
            Assert.AreEqual(isProvision, testDevicAvailbility.IsProvisioned);
            Assert.IsFalse(testDevicAvailbility.Validate());

        }
        
        [TestMethod]
        public void DA_TestMethod1()
        {
            
            string serialNumber = "number02number02number02number02";
            int deviceTypeID = 2;
            bool isProvision = true;
            int testDeviceAvb = 0;
            testDeviceAvb = DataAccess.DeviceAvailbilityDao.Insert(new DeviceAvailbility(0, serialNumber, deviceTypeID, isProvision), _contextUser);

            this.Return_Devices(testDeviceAvb, serialNumber, deviceTypeID, isProvision);
          

            
            string serialNumber2 = "number03number03number03number03";
            int deviceTypeID2 = 3;
            bool isProvision2 = true;
  
            DataAccess.DeviceAvailbilityDao.Update(new DeviceAvailbility(testDeviceAvb, serialNumber2, deviceTypeID2, isProvision2), _contextUser);
            this.Return_Devices(testDeviceAvb, serialNumber2, deviceTypeID2, isProvision2);


            DataAccess.DeviceAvailbilityDao.Delete(testDeviceAvb, _contextUser);
            DeviceAvailbility test3 = DataAccess.DeviceAvailbilityDao.GetDeviceByDeviceId(testDeviceAvb, this._contextUser.AppIds);
            Assert.IsNull(test3);
        }

        [TestMethod]
        public void DA_TestMethod2()
        {
            IList<DeviceAvailbility> test = DataAccess.DeviceAvailbilityDao.GetAllDevices();
            if (test.Count == 0) Assert.Fail("No such a groupdecice");
        }
        [TestMethod]
        public void DA_TestMethod3()
        {
            string sn = "HY-SN002";
            DeviceAvailbility test = DataAccess.DeviceAvailbilityDao.GetDeviceBySerialNumber(sn, this._contextUser.AppIds);
            Assert.IsNotNull(test);
            Assert.AreEqual(sn, test.DeviceSerialNumber);
        }

        [TestMethod]
        public void DA_TestMethod4()
        {
            int groupid = 1;
            string sn = "HY-SN002";
            IList<DeviceAvailbility> test = DataAccess.DeviceAvailbilityDao.GetDevicesByGroupId(groupid);

            bool found = false;
            foreach(DeviceAvailbility d in test)
            {
                if(d.DeviceSerialNumber == sn)
                {
                    found = true; break;
                }
            }

            Assert.IsTrue(found);
        }

        [TestMethod]
        public void DA_TestMethod5()
        {
            int appId = 1;
            string sn = "HY-SN002";
            IList<DeviceAvailbility> test = DataAccess.DeviceAvailbilityDao.GetAllAvalibleDevicesByAppId(appId);
            bool found = false;
            foreach (DeviceAvailbility d in test)
            {
                if (d.DeviceSerialNumber == sn)
                {
                    found = true; break;
                }
            }

            Assert.IsTrue(found);

        }

        private void Return_Devices(int id, string serialNumber, int deviceTypeID, bool isProvision)
        {
            IList<DeviceAvailbility> list = DataAccess.DeviceAvailbilityDao.GetAllDevices();
            foreach( DeviceAvailbility d in list)
            {
                if (d.DeviceId == id)
                {
                    Assert.AreEqual(id, d.DeviceId);
                    Assert.AreEqual(serialNumber, d.DeviceSerialNumber);
                    Assert.AreEqual(deviceTypeID, d.DeviceTypeID);
                    Assert.AreEqual(isProvision, d.IsProvisioned);
                    return;
                }
            }

            Assert.Fail("Fail to find device by ID");

        }
    }
}