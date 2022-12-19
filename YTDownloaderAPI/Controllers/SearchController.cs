using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode.Search;
using YTDownloaderAPI.wrapper;

namespace YTDownloaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<List<Video>> SearchVideo(string search)
        {
            var result = await YoutubeExplodeHelper.SearchVideo(search);
            
            List<Video> videoList = new List<Video>();
            result.ForEach(video => 
                videoList.Add(new Video()
                {
                    Id = video.Id.Value,
                    Title = video.Title,
                    ChannelTitle = video.Author.Title,
                    Url = video.Url,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                })
            );

            return videoList;
        }
    }
}
