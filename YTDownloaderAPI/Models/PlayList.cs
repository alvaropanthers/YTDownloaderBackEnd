namespace YTDownloaderAPI.Models
{
    public class PlayList
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string PlayListUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Audio> Audios { get; set; }
    }
}
