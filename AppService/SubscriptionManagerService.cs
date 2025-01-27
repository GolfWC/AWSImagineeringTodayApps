using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects;
using ServiceObject.Messages;
using DataObjects;


namespace ServiceObject
{
    public class SubscriptionManagerService
    {
        
        public SubscriptionResponse set_operatiors(SubscriptionRequest request, User contextUser)
        {
            SubscriptionResponse response = new SubscriptionResponse();

            if (contextUser.IsAdmin != true)
            {
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
                response.Message = "Unauthorized!";
                return response;
            }

           PlatformSubscription subscription = DtoMapper.FromMapper.FromDataTransferObject(request.Subscription);

            if (request.Action.ToLower() != "delete")
            {
                if (subscription != null)
                {
                    if (!subscription.Validate())
                    {
                        response.Acknowledge = MessageBase.AcknowledgeType.Failure;

                        foreach (string error in subscription.ValidationErrors)
                            response.Message += error + Environment.NewLine;

                        return response;
                    }
                }
            }

            if (request.Action.ToLower() == "create")
            {
                try
                {
                    DataAccess.PlatformSubscriptionsDao.Insert(subscription, contextUser);
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
                DataAccess.PlatformSubscriptionsDao.Update(subscription, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "delete")
            {
                DataAccess.PlatformSubscriptionsDao.Delete(request.Criteria.SubscriptionId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else
            {
                response.Message = "Not a valid action: " + request.Action.ToLower();
            }

            
            return response;
        }

        public SubscriptionResponse get_BySubscriptionId(SubscriptionRequest request)
        {
            SubscriptionResponse response = new SubscriptionResponse();
            
            PlatformSubscription subscription = DataAccess.PlatformSubscriptionsDao.GetSubscriptionById(request.Criteria.SubscriptionId);

            IList<PlatformSubscription> subscriptions = new List<PlatformSubscription>();
            subscriptions.Add(subscription);

            response.Subscriptions = DtoMapper.ToMapper.ToDataTransferObject(subscriptions);
            response.Acknowledge = MessageBase.AcknowledgeType.Success;

           
            return response;
        }
        public SubscriptionResponse get_AllSubscription( SubscriptionRequest request)
        {
            SubscriptionResponse response = new SubscriptionResponse();
            IList<PlatformSubscription> subscriptions = DataAccess.PlatformSubscriptionsDao.GetAllSubscriptions();

            response.Subscriptions = DtoMapper.ToMapper.ToDataTransferObject(subscriptions);
            response.Acknowledge = MessageBase.AcknowledgeType.Success;

            return response;
        }
        
    }
}
