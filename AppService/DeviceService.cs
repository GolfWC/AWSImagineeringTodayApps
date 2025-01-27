using System;
using System.Collections.Generic;
using System.Linq;

using ServiceObject.Messages;
using BusinessObjects;
using DataObjects;
using BusinessLogic;

namespace ServiceObject
{
    public class DeviceService
    {
        public DeviceResponse set_operatiors(DeviceRequest request, User contextUser)
        {
            DeviceResponse response = new DeviceResponse();

            DeviceAvailbility device = DtoMapper.FromMapper.FromDataTransferObject(request.Device);

            if (request.Action.ToLower() != "delete")
            {
                if(device != null)
                {
                    if (!device.Validate())
                    {
                        response.Acknowledge = MessageBase.AcknowledgeType.Failure;

                        foreach (string error in device.ValidationErrors)
                            response.Message += error + Environment.NewLine;

                        return response;
                    }
                }

            }

            if (request.Action.ToLower() == "create")
            {
                try
                {
                    DeviceAvailbility newDevice = new DeviceAvailbility();

                    if (request.Device != null && request.Criteria.DeviceGroupId > 0)
                    {
                        newDevice = DeviceProvisioner.provision(DtoMapper.FromMapper.FromDataTransferObject(request.Device), request.Criteria.DeviceGroupId, contextUser);
                    }
                    else if (request.Criteria.DeviceTypeId > 0 && request.Criteria.DeviceGroupId > 0)
                    {
                        newDevice = DeviceProvisioner.provision(request.Criteria.DeviceTypeId, request.Criteria.DeviceGroupId, contextUser);
                    }
                    else
                    {
                        throw new Exception("Did not incude a valid device criteria in the request");
                    }

                    if (newDevice.DeviceId > 0)
                    {
                        IList<DeviceAvailbility> list = new List<DeviceAvailbility>();
                        list.Add(newDevice);

                        response.Devices = DtoMapper.ToMapper.ToDataTransferObject(list);
                        response.Acknowledge = MessageBase.AcknowledgeType.Success;
                    }
                }
                catch (Exception e)
                {
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                    response.Message = e.Message;
                }
            }
            else if (request.Action.ToLower() == "update")
            {
                DataAccess.DeviceAvailbilityDao.Update(device, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "delete")
            {
                DataAccess.DeviceGroupDao.RemoveDeviceFromAllGroups(request.Criteria.DeviceId, contextUser);
                DataAccess.DeviceAvailbilityDao.Delete(request.Criteria.DeviceId, contextUser);
                
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else
            {
                response.Message = "Not a valid action: " + request.Action.ToLower();
            }

            return response;
        }

        public DeviceResponse get_DeviceByDeviceId(DeviceRequest request, User contextUser)
        {
            DeviceResponse response = new DeviceResponse();
            try
            {
                IList<DeviceAvailbility> list = new List<DeviceAvailbility>();
                DeviceAvailbility device = DataAccess.DeviceAvailbilityDao.GetDeviceByDeviceId(request.Criteria.DeviceId, contextUser.AppIds);

                if (device != null) 
                { 
                    list.Add(device); 
                    Fill_loadingOptions(request.LoadOptions, list); 
                }

                response.Devices = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserSevice, get_DeviceByDeviceId()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
            
            return response;
        }

        public DeviceResponse get_DeviceBySerialNumber(DeviceRequest request, User contextUser)
        {
            DeviceResponse response = new DeviceResponse();

            try
            {
                IList<DeviceAvailbility> list = new List<DeviceAvailbility>();
                DeviceAvailbility device = DataAccess.DeviceAvailbilityDao.GetDeviceBySerialNumber(request.Criteria.SerialNumber, contextUser.AppIds);

                if (device != null) 
                { 
                    list.Add(device); 
                    Fill_loadingOptions(request.LoadOptions, list); 
                }

                response.Devices = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserSevice, get_DeviceBySerialNumbe()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
            
            return response;
        }

        public DeviceResponse get_DevicesByGroupId(DeviceRequest request, User contextUser)
        {
            DeviceResponse response = new DeviceResponse();

            try
            {
                IList<DeviceAvailbility> devices = DataAccess.DeviceAvailbilityDao.GetDevicesByGroupId(request.Criteria.DeviceGroupId);
                
                Fill_loadingOptions(request.LoadOptions, devices);

                response.Devices = DtoMapper.ToMapper.ToDataTransferObject(devices);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserSevice, get_DeviceBySerialNumbe()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;
        }

        public DeviceResponse get_DevicesByAppId(DeviceRequest request)
        {
            DeviceResponse response = new DeviceResponse();

            try
            {
                IList<DeviceAvailbility> devices = DataAccess.DeviceAvailbilityDao.GetAllAvalibleDevicesByAppId(request.Criteria.AppId);
                Fill_loadingOptions(request.LoadOptions, devices);

                response.Devices = DtoMapper.ToMapper.ToDataTransferObject(devices);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserSevice, get_DevicesByAppId()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;
        }

        public DeviceGroupResponse set_operatiors(DeviceGroupRequest request, User contextUser)
        {
            DeviceGroupResponse response = new DeviceGroupResponse();
            DeviceGroup group = DtoMapper.FromMapper.FromDataTransferObject(request.DeviceGroup);

            if (request.Action.ToLower() == "create" || request.Action.ToLower() == "update")
            {
                
                if (!group.Validate())
                {
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;

                    foreach (string error in group.ValidationErrors)
                        response.Message += error + Environment.NewLine;

                    return response;
                }
            }

            if (request.Action.ToLower() == "create")
            {
                DataAccess.DeviceGroupDao.Insert(group, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "update")
            {
                DataAccess.DeviceGroupDao.Update(group, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "delete")
            {
                DataAccess.DeviceGroupDao.Delete(request.Criteria.DeviceGorupId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "adddevice")
            {
                //TODO add logic to protect the database for duplicate keys
                if (!DataAccess.DeviceGroupDao.mapExist(request.Criteria.DeviceGorupId, request.Criteria.DeviceId))
                {
                    DataAccess.DeviceGroupDao.AddDeviceToGroup(request.Criteria.DeviceGorupId, request.Criteria.DeviceId, contextUser);
                    response.Acknowledge = MessageBase.AcknowledgeType.Success;
                }
            }
            else if (request.Action.ToLower() == "removedevice")
            {
                DataAccess.DeviceGroupDao.RemoveDeviceFromGroup(request.Criteria.DeviceGorupId, request.Criteria.DeviceId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else
            {
                response.Message = "Not a valid action: " + request.Action.ToLower();
            }

            return response;
        }

        public DeviceGroupResponse get_DeviceGroupByAppId(DeviceGroupRequest request, User contextUser)
        {
            DeviceGroupResponse response = new DeviceGroupResponse();
            try
            {
                IList<DeviceGroup> list = DataAccess.DeviceGroupDao.GetDeviceGroupsByAppId(request.Criteria.ApplicationId, contextUser.CustomerId);

                Fill_loadingOptions(request.LoadOptions, list);

                response.DeviceGroups = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserSevice, get_DeviceGroupByAppId()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
            
            return response;
        }

        public DeviceGroupResponse get_DeviceGroupByGroupId(DeviceGroupRequest request, User contextUser)
        {
            DeviceGroupResponse response = new DeviceGroupResponse();
               
            try
            {
                IList<DeviceGroup> list = new List<DeviceGroup>();
                DeviceGroup group = DataAccess.DeviceGroupDao.GetDeviceGroupById(request.Criteria.DeviceGorupId, contextUser.CustomerId);

                if (group != null) 
                { 
                    list.Add(group);
                    Fill_loadingOptions(request.LoadOptions, list);
                }

                response.DeviceGroups = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserSevice, get_DeviceGroupByGroupId()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
               
            return response;
        }

        public DeviceGroupResponse get_DeviceGroupByCustomerId(DeviceGroupRequest request, User contextUser)
        {
            DeviceGroupResponse response = new DeviceGroupResponse();
            try
            {
                IList<DeviceGroup> list = DataAccess.DeviceGroupDao.GetDeviceGroupsByCustomerId(contextUser.CustomerId);
                
                Fill_loadingOptions(request.LoadOptions, list);
                
                response.DeviceGroups = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserSevice, get_DeviceGroupByCustomerId()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
            
            return response;
        }

        public DeviceTypeResponse get_DeviceTypeByAppId(DeviceTypeRequest request)
        {
            DeviceTypeResponse response = new DeviceTypeResponse();

            try
            {
                IList<DeviceType> list = DataAccess.DeviceTypeDao.GetAllDeviceTypesByAppId(request.Criteria.AppId);
                response.Types = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in DeviceTypeResponse", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;
        }

        public DeviceTypeResponse get_AllDeviceType(DeviceTypeRequest request)
        {
            DeviceTypeResponse response = new DeviceTypeResponse();

            try
            {
                IList<DeviceType> list = DataAccess.DeviceTypeDao.GetAllTypes();
                response.Types = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;

            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in DeviceTypeResponse", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;

            
        }

        public DeviceGroupResponse get_groupsByDeviceType(DeviceGroupRequest request, User contextUser)
        {
            DeviceGroupResponse response = new DeviceGroupResponse();
            try
            {
                IList<DeviceGroup> groups = DataAccess.DeviceGroupDao.GetDeviceGroupsByTypeId(request.Criteria.DeviceTypeId, contextUser.CustomerId);
                response.DeviceGroups = DtoMapper.ToMapper.ToDataTransferObject(groups);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in groupsByDeviceType", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }


            return response;
        }

        public static void Fill_loadingOptions(string[] LoadOptions, IList<DeviceGroup> groups)
        {
            if(LoadOptions == null) { return; }
            
            if (LoadOptions.Contains("devices"))
            {
                foreach (DeviceGroup g in groups)
                {
                    g.Devices = DataAccess.DeviceAvailbilityDao.GetDevicesByGroupId(g.DeviceGroupID);

                    Fill_loadingOptions(LoadOptions, g.Devices);
                }
            }
        }

        public static void Fill_loadingOptions(string[] LoadOptions, IList<DeviceAvailbility> devices)
        {
            if (LoadOptions == null) { return; }

            if (LoadOptions.Contains("tags"))
            {
                foreach (DeviceAvailbility d in devices)
                {
                    d.Tags = DataAccess.TagCloudDao.GetTagByMappedToId(d.DeviceId);
                }
            }
        }
    }
}