using System;
using System.Collections.Generic;

using ServiceObject.Messages;
using BusinessObjects;
using DataObjects;

namespace ServiceObject
{
    public class GroupService
    {
        public GroupsResponse set_operatiors(GroupsRequest request, User contextUser)
        {
            GroupsResponse response = new GroupsResponse();

            Group group = DtoMapper.FromMapper.FromDataTransferObject(request.Group);
            
            if(request.Action.ToLower() != "delete")
            {
                if(!group.Validate())
                {
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;

                    foreach (string error in group.ValidationErrors)
                        response.Message += error + Environment.NewLine;

                    return response;
                }
            }

            if(request.Action.ToLower() == "create")
            {
                DataAccess.GroupsDao.Insert(group, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if(request.Action.ToLower() == "update")
            {
                DataAccess.GroupsDao.Update(group, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if(request.Action.ToLower() == "delete")
            {
                DataAccess.GroupsDao.Delete(request.Criteria.GroupId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "adduser")
            {
                DataAccess.GroupsDao.AddUserToGroup(request.Criteria.UserId, request.Criteria.GroupId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "removeuser")
            {
                DataAccess.GroupsDao.RemoveUserFromGroup(request.Criteria.UserId, request.Criteria.GroupId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else
            {
                response.Message = "Not a valid action: " + request.Action.ToLower();
            }

            return response;
        }

        public GroupsResponse get_ForUser(User contextUser)
        {
            GroupsResponse response = new GroupsResponse();

            try
            {
                IEnumerable<Group> groups = DataAccess.GroupsDao.GetAllGroupsForUser(contextUser);

                response.Groups = DtoMapper.ToMapper.ToDataTransferObject(groups);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Fatal("Exception thrown in Groups Service, get_ForUser() ", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;
        }

        public GroupsResponse get_ForCustomer(User contextUser)
        {
            GroupsResponse response = new GroupsResponse();

            try
            {
                IEnumerable<Group> groups = DataAccess.GroupsDao.GetAllGroupsForCustomer(contextUser);

                response.Groups = DtoMapper.ToMapper.ToDataTransferObject(groups);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Fatal("Exception thrown in Groups Service, get_ForCustomer() ", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
            

            return response;
        }

        public GroupsResponse get_ByUserId(GroupsRequest request)
        {
            GroupsResponse response = new GroupsResponse();

            try
            {
                IEnumerable<Group> groups = DataAccess.GroupsDao.GetGroupsByUserId(request.Criteria.UserId);

                response.Groups = DtoMapper.ToMapper.ToDataTransferObject(groups);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Fatal("Exception thrown in Groups Service, get_ByUserId() ", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
            

            return response;
        }

        public GroupsResponse get_ByCustomerId(GroupsRequest request)
        {
            GroupsResponse response = new GroupsResponse();

            try
            {
                IEnumerable<Group> groups = DataAccess.GroupsDao.GetGroupsByCustomerId(request.Criteria.CustomerId);

                response.Groups = DtoMapper.ToMapper.ToDataTransferObject(groups);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Fatal("Exception thrown in Groups Service, get_ByCustomerId() ", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
           

            return response;
        }
    }
}