using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels.Blog;
using Listening.Infrastructure.Services.Contracts;
using Listening.Server.Filters;
using Listening.Web.Controllers.api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Listening.Web.Controllers.api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Super")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(
            UserManager<ApplicationUser> userManager,
            IBlogService blogService)
            : base(userManager)
        {
            _blogService = blogService;
        }

        [AllowAnonymous]
        [HttpGet("{isAsc}")]
        [LogFilter]
        public async Task<PostDto[]> Get(bool isAsc = false)
        {
            //System.Threading.Thread.Sleep(300000);
            var result = await _blogService.GetPosts(isAsc);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("post/{id}")]
        [LogFilter]
        public async Task<SinglePostDto> GetPost(long id)
        {
            var result = await _blogService.GetPost(id);
            return result;
        }

        [AllowAnonymous]
        [LogFilter]
        [HttpGet("descriptions/{isAsc}")]
        public async Task<PostDescriptionDto[]> GetDescriptions(bool isAsc = false)
        {
            var result = await _blogService.GetPostDescriptions(isAsc);
            return result;
        }

        [AllowAnonymous]
        [HttpGet("info")]
        public async Task<AdditionalDto> GetAdditionalInfo()
        {
            var result = await _blogService.GetAdditionalInfo();
            return result;
        }

        [HttpPost("filtered")]
        public IActionResult GetFiltered()
        {
            return View();
        }

        [HttpPost("addTopic")]
        public async Task<int> AddTopic([FromBody]string topic)
        {
            return await _blogService.InsertTopic(topic);
        }

        [HttpPost("add")]
        [InsertLogDataFilter]
        public async Task<long> Add([FromBody]PostWriteDto post)
        {
            post.UserId = (await GetCurrentUserAsync()).Id;
            return await _blogService.InsertPost(post);
        }

        [HttpPut("update")]
        [InsertLogDataFilter]
        public async Task Update([FromBody]PostWriteDto post)
        {
            post.UserId = (await GetCurrentUserAsync()).Id;
            await _blogService.UpdatePosts(post);
        }

        [HttpDelete("delete/{id}")]
        public async Task Delete(long id)
        {
            await _blogService.DeletePosts(id);
        }
    }
}