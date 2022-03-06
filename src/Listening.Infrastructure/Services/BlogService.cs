using AutoMapper;
using Listening.Core.Entities.Specialized.MiniBlog;
using Listening.Core.ViewModels.Blog;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services
{
    public class BlogService : IBlogService
    {
        private readonly string _videoFolderName;
        private readonly string _videoPath;
        private readonly IPostEFRepository _postEFRepository;
        private readonly IMapper _mapper;

        public BlogService(
            IPostEFRepository postEFRepository,
            IConfiguration configuration,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _videoFolderName = configuration["Data:FileStorage:Blog:Video"];
            _videoPath = $"{env.WebRootPath}{_videoFolderName}";
            _postEFRepository = postEFRepository;
            _mapper = mapper;
        }

        public async Task<SinglePostDto> GetPost(long id)
        {
            var posts = await _postEFRepository.GetPost(id);
            var postDto = _mapper.Map<SinglePostDto>(posts);
            return postDto;
        }

        public async Task<PostDto[]> GetPosts(bool isAsc = false)
        {
            var posts = await _postEFRepository.GetPosts(isAsc);
            var postDtos = _mapper.Map<PostDto[]>(posts);
            return postDtos;
        }

        public async Task<PostDescriptionDto[]> GetPostDescriptions(bool isAsc = false)
        {
            var posts = await _postEFRepository.GetPostDescriptions(isAsc);
            var postDtos = _mapper.Map<PostDescriptionDto[]>(posts);
            return postDtos;
        }

        public async Task<AdditionalDto> GetAdditionalInfo()
        {
            var (topics, priorities) = await _postEFRepository.GetAdditional();
            var topicDtos = _mapper.Map<TopicDto[]>(topics);
            var priorityDtos = _mapper.Map<PriorityDto[]>(priorities);
            var files = Directory.GetFiles(_videoPath).Select(x => x.Split(_videoFolderName).Last()).ToArray();

            var result = new AdditionalDto
            {
                Topics = topicDtos,
                Priorities = priorityDtos,
                Videos = files,
                VideoFolderName = _videoFolderName
            };

            return result;
        }

        public async Task<long> InsertPost(PostWriteDto postWriteDto)
        {
            var post = _mapper.Map<Post>(postWriteDto);

            return await _postEFRepository.Insert(post, postWriteDto.TopicIds);
        }

        public async Task<int> InsertTopic(string topic)
        {
            return await _postEFRepository.InsertTopic(topic);
        }

        public async Task UpdatePosts(PostWriteDto postWriteDto)
        {
            var post = _mapper.Map<Post>(postWriteDto);
            await _postEFRepository.Update(post, postWriteDto.TopicIds);
        }

        public async Task DeletePosts(long id)
        {
            await _postEFRepository.Delete(id);
        }
    }
}
