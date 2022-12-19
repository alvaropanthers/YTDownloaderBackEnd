using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Common;
using YTDownloaderAPI.wrapper;

namespace YTDownloaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        [HttpGet]
        public PlayList GetPlayList(string url)
        {
            return YoutubeExplodeHelper.ListPlayListVideosAsync(url).Result;
        }
    }
}
