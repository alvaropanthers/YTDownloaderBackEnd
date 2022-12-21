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
            var playlist = _context.PlayLists
                .Where(x => x.Id == id)
                .First();

            playlist.Audios = _context.Audios
                .Where(x => x.PlayListId == playlist.Id)
                .ToList();
            
            return playlist;
        }

        [HttpPut]
        public PlayList SavePlayList(PlayList playList)
        {
            YoutubePlayList youtubePlayList = YoutubeExplodeHelper.ListPlayListVideosAsync(playList.PlayListUrl).Result;

            playList.Audios = new List<Audio>();
            youtubePlayList.Videos.ForEach(video =>
            {
                playList.Audios.Add(new Audio()
                {
                    YtId = video.Id,
                    Url = video.Url,
                    ChannelTitle = video.ChannelTitle,
                    Title = video.Title
                });
            });

            var playListEntityEntry = _context.PlayLists.Add(playList);
            _context.SaveChanges();

            return playListEntityEntry.Entity;
        }
    }
}
