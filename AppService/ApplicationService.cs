using System;
using System.Collections.Generic;
using DataObjects;
using BusinessObjects;
using ServiceObject.DtoMapper;
using ServiceObject.Messages;
using System.Linq;

namespace ServiceObject
{ 
    public class ApplicationService
    {
        public ApplicationResponse get_AppsForCurrentUser(User contextUser)
        {
            return get_AppsForCurrentUser(null, contextUser);
        }

        public ApplicationResponse get_AppsForCurrentUser(ApplicationRequest request, User contextUser)
        {
            ApplicationResponse response = new ApplicationResponse();

            try
            {
                IList<Application> list = DataAccess.ApplicationDao.GetApplicationsByAppIds(contextUser.AppIds);
                if (request != null)
                {
                    Fill_loadingOptions(request.LoadOptions, list);
                }
                response.Applications = ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Fatal("Exception thrown in Groups Service, get_AppsForCurrentUser() ", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;
        }

        public static void Fill_loadingOptions(string[] LoadOptions, IList<Application> application)
        {
            if (LoadOptions == null) { return; }

            if (LoadOptions.Contains("devices"))
            {
                foreach (Application app in application)
                {
                    app.Devices = DataAccess.DeviceAvailbilityDao.GetAllAvalibleDevicesByAppId(app.AppID);
                    DeviceService.Fill_loadingOptions(LoadOptions, app.Devices);
                }
            }
        }
    }
}