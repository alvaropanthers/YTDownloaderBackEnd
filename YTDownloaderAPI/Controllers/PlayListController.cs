using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Common;
using YTDownloaderAPI.Models;
using YTDownloaderAPI.wrapper;

namespace YTDownloaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        PlayListContext _context;
        public PlayListController(PlayListContext playListContext)
        {
            _context = playListContext;
        }

        [HttpGet]
        public YoutubePlayList GetPlayList(string url)
        {
            return YoutubeExplodeHelper.ListPlayListVideosAsync(url).Result;
        }

        [HttpGet]
        [Route("SavedPlayList")]
        public PlayList GetSavedPlayList(int id)
        {
            return _context.PlayLists.Where(x => x.Id == id).First();
        }

        [HttpPut]
        public PlayList SavePlayList(PlayList playList)
        {
            YoutubePlayList youtubePlayList = YoutubeExplodeHelper.ListPlayListVideosAsync(playList.PlayListUrl).Result;

            var playListEntityEntry = _context.PlayLists.Add(playList);
            _context.SaveChanges();

            var playListEntity = playListEntityEntry.Entity;
            youtubePlayList.Videos.ForEach(video =>
            {
                _context.Audios.Add(new Audio()
                {
                    YtId = video.Id,
                    Url = video.Url,
                    ChannelTitle = video.ChannelTitle,
                    Title = video.Title,
                    PlayList = playListEntity
                });
            });

            _context.SaveChanges();

            return playListEntityEntry.Entity;
        }
    }
}
