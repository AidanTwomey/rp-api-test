using Microsoft.AspNetCore.Mvc;

namespace rp_api_test.csproj.Controllers
{
    [Route("api/photos")]
    public class PhotoController : Controller
    {
        [HttpGet]
        public IActionResult GetPhotos()
        {
            return Ok("hello photos");
        }
    }
}