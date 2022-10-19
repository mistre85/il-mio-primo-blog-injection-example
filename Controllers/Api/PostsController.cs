using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Models;
using NetCore_01.Data;
using NetCore_01.Models.Repositories.Interfaces;

namespace NetCore_01.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostRepository postRepository;

        public PostsController(IPostRepository _postRepository)
        {
            this.postRepository = _postRepository;
        }

        [HttpGet]
        public IActionResult Get(string? search)
        {
            List<Post> posts = this.postRepository.GetListByFilter(search);

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Post post = this.postRepository.GetById(id);

            return Ok(post);
        }
    }
}
