using Listening.Core.Entities.Specialized.MiniBlog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Abstract
{
    public interface IPostEFRepository : IEntityBaseRepository<Post>
    {
        Task<Post> GetPost(long id);
        Task<Post[]> GetPosts(bool isAsc = false);
        Task<Post[]> GetPostDescriptions(bool isAsc = false);
        Task<(Topic[] topics, Priority[] priorities)> GetAdditional();
        Task<long> Insert(Post post, int[] topicIds);
        Task Update(Post posts, int[] topicIds);
        Task Delete(long id);
        Task<int> InsertTopic(string topicName);
    }
}
