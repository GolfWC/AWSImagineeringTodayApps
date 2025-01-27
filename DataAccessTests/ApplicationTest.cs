using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessObjects;
using DataObjects;


namespace DataAccessTests
{
    [TestClass]
    public class ApplicationTest
    {
        private readonly TestInit Init = new TestInit(); //TODO have golf refactor all tests to use this object to simplify code changes

        [TestInitialize]
        public void InitTestVars() { }

        [TestMethod]
        public void BO_TestMedthod1()
        {
            int testID = 1;
            string testName = "App Name 1";
            Application testAppication = new Application(testID, testName);

            int deviceGroupID = 1;
            int appID = 2;
            int companyID = 4;
            string deviceGroupName = "GroupTest 1";

            DeviceGroup testGroup = new DeviceGroup( deviceGroupID,  appID,  companyID, deviceGroupName);

            testAppication.DeviceGroups.Add(testGroup);

            Assert.AreEqual(testID, testAppication.AppID);
            Assert.AreEqual(testName, testAppication.AppName);
            Assert.AreEqual("appname1", testAppication.RouteURL);

            if(testAppication.DeviceGroups.Count != 1)
            {
                Assert.Fail("The message is fail ");
            }
            else
            {
                Assert.AreEqual(deviceGroupID, testAppication.DeviceGroups[0].DeviceGroupID);
                Assert.AreEqual(appID, testAppication.DeviceGroups[0].AppID);
                Assert.AreEqual(companyID, testAppication.DeviceGroups[0].CompanyID);
                Assert.AreEqual(deviceGroupName, testAppication.DeviceGroups[0].DeviceGroupName);
            }

            Assert.IsTrue(testAppication.Validate());
        }
        
        [TestMethod]
        public void BO_TestMethod2()
        {
            int testID = 1;
            string testName = "n";
            Application testAppication = new Application(testID, testName);

            int deviceGroupID = 1;
            int appID = 2;
            int companyID = 4;
            string deviceGroupName = "GroupTest 1";

            DeviceGroup testGroup = new DeviceGroup(deviceGroupID, appID, companyID, deviceGroupName);

            testAppication.DeviceGroups.Add(testGroup);

            Assert.AreEqual(testID, testAppication.AppID);
            Assert.AreEqual(testName, testAppication.AppName);

            if (testAppication.DeviceGroups.Count != 1)
            {
                Assert.Fail("The message is fail ");
            }
            else
            {
                Assert.AreEqual(deviceGroupID, testAppication.DeviceGroups[0].DeviceGroupID);
                Assert.AreEqual(appID, testAppication.DeviceGroups[0].AppID);
                Assert.AreEqual(companyID, testAppication.DeviceGroups[0].CompanyID);
                Assert.AreEqual(deviceGroupName, testAppication.DeviceGroups[0].DeviceGroupName);
            }

            Assert.IsFalse(testAppication.Validate());
        }

        [TestMethod]
        public void DA_TestMethod1()
        {
            string appName = "AppName 1";
            int testAppID = 0;
            testAppID = DataAccess.ApplicationDao.Insert(new Application(0, appName), this.Init.ContextUser);

            Application test = DataAccess.ApplicationDao.GetApplicationByAppId(testAppID);
            Assert.AreEqual(appName, test.AppName);

            string appName2 = "NewApp 2";
            DataAccess.ApplicationDao.Update(new Application(testAppID, appName2), this.Init.ContextUser);

            Application test2 = DataAccess.ApplicationDao.GetApplicationByAppId(testAppID);
            Assert.AreEqual(testAppID, test2.AppID);
            Assert.AreEqual(appName2, test2.AppName);

            DataAccess.ApplicationDao.Delete(testAppID, this.Init.ContextUser);
            Application test3 = DataAccess.ApplicationDao.GetApplicationByAppId(testAppID);
            Assert.IsNull(test3);
        }

        [TestMethod]
        public void DA_Testmethod2()
        {
            IList<int> appID = new List<int>();
            appID.Add(1);
            appID.Add(2);
            appID.Add(4);

            IList<Application> testList = DataAccess.ApplicationDao.GetApplicationsByAppIds(appID);
            Assert.AreEqual(appID.Count, testList.Count);

        }

        [TestMethod]
        public void DA_Testmethod3()
        {
            int appId = 999;
            int deviceTypeId = 5;

            DataAccess.ApplicationDao.AddDeviceTypeToApp(appId, deviceTypeId, this.Init.ContextUser);

            IList<DeviceType> test = DataAccess.DeviceTypeDao.GetAllDeviceTypesByAppId(appId);
            
            bool found = false;
            foreach(BusinessObjects.Type t in test)
            {
                if(t.TypeId == deviceTypeId)
                {
                    found = true; break;
                }
            }

            Assert.IsTrue(found);

            DataAccess.ApplicationDao.RemoveDeviceTypeFromApp(appId, deviceTypeId, this.Init.ContextUser);

            IList<DeviceType> test2 = DataAccess.DeviceTypeDao.GetAllDeviceTypesByAppId(appId);

            found = false;
            foreach (BusinessObjects.Type t in test2)
            {
                if (t.TypeId == deviceTypeId)
                {
                    found = true; break;
                }
            }

            Assert.IsFalse(found);
        }
    }
}