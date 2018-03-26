using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using rp_api_test.Models;
using Newtonsoft.Json;
using rp_api_test.Interfaces;

namespace rp_api_test.Controllers
{    
    [Route("api/photos")]
    public class PhotoController : Controller
    {  
        private readonly IPhotoRetriever retriever;

        public PhotoController(IPhotoRetriever retriever)
        {
            this.retriever = retriever;
        }

        [HttpGet]
        public IActionResult GetPhotos()
        {
            return Ok(GetApiPhotos());
        }

        private async Task<IEnumerable<Photo>> GetApiPhotos()
        {
            return await retriever.JoinPhotosToAlbumsAsync();
        }
    }
}