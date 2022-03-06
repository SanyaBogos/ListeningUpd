using Listening.Core.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IBlogService
    {
        Task<SinglePostDto> GetPost(long id);
        Task<PostDto[]> GetPosts(bool isAsc = false);
        Task<PostDescriptionDto[]> GetPostDescriptions(bool isAsc = false);
        Task<AdditionalDto> GetAdditionalInfo();
        Task<long> InsertPost(PostWriteDto postWriteDto);
        Task<int> InsertTopic(string topic);
        Task UpdatePosts(PostWriteDto post);
        Task DeletePosts(long id);
    }
}
