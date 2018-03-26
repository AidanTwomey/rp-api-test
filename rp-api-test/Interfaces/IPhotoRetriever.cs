using rp_api_test.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace rp_api_test.Interfaces
{
    public interface IPhotoRetriever
    {
        Task<IEnumerable<Photo>> JoinPhotosToAlbumsAsync();
    }
}