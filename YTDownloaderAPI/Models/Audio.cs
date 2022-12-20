namespace YTDownloaderAPI.Models
{
    public class Audio
    {
        public int Id { get; set; }
        public string YtId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string ChannelTitle { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int? PlayListId { get; set; }
        public PlayList PlayList { get; set; }
    }
}
