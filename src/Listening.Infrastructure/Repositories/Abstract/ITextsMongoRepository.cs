using Listening.Core.Entities.Custom;
using Listening.Server.Entities.Specialized.Text;
using Listening.Core.ViewModels.Text;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface ITextsMongoRepository : IRepositoryMongo<Text, ObjectId>
    {
        Task<List<Text>> GetAndCheckUpdatePossiblity(TextDto[] textDtos, ApplicationUser user, bool isAdmin);
        Task<List<Text>> Get(string[] ids);
        Task MarkAsDeleted(IEnumerable<ObjectId> ids);
    }
}