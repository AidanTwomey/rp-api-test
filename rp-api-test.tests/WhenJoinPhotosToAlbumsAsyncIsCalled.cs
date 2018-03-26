using System;
using Xunit;
using rp_api_test;
using rp_api_test.Interfaces;
using RichardSzalay.MockHttp;
using System.Linq;

namespace rp_api_test.tests
{
    public class WhenJoinPhotosToAlbumsAsyncIsCalled
    {
        private readonly IPhotoRetriever retriever;

        private const string samplePhotos = @"[
{
""albumId"": 1,
""id"": 1,
""title"": ""accusamus beatae ad facilis cum similique qui sunt"",
""url"": ""http://placehold.it/600/92c952"",
""thumbnailUrl"": ""http://placehold.it/150/92c952""
},
{
""albumId"": 1,
""id"": 2,
""title"": ""reprehenderit est deserunt velit ipsam"",
""url"": ""http://placehold.it/600/771796"",
""thumbnailUrl"": ""http://placehold.it/150/771796""
}]";    

        private const string sampleAlbums = @"[
{
""userId"": 1,
""id"": 1,
""title"": ""quidem molestiae enim""
}]";

        public WhenJoinPhotosToAlbumsAsyncIsCalled()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://jsonplaceholder.typicode.com/photos").Respond("application/json", samplePhotos);
            mockHttp.When("http://jsonplaceholder.typicode.com/albums").Respond("application/json", sampleAlbums);

            this.retriever = new PhotoRetriever(mockHttp);
        }
        [Fact]
        public void PhotosAreReturned()
        {
            var photos = this.retriever.JoinPhotosToAlbumsAsync();

            Assert.Equal(2, photos.Result.Count() );
        }
    }
}
