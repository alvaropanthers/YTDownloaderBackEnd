using YTDownloader.wrapper;

//args = new string[2];
//args[0] = "https://www.youtube.com/watch?v=-xNzx_9APdE";
//args[1] = "C:\\Users\\Alvaro.Orozco\\Documents";

if (args.Length > 0)
{
    string videoUrl = args[0], path = args[1];
    //await YoutubeExplodeHelper.DownLoadVideo(, );
    await YoutubeExplodeHelper.DownLoadVideo(videoUrl, path);
    Console.WriteLine("Operation completed!");
}
else
{
    Console.WriteLine("Missing arguments -url or -path");
}