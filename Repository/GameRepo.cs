using GameStore.Data;
using GameStore.Entities;
using GameStore.Interface;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Repository;

public class GameRepo : IGameRepo
{
    private readonly GameStoreContext _context;

    public GameRepo(GameStoreContext context)
    {
        _context = context;
    }

    public async Task<VideoGame> AddGame(VideoGame vgame)
    {
        vgame.Id = 0; // Ensures EF Core treats it as a new record
        await _context.VideoGame.AddAsync(vgame);
        _context.SaveChanges();
        return vgame;

    }

    public async Task<VideoGame> UpdateGame(VideoGame vgame){
        
        // Find the existing game by Id
        var existingGame = await _context.VideoGame.FindAsync(vgame.Id);

        if (existingGame == null)
        {
            throw new KeyNotFoundException($"VideoGame with Id {vgame.Id} not found.");
        }

        // Update the properties
        existingGame.Name = vgame.Name;
        existingGame.Price = vgame.Price;

        // Save changes to the database
        await _context.SaveChangesAsync();

        return existingGame;
    }

     public async Task<bool> DeleteGame(int id){

        var game = await _context.VideoGame.FindAsync(id);
        if (game == null)
        {
            return false; // Game not found
        }

        _context.VideoGame.Remove(game);
        await _context.SaveChangesAsync();
        return true; // Game deleted successfully
    }

    public async Task<IEnumerable<VideoGame>> GetAllGames()
    {
        return await _context.VideoGame.ToListAsync();
    }

    public async Task<VideoGame?> GetGameById(int id)
    {
        return await _context.VideoGame.FindAsync(id);
    }

}