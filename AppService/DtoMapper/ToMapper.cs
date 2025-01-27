using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using ServiceObject.DataTransferObjects;

namespace ServiceObject.DtoMapper
{
    public static class ToMapper
    {
        public static IList<GroupDto> ToDataTransferObject(IEnumerable<Group> groups)
        {
            if (groups == null) return null;
            return groups.Select(g => ToDataTransferObject(g)).ToList();
        }

        public static GroupDto ToDataTransferObject(Group group)
        {
            if (group == null) return null;

            return new GroupDto
            {
                GroupID = group.GroupID,
                CustomerID = group.CustomerID,
                GroupName = group.GroupName,
                Users = ToDataTransferObject(group.Users)
            };
        }

        public  static IList<UserDto> ToDataTransferObject(IEnumerable<User> users)
        {
            if (users == null) return null;

            return users.Select(u => ToDataTransferObject(u)).ToList();
        }

        public static UserDto ToDataTransferObject(User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                UserID = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CustomerID = user.CustomerId,
                EmailAddress = user.EmailAddress,
                Profilephoto = user.Photo,
                Groups = ToDataTransferObject(user.Groups),
                Customer = ToDataTransferObject(user.Customer),
                Applications = ToDataTransferObject(user.Applications),
                IsAdmin = user.IsAdmin,
                IsRead = user.IsRead,
                IsWrite = user.IsWrite
            };
        }

        public static IList<CustomerDto> ToDataTransferObject(IEnumerable<Customer> customer)
        {
            if (customer == null) return null;

            return customer.Select(c => ToDataTransferObject(c)).ToList();
        }

        public static CustomerDto ToDataTransferObject(Customer customer)
        {
            if (customer == null) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName
            };
        }

        public static IList<ApplicationDto> ToDataTransferObject(IEnumerable<Application> application)
        {
            if (application == null) return null;

            return application.Select(c => ToDataTransferObject(c)).ToList();
        }

        public static ApplicationDto ToDataTransferObject(Application application)
        {
            if (application == null) return null;

            return new ApplicationDto
            {
                AppID = application.AppID,
                AppName = application.AppName,
                RouteURL = application.RouteURL,
                Devices = ToDataTransferObject(application.Devices)

            };
        }
        
        public static IList<DeviceAvailbilityDto> ToDataTransferObject(IEnumerable<DeviceAvailbility> deviceAvailbility)
        {
            if (deviceAvailbility == null) return null;

            return deviceAvailbility.Select(c => ToDataTransferObject(c)).ToList();
        }

        public static DeviceAvailbilityDto ToDataTransferObject(DeviceAvailbility deviceAvailbility)
        {
            if (deviceAvailbility == null) return null;

            return new DeviceAvailbilityDto
            {
                DeviceID = deviceAvailbility.DeviceId,
                DeviceSerialNumber = deviceAvailbility.DeviceSerialNumber,
                DeviceTypeID = deviceAvailbility.DeviceTypeID,
                Tags = ToDataTransferObject(deviceAvailbility.Tags)
            };
        }

        public static IList<DeviceGroupDto> ToDataTransferObject(IEnumerable<DeviceGroup> deviceGroup)
        {
            if (deviceGroup == null) return null;

            return deviceGroup.Select(c => ToDataTransferObject(c)).ToList();
        }
        
        public static  DeviceGroupDto ToDataTransferObject(DeviceGroup deviceGroup)
        {
            if (deviceGroup == null) return null;

            return new  DeviceGroupDto
            {
                DeviceGroupID = deviceGroup.DeviceGroupID,
                AppID =deviceGroup.AppID,
                CustomerId = deviceGroup.CompanyID,
                DeviceGroupName = deviceGroup.DeviceGroupName,
                Devices = ToDataTransferObject(deviceGroup.Devices)
            };
        }

        public static TagDto ToDataTransferObject(Tag tag)
        {
            if (tag == null) return null;

            return new TagDto
            {
                TagId = tag.TagId,
                TagName = tag.TagName,
                Description = tag.Description,
                CustomerId = tag.CustomerId
            };
        }

        public static IList<TagDto> ToDataTransferObject(IEnumerable<Tag> tag)
        {
            if (tag == null) return null;

            return tag.Select(c => ToDataTransferObject(c)).ToList();
        }

        public static IList<UserIdentityDto> ToDataTransferObject(IEnumerable<UserIdentity> userIdentities)
        {
            if (userIdentities == null) return null;

            return userIdentities.Select(c => ToDataTransferObject(c)).ToList();
        }

        public static UserIdentityDto ToDataTransferObject(UserIdentity userIdentity)
        {
            if (userIdentity == null) return null;

            return new UserIdentityDto
            {
                UserId = userIdentity.UserId,
                Provider = userIdentity.Provider,
                Identifier = userIdentity.Identifier
            };
        }
        public static IList<DeviceTypeDto> ToDataTransferObject(IEnumerable<DeviceType> deviceTypes)
        {
            if (deviceTypes == null) return null;
            return deviceTypes.Select(c => ToDataTransferObject(c)).ToList();
        }

        public static DeviceTypeDto ToDataTransferObject(DeviceType deviceType)
        {
            if (deviceType == null) return null;

            return new DeviceTypeDto
            {
                TypeId = deviceType.TypeId,
                Name = deviceType.TypeName
            };
        }

        public static IList<SubscriptionDto> ToDataTransferObject(IEnumerable<PlatformSubscription> subscription)
        {
            if (subscription == null) return null;
            return subscription.Select(c => ToDataTransferObject(c)).ToList();
        }
        public static SubscriptionDto ToDataTransferObject(PlatformSubscription subscription)
        {
            if (subscription == null) return null;
            return new SubscriptionDto
            {
                Id = subscription.SubId,
                SubDefId = subscription.SubdefId,
                EntityType = subscription.EntityType,
                EntityValue = subscription.EntityValue,
                SubValue = subscription.SubValue
            };
        }

        public static IList<PolicyDto> ToDataTransferObject(IEnumerable<Policy> policy)
        {
            if (policy == null) return null;
            return policy.Select(c => ToDataTransferObject(c)).ToList();
        }
        public static PolicyDto ToDataTransferObject(Policy policy)
        {
            if (policy == null) return null;
            return new PolicyDto
            {
                Id = policy.PolicyId,
                PolicDefId = policy.PolicyDefId,
                PolicyValue1 = policy.PolicyValue1,
                EntityType = policy.EntityType,
                EntityValue = policy.EntityValue

            };
        }
    }
}