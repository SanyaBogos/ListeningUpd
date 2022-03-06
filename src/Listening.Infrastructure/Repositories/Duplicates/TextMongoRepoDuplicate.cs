using Listening.Server.Entities.Specialized.Text;
using Listening.Infrastructure.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Server.Repositories.Mongo;

namespace Listening.Server.Repositories.Duplicates
{
    public interface ITextMongoRepoDuplicate : IRepositoryMongo<Text, ObjectId> { }

    public class TextMongoRepoDuplicate : TextsMongoRepository, ITextMongoRepoDuplicate
    {
        public TextMongoRepoDuplicate(IConfiguration configuration) : base(configuration) { }
    }
}
