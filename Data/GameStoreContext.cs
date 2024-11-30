using GameStore.Entities;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Data;
public class GameStoreContext:DbContext
{
    public GameStoreContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
    public DbSet<Game> Games => Set<Game>();
    public DbSet<VideoGame> VideoGame => Set<VideoGame>();
    public DbSet<Genre> Genres => Set<Genre>();

}
