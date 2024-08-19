using Microsoft.AspNetCore.Mvc;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(
                "Hello, World! is not enought"
        );
        }
    }
}
