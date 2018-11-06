using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Posts.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [ProducesResponseType(500)]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return StatusCode(500, new { Error = "Unknown error" });
        } 
    }
}
