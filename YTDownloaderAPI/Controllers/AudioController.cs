using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;
using YTDownloaderAPI.wrapper;

namespace YTDownloaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAudioFileAsync(string name)
        {
            Stream audioStream = YoutubeExplodeHelper.DownLoadVideoStream(name).Result;
            Byte[] b = Utils.ReadFully(audioStream);
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(b)
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");

            return File(b, "audio/mp4");
        }

        [HttpGet]
        [Route("/download")]
        public async Task DownloadAudioFileAsync(string videoUrl)
        {
            await YoutubeExplodeHelper.DownLoadVideo(videoUrl, "C:\\Users\\alvaro\\Documents\\audios");
        }
    }
}
