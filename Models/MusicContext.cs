using Microsoft.EntityFrameworkCore;

namespace dot_net_api.Models
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options) : base(options){} 

        public DbSet<SongItem>  SongItems { get; set; }
    }
}