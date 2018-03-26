using rp_api_test.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using rp_api_test.Interfaces;

namespace rp_api_test
{
    public class PhotoRetriever : IPhotoRetriever
    {
        private class LocalPhoto
        {
            public int AlbumId { get;set;}
            public int Id { get;set;}
            public string Title { get;set;}
            public string Url { get;set;}
            public string ThumbnailUrl { get;set;}
        }

        private class LocalAlbum
        {
            public int Id { get;set;}
            public int UserId { get;set;}
            public string Title { get;set;}
        }
        private readonly HttpClient client;

        public PhotoRetriever(HttpMessageHandler handler)
        {
            this.client = new HttpClient(handler);
        }

        public async Task<IEnumerable<Photo>> JoinPhotosToAlbumsAsync()
        {
            var albumsResponse = await this.client.GetStringAsync("http://jsonplaceholder.typicode.com/albums");
            var photosResponse = await this.client.GetStringAsync("http://jsonplaceholder.typicode.com/photos");

            var albums = JsonConvert.DeserializeObject<ICollection<LocalAlbum>>(albumsResponse);
            var photos = JsonConvert.DeserializeObject<ICollection<LocalPhoto>>(photosResponse);

            return photos.Join( 
                albums, 
                p => p.AlbumId, 
                a => a.Id, 
                (p,a) => new Photo()
                    { 
                        Title = p.Title, 
                        AlbumName = a.Title, 
                        ThumbnailUrl = p.ThumbnailUrl
                    } );
        }
    }
}