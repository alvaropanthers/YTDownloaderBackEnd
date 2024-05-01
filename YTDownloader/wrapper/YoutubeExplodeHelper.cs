using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;

namespace YTDownloader.wrapper
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

        public static string ToPP(YoutubeExplode.Videos.Video video, string fileName)
        {
            return $"""
                id={video.Id};
                url={video.Url};
                title={video.Title};
                duration={video.Duration};
                author={video.Author};
                uploadDate={video.UploadDate};
                fileName={fileName};
                """;
        }

        public static async Task DownLoadVideo(string videoUrl, string path)
        {
            Console.ForegroundColor = ConsoleColor.White;

            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetAudioOnlyStreams().First();

            if (streamInfo == null)
                throw new NullReferenceException();

            var randomName = Utils.RandomString(10);
            var fileName = $"{randomName}.{streamInfo.Container}";
            var fullPath = $"{path}\\{fileName}";

            try
            {
                var videoInfo = await youtube.Videos.GetAsync(videoUrl);
                var ppFormatted = ToPP(videoInfo, fileName);
                
                var fullFileNameWithoutExtension = $"{path}\\{randomName}";
                var ppFilePath = $"{fullFileNameWithoutExtension}.pp";

                Console.WriteLine("----------------------------");
                Console.WriteLine("Creating .pp file");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n" + ppFormatted + "\n");
                File.WriteAllText(ppFilePath, ppFormatted);
                Console.WriteLine($".pp file created \n {ppFilePath}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------");
                Console.WriteLine();

                Console.WriteLine("----------------------------");
                Console.WriteLine($"Downloading File");
                await youtube.Videos.Streams.DownloadAsync(streamInfo, fullPath);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"File downloaded \n {fullPath}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------");

                Console.WriteLine("----------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Creating wav file");

                var _waveSource =
                CodecFactory.Instance.GetCodec(fullPath)
                    .ToSampleSource()
                    .ToMono()
                    .ToWaveSource();

                var _soundOut = new WasapiOut();
                _soundOut.Initialize(_waveSource);

                _soundOut.WaveSource.WriteToFile($"{fullFileNameWithoutExtension}.wav");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("----------------------------");
                Console.WriteLine();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error creating .pp file");
                Console.WriteLine(ex.Message);

                Console.ForegroundColor = ConsoleColor.White;
            }
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
