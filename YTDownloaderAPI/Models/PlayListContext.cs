using Microsoft.EntityFrameworkCore;

namespace YTDownloaderAPI.Models
{
    public class PlayListContext : DbContext
    {
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public PlayListContext(DbContextOptions<PlayListContext> dbContextOptions): base(dbContextOptions)
        { }
    }
}
