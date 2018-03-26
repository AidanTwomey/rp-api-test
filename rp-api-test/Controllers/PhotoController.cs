using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using rp_api_test.Models;
using Newtonsoft.Json;

namespace rp_api_test.csproj.Controllers
{    
    [Route("api/photos")]
    public class PhotoController : Controller
    {  
        private class LocalPhoto
        {
            public int AlbumId { get;set;}
            public int Id { get;set;}
            public string Title { get;set;}
            public string Url { get;set;}
            public string ThumbnailUrl { get;set;}
        }

        private class Album
        {
            public int Id { get;set;}
            public int UserId { get;set;}
            public string Title { get;set;}
        }
        private static readonly HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult GetPhotos()
        {
            return Ok(GetApiPhotos().Result);
        }

        private async Task<IEnumerable<rp_api_test.Models.Photo>> GetApiPhotos()
        {
            var albumsResponse = await client.GetStringAsync("http://jsonplaceholder.typicode.com/albums");
            var photosResponse = await client.GetStringAsync("http://jsonplaceholder.typicode.com/photos");

            var albums = JsonConvert.DeserializeObject<ICollection<PhotoController.Album>>(albumsResponse);
            var photos = JsonConvert.DeserializeObject<ICollection<PhotoController.LocalPhoto>>(photosResponse);

            return photos.Join( 
                albums, 
                p => p.AlbumId, 
                a => a.Id, 
                (p,a) => new rp_api_test.Models.Photo()
                    { 
                        Title = p.Title, 
                        // AlbumName = a.Title, 
                        // ThumbnailUrl = p.ThumbnailUrl
                    } );
        }
    }
}