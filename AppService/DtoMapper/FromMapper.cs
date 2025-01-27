using System;
using System.Collections.Generic;
using System.Text;

using ServiceObject.DataTransferObjects;
using BusinessObjects;

namespace ServiceObject.DtoMapper
{
    public static class FromMapper
    {
        public static Group FromDataTransferObject(GroupDto group)
        {
            if (group == null) return null;

            return new Group(group.GroupID, group.CustomerID, group.GroupName);
        }

        public static User FromDataTransferObject(UserDto user)
        {
            if (user == null) return null;
            User u = new User(user.UserID, user.FirstName, user.LastName, user.CustomerID, user.EmailAddress);
            u.Photo = user.Profilephoto;
            return u;
            
        }

        public static Customer FromDataTransferObject(CustomerDto customer)
        {
            if (customer == null) return null;

            return new Customer(customer.CustomerId, customer.CustomerName);
        }

         public static Application FromDataTransferObject(ApplicationDto application)
        {
            if (application == null) return null;

            return new Application(application.AppID, application.AppName);
        }

        public static DeviceAvailbility FromDataTransferObject(DeviceAvailbilityDto deviceAvailbility)
        {
            if (deviceAvailbility == null) return null;

            return new DeviceAvailbility(deviceAvailbility.DeviceID, deviceAvailbility.DeviceSerialNumber,deviceAvailbility.DeviceTypeID,deviceAvailbility.IsProvisioned);
        }

        public static DeviceGroup FromDataTransferObject(DeviceGroupDto deviceGroup)
        {
            if (deviceGroup == null) return null;

            return new DeviceGroup(deviceGroup.DeviceGroupID,deviceGroup.AppID,deviceGroup.CustomerId,deviceGroup.DeviceGroupName);
        }

        public static Tag FromDataTransferObject(TagDto tag)
        {
            if (tag == null) return null;

            return new Tag(tag.TagId, tag.TagName, tag.Description, tag.CustomerId);
        }

        public static UserIdentity FromDataTransferObject(UserIdentityDto identity)
        {
            if (identity == null) return null;

            return new UserIdentity( identity.UserId, identity.Provider, identity.Identifier);
        }

        public static DeviceType FromDataTransferObject(DeviceTypeDto deviceType)

        {
            if (deviceType == null) return null;
            return new DeviceType(deviceType.TypeId, deviceType.Name, deviceType.Description, deviceType.Prefix);
        }
        public static PlatformSubscription FromDataTransferObject( SubscriptionDto subscription)
        {
            if (subscription == null) return null;
            return new PlatformSubscription(subscription.Id, subscription.EntityType, subscription.EntityValue, subscription.SubDefId, subscription.SubValue);
        }
        public static Policy FromDataTransferObject(PolicyDto policy)
        {
            if (policy == null) return null;
            return new Policy(policy.Id, policy.PolicDefId, policy.EntityType, policy.EntityValue, policy.PolicyValue1);
        }


    }
}