using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCore.Api;

namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : BaseApiController
    {
        public MenuController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return NoContent();
        }
    }
}