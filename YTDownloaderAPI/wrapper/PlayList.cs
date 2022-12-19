namespace YTDownloaderAPI.wrapper
{
    public class PlayList
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public List<Video> Videos { get; set; } = new List<Video>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
