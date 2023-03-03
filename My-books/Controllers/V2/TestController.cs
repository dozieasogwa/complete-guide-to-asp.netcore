using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_books.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiversion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is TestController V2.");
        }
    }
}
