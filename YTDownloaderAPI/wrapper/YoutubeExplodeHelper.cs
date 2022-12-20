using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;

namespace YTDownloaderAPI.wrapper
{
    public class YoutubeExplodeHelper
    {
        public static async Task<List<VideoSearchResult>> SearchVideo(string search)
        {
            var youtube = new YoutubeClient();
            var result = await youtube.Search.GetVideosAsync(search);
            var resultList = result.ToList();

            return resultList;
        }

        public static async Task<Stream> DownLoadVideoStream(string videoUrl)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetAudioOnlyStreams().First();
            return await youtube.Videos.Streams.GetAsync(streamInfo);
        }

        public static async Task DownLoadVideo(string videoUrl, string path)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetAudioOnlyStreams().First();
            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"{path}/{Utils.RandomString(10)}.{streamInfo.Container}");
        }

        public static async Task<YoutubePlayList> ListPlayListVideosAsync(string playListURL)
        {
            var youtube = new YoutubeClient();
            var youTubePlayList = await youtube.Playlists.GetAsync(playListURL);

            var playList = new YoutubePlayList()
            {
                Id = youTubePlayList.Id.Value,
                Title = youTubePlayList.Title,
                Url = youTubePlayList.Url,
                Description = youTubePlayList.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var playListVideos = await youtube.Playlists.GetVideosAsync(playListURL);
            foreach (var pVideo in playListVideos)
            {
                playList.Videos.Add(new Video()
                {
                    Id = pVideo.Id.Value,
                    Title = pVideo.Title,
                    Url = pVideo.Url,
                    ChannelTitle = pVideo.Author.ChannelTitle,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });
            }

            return playList;
        }
    }
}
