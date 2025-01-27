using System;
using System.Collections.Generic;
using System.Text;
using DataObjects;
using BusinessObjects;
using ServiceObject.DtoMapper;
using ServiceObject.Messages;

namespace ServiceObject
{
    public class TagService
    {
        public TagResponse set_operatiors(TagRequest request, User contextUser)
        {
            TagResponse response = new TagResponse();

            Tag tag = DtoMapper.FromMapper.FromDataTransferObject(request.Tag);

            if (request.Action.ToLower() == "create" || request.Action.ToLower() == "update" )
            {
                
                if (!tag.Validate())
                {
                    response.Acknowledge = MessageBase.AcknowledgeType.Failure;

                    foreach (string error in tag.ValidationErrors)
                        response.Message += error + Environment.NewLine;

                    return response;
                }
                
            }

            if (request.Action.ToLower() == "create")
            {
                DataAccess.TagCloudDao.Insert( tag, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "update")
            {
                DataAccess.TagCloudDao.Update(tag, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else if (request.Action.ToLower() == "delete")
            {
                DataAccess.TagCloudDao.Delete( request.Criteria.TagId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;

            }
            else if (request.Action.ToLower() == "addtagmap")
            {
                //TODO add logic to protect the database for duplicate keys
                if(!DataAccess.TagCloudDao.mapExist(request.Criteria.TagId, request.Criteria.EntityId, request.Criteria.MappedToId))
                {
                    DataAccess.TagCloudDao.AddMapTag(request.Criteria.TagId, request.Criteria.EntityId, request.Criteria.MappedToId, contextUser);
                    response.Acknowledge = MessageBase.AcknowledgeType.Success;
                }
            }
            else if (request.Action.ToLower() == "removetagmap")
            {
                DataAccess.TagCloudDao.RemoveMapTag(request.Criteria.TagId, request.Criteria.EntityId, request.Criteria.MappedToId, contextUser);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            else
            {
                response.Message = "Not a valid action: " + request.Action.ToLower();
            }

            return response;
        }

        public TagResponse get_TagsByMappedId(TagRequest request)
        {
            TagResponse response = new TagResponse();

            try
            {
                IList<Tag> list = DataAccess.TagCloudDao.GetTagByMappedToId(request.Criteria.MappedToId);
                response.Tags = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;
            }
            catch (Exception e)
            {
                Logger.Logger.Instance.Fatal("Exception thrown in Groups Service, get_TagsByMappedID() ", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;
        }

        public TagResponse get_TagsByCustomerId(TagRequest request)
        {
            TagResponse response = new TagResponse();

            try
            {
                IList<Tag> list = DataAccess.TagCloudDao.GetTagSByCustomerId(request.Criteria.CustomerId);
                response.Tags = DtoMapper.ToMapper.ToDataTransferObject(list);
                response.Acknowledge = MessageBase.AcknowledgeType.Success;

            }catch(Exception e)
            {
                Logger.Logger.Instance.Fatal("Exception thrown in Groups Service, get_TagsByCustomerId() ", e);
                response.Acknowledge = MessageBase.AcknowledgeType.Failure;
            }

            return response;
        }

    }
}