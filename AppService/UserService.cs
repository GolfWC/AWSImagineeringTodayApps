using System;
using System.Collections.Generic;
using System.Linq;

using ServiceObject.Messages;
using BusinessObjects;
using DataObjects;
using ServiceObject.DataTransferObjects;
using AuthenticationObjects;

namespace ServiceObject
{
    public class UserService
    {
        public UserResponse set_operatiors(UserRequest request, User contextUser)
        {
            UserResponse response = new UserResponse();           
            User user = DtoMapper.FromMapper.FromDataTransferObject(request.User);

            if (request.Action.ToLower() != "delete")
            {
                if (!user.Validate())
                {
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;

                    foreach (string error in user.ValidationErrors)
                        response.Message += error + Environment.NewLine;

                    return response;
                }
            }

            if (request.Action.ToLower() == "create")
            {
                BusinessLogic.UserAdministration.Provision(user, "firebase", contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "update")
            {

                DataAccess.UserDao.Update(user, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "delete")
            {
                BusinessLogic.UserAdministration.DeleteUser(request.Criteria.UserId, contextUser, "firebase");
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else
            {
                response.Message = "Not a valid action: " + request.Action.ToLower();
            }

            return response;
        }

        public UserResponse get_AllUsersByCustomerId(UserRequest request)
        {
            UserResponse response = new UserResponse();

            IEnumerable<User> users = DataAccess.UserDao.GetAllUsersByCustomerId(request.Criteria.CustomerId);

            if (users.Count<User>() > 0)
            {
                try
                {
                    response.Users = users.Select(u => DtoMapper.ToMapper.ToDataTransferObject(u)).ToList();
                    response.Acknowledge = MessageBase.AcknowledgeType.Success;
                }
                catch (Exception e)
                {
                    Logger.Logger.Instance.Error("Exception throw in UserSevice, get_AllUsersByCustomerId()", e);
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                }
               
            }

            return response;
        }

        public UserResponse get_AllUsersByContext(User contextUser)
        {
            UserResponse response = new UserResponse();

            IEnumerable<User> users = DataAccess.UserDao.GetAllUsersByCustomerId(contextUser.CustomerId);

            if (users.Count<User>() > 0)
            {
                try
                {
                    response.Users = users.Select(u => DtoMapper.ToMapper.ToDataTransferObject(u)).ToList();
                    response.Acknowledge = MessageBase.AcknowledgeType.Success;
                }catch(Exception e)
                {
                    Logger.Logger.Instance.Error("Exception throw in UserService, get_AllMyUsers()", e);
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                }
               
            }

            return response;
        }

        public UserResponse get_AllUsersByGroupId(UserRequest request)
        {
            UserResponse response = new UserResponse();

            IEnumerable<User> users = DataAccess.UserDao.GetAllUsersByGroupId(request.Criteria.GroupId);

            if (users.Count<User>() > 0)
            {
                try
                {
                    response.Users = users.Select(u => DtoMapper.ToMapper.ToDataTransferObject(u)).ToList();
                    response.Acknowledge = MessageBase.AcknowledgeType.Success;
                }catch(Exception e)
                {
                    Logger.Logger.Instance.Error("Exception throw in UserService, get_AllUsersByGroupId()", e);
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                }
            }

            return response;
        }
        
        public UserResponse get_UserById(UserRequest request)
        {
            UserResponse response = new UserResponse();
            response.Users = new List<UserDto>();

            User user = DataAccess.UserDao.GetUserById(request.Criteria.UserId);

            if(user != null)
            {
                try
                {
                    IList<User> list = new List<User>();
                    if (user != null) 
                    {
                        for(int i = 0; i < request.LoadOptions.Length; i++)
                        {
                            if(request.LoadOptions[i] == "Groups")
                            {
                                user.Groups = DataAccess.GroupsDao.GetGroupsByUserId(request.Criteria.UserId);
                            }
                        }
                        
                        list.Add(user);
                    }

                    response.Users = DtoMapper.ToMapper.ToDataTransferObject(list);
                    response.Acknowledge = MessageBase.AcknowledgeType.Success;
                }
                catch(Exception e)
                {
                    Logger.Logger.Instance.Error("Exception throw in UserService, get_UserById()", e);
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                }
                
            }

            return response;
        }

        //Only expose this method in the App Service Host
        public UserResponse get_ContextUserResponse(UserRequest request, User contextUser)
        {
            UserResponse response = new UserResponse();

            try
            {
                Fill_loadingOptions(request.LoadOptions, contextUser);

                response.Users = new List<UserDto>();
                response.Users.Add(DtoMapper.ToMapper.ToDataTransferObject(contextUser));

                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Error("Exception throw in UserService, get_MyUser()", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }
               
            return response;
        }


        //TODO: Move this into Business Logic
        public User Get_ContextUser(string idtoken)
        {
            return this.Get_ContextUser(idtoken, false);
        }

        public User Get_ContextUser(string idToken, bool test)
        {
            User contextUser;

            try
            {
                if (test != true)
                { 
                    string uid = AuthProvider.GetProvider("firebase").get_Uid(idToken);
                    contextUser = DataAccess.UserDao.GetUserByIdentifier(uid, "firebase");
                }
                else
                {
                    contextUser = DataAccess.UserDao.GetUserByIdentifier(idToken, "firebase");
                }
                
                if (contextUser == null) { return null; }

                contextUser.Groups = DataAccess.GroupsDao.GetAllGroupsForUser(contextUser);

                //BusinessLogic.UserAdministration.Fill_UserSubscriptions(contextUser);

                BusinessLogic.UserAdministration.Fill_UserPolicys(contextUser);
                return contextUser;
            }
            catch(Exception e)
            {
                Logger.Logger.Instance.Error("Error thrown: " + e.Message, e);
                return null;
            }
        }      
        public static void Fill_loadingOptions(string[] LoadOptions, User user)
        {
            if (LoadOptions == null) { return; }

            if (LoadOptions.Contains("customer"))
            {
                user.Customer = DataAccess.CustomerDao.GetCustomer(user.CustomerId);
            }

            if (LoadOptions.Contains("applications"))
            {
                user.Applications = DataAccess.ApplicationDao.GetApplicationsByAppIds(user.AppIds);
            }
        }
    }
}