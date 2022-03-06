using Listening.Core.Entities.Specialized.MiniBlog;
using Listening.Core.ViewModels.Blog;
using Listening.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class PostEFRepository : EntityBaseRepository<Post>, IPostEFRepository
    {
        private readonly DbSet<Post> _dbset;

        public PostEFRepository(ApplicationDbContext context) : base(context)
        {
            _dbset = _context.Set<Post>();
        }

        public async Task<(Topic[] topics, Priority[] priorities)> GetAdditional()
        {
            var topics = _context.Set<Topic>().ToArray();
            var priorities = _context.Set<Priority>().ToArray();
            return (topics, priorities);
        }

        public async Task<Post> GetPost(long id)
        {
            var result = _dbset.Include(x => x.Attachments)
                    .Include(x => x.PostTopics).ThenInclude(x => x.Topic)
                    .FirstOrDefault(x => x.Id == id);

            return result;
        }


        public async Task<Post[]> GetPosts(bool isAsc = false)
        {
            Post[] result;

            if (!isAsc)
            {
                result = await _dbset.Include(x => x.Attachments)
                    .Include(x => x.PostTopics)
                    .OrderByDescending(x => x.Id)
                    .ToArrayAsync();
            }
            else
            {
                result = await _dbset.Include(x => x.Attachments)
                    .Include(x => x.PostTopics)
                    .OrderBy(x => x.Id)
                    .ToArrayAsync();
            }

            return result;
        }

        public async Task<Post[]> GetPostDescriptions(bool isAsc = false)
        {
            IOrderedQueryable<Post> result;

            if (!isAsc)
            {
                result = _dbset.Include(x => x.Attachments)
                    .Include(x => x.PostTopics)
                    .OrderByDescending(x => x.Id);
            }
            else
            {
                result = _dbset.Include(x => x.Attachments)
                    .Include(x => x.PostTopics)
                    .OrderBy(x => x.Id);
            }

            var resultArray = await result.Select(x => new Post
            {
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                Header = x.Header,
                Description = x.Description,
                UpdatedBy = x.UpdatedBy,
                LastModifiedDate = x.LastModifiedDate,
                PriorityId = x.PriorityId,
                UserId = x.UserId,
                PostTopics = x.PostTopics
            }).ToArrayAsync();

            return resultArray;
        }

        public async Task<long> Insert(Post post, int[] topicIds)
        {
            var newPostId = await AddNowAsync(post);

            if (topicIds == null || topicIds.Length == 0)
                return newPostId;

            var dbset = _context.Set<PostTopic>();
            var postTopics = topicIds.Select(x => new PostTopic { PostId = newPostId, TopicId = x }).ToArray();
            dbset.AddRange(postTopics);
            await _context.SaveChangesAsync();

            return newPostId;
        }

        public async Task<int> InsertTopic(string topicName)
        {
            var topicDbSet = _context.Set<Topic>();
            var topic = new Topic { Name = topicName };
            topicDbSet.Add(topic);
            await _context.SaveChangesAsync();
            return topic.Id;
        }

        public async Task Update(Post post, int[] topicIds)
        {
            var dbPost = _dbset.SingleOrDefault(x => x.Id == post.Id);

            if (dbPost == null)
                return;

            dbPost.UpdatedBy = post.UserId;
            dbPost.LastModifiedDate = DateTime.Now;
            dbPost.Message = post.Message;
            dbPost.PriorityId = post.PriorityId;
            dbPost.UserId = post.UserId;
            dbPost.Header = post.Header;
            dbPost.Description = post.Description;

            var postTopics = _context.Set<PostTopic>();
            var postTopicsInDb = postTopics.Where(x => x.PostId == post.Id).ToArray();
            var toRemove = postTopicsInDb.Where(x => !topicIds.Contains(x.TopicId)).ToArray();
            var topicsInDb = postTopicsInDb.Select(x => x.TopicId).ToArray();
            var toAddId = topicIds.Where(x => !topicsInDb.Contains(x)).ToArray();
            var toAdd = toAddId.Select(x => new PostTopic { PostId = dbPost.Id, TopicId = x });

            postTopics.RemoveRange(toRemove);
            postTopics.AddRange(toAdd);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var postToDelete = _dbset.SingleOrDefault(x => x.Id == id);

            if (postToDelete != null)
            {
                Delete(postToDelete);
                await CommitAsync();
            }
        }
    }
}
