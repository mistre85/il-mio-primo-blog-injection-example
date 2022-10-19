using Microsoft.AspNetCore.Mvc;
using NetCore.Models;
using NetCore_01.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCore_01.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] Message message)
        {
            using (BlogContext context = new BlogContext())
            {
                context.Messages.Add(message);

                context.SaveChanges();
            }
        }
    }
}
