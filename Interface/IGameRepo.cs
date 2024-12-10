using GameStore.Data;
using GameStore.Entities;
using GameStore.Interface;
using Microsoft.AspNetCore.Mvc;
namespace GameStore.Interface;


public interface IGameRepo
{

    Task<VideoGame> AddGame(VideoGame vgame);
    // public Task<VideoGame> UpdateGame(VideoGame vgame);

    public Task<VideoGame> UpdateGame(int id, VideoGameUpdateDto updatedGame);

    Task<bool> DeleteGame(int id);
    Task<IEnumerable<VideoGame>> GetAllGames(); // Get all games
    Task<VideoGame?> GetGameById(int id);       // Get a game by ID
    public Task<IEnumerable<VideoGame>> GetAllInStock();
    public Task<VideoGame?> GetInStockById(int id);

}