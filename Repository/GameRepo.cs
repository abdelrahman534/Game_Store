using GameStore.Data;
using GameStore.DTOs;
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
        var existingGame = await _context.VideoGame.FirstOrDefaultAsync(g => g.Name == vgame.Name);
        if (existingGame == null)
        {
            vgame.Id = 0; // to ensure EF Core treats it as a new record
            await _context.VideoGame.AddAsync(vgame);
            _context.SaveChanges();
            return vgame;
        }

        else
        {
            throw new Exception($"A game with the name '{vgame.Name}' already exists.");
        }

    }

    // public async Task<VideoGame> UpdateGame(VideoGame vgame)
    // {

    //     // Find the existing game by Id
    //     var existingGame = await _context.VideoGame.FindAsync(vgame.Id);

    //     if (existingGame == null)
    //     {
    //         throw new KeyNotFoundException($"VideoGame with Id {vgame.Id} not found.");
    //     }

    //     if (vgame.Name != null)
    //     {
    //         existingGame.Name = vgame.Name;
    //     }
    //     if (vgame.Price.HasValue)
    //     {
    //         existingGame.Price = vgame.Price.Value;
    //     }

    //     // Save changes to the database
    //     await _context.SaveChangesAsync();

    //     return existingGame;
    // }


public async Task<VideoGame> UpdateGame(int id, VideoGameUpdateDto updatedGame)
{
    // Find the existing game by ID
    var existingGame = await _context.VideoGame.FindAsync(id);

    if (existingGame == null)
    {
        throw new KeyNotFoundException($"VideoGame with Id {id} not found.");
    }

    // Update only the fields that are explicitly provided in the DTO
    if (!string.IsNullOrWhiteSpace(updatedGame.Name) && updatedGame.Name != "string")
    {
        existingGame.Name = updatedGame.Name;
    }
    if (!string.IsNullOrWhiteSpace(updatedGame.Genre) && updatedGame.Genre != "string")
    {
        existingGame.Genre = updatedGame.Genre;
    }
    if (updatedGame.Stock.HasValue) // Nullable check for numeric fields
    {
        existingGame.Stock = updatedGame.Stock.Value;
    }
    if (!string.IsNullOrWhiteSpace(updatedGame.Platform) && updatedGame.Platform != "string")
    {
        existingGame.Platform = updatedGame.Platform;
    }
    if (updatedGame.Price.HasValue)
    {
        existingGame.Price = updatedGame.Price.Value;
    }

    // Save changes to the database
    await _context.SaveChangesAsync();

    return existingGame;
}





    public async Task<bool> DeleteGame(int id)
    {

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


     public async Task<IEnumerable<VideoGame>> GetAllInStock()
    {
        return await _context.VideoGame.Where(g => g.Stock > 0).ToListAsync();
    }

    public async Task<VideoGame?> GetInStockById(int id)
    {
        return await _context.VideoGame
                             .Where(g => g.Id == id && g.Stock > 0)
                             .FirstOrDefaultAsync();
    }

}