using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects;
using ServiceObject.Messages;
using DataObjects;

namespace ServiceObject
{
    public class PolicyManagerService
    {
        
        public PolicyResponse set_operatiors(PolicyRequest request, User contextUser)
        {
            PolicyResponse response = new PolicyResponse();

            if(contextUser.IsAdmin != true)
            {
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                response.Message = "Unauthorized!";
                return response;
            }

            Policy policy = DtoMapper.FromMapper.FromDataTransferObject(request.policy);

            if(request.Action.ToLower() != "delete")
            {
                if (policy != null)
                {
                    if (!policy.Validate())
                    {
                        response.Acknowledge = MessageBase.AcknowledgeType.Failure;

                        foreach (string error in policy.ValidationErrors)
                            response.Message += error + Environment.NewLine;

                        return response;
                    }
                }
            }

            if (request.Action.ToLower() == "create")
            {
                try
                {
                    DataAccess.PolicyDao.Insert(policy, contextUser);
                    response.Acknowledge = MessageBase.AcknowledgeType.Success;

                }
                catch (Exception e)
                {
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                    response.Message = e.Message;
                }
            }
            else if (request.Action.ToLower() == "update")
            {
                DataAccess.PolicyDao.Update(policy, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "delete")
            {
                DataAccess.PolicyDao.Delete(request.Criteria.PolicyId, contextUser);  
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else
            {
                response.Message = "Not a valid action: " + request.Action.ToLower();
            }

            return response;


        }

        public PolicyResponse get_ByPolicyId(PolicyRequest request, User contextUser)
        {
            PolicyResponse response = new PolicyResponse();
            Policy policy = DataAccess.PolicyDao.GetPolicyById(request.Criteria.PolicyId);

            IList<Policy> policies = new List<Policy>();
            policies.Add(policy);

            response.Policies = DtoMapper.ToMapper.ToDataTransferObject(policies);
            response.Acknowledge = MessageBase.AcknowledgeType.Success;

            return response;
        }

        public PolicyResponse get_AllPolicies(PolicyRequest request, User contextUser)
        {
            PolicyResponse response = new PolicyResponse();
            IList<Policy> policies = DataAccess.PolicyDao.GetAllPolicies();

            response.Policies = DtoMapper.ToMapper.ToDataTransferObject(policies);
            response.Acknowledge = MessageBase.AcknowledgeType.Success;

            return response;
        }
        
        
        
    }
}
