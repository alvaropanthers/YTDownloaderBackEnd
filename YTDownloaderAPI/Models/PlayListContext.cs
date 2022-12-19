using Microsoft.EntityFrameworkCore;

namespace YTDownloaderAPI.Models
{
    public class PlayListContext : DbContext
    {
        public DbSet<PlayList> PlayList { get; set; }

    }
}
