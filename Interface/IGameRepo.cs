using GameStore.Data;
using GameStore.Entities;
using GameStore.Interface;
namespace GameStore.Interface;

public interface IGameRepo{

    public Task<VideoGame> AddGame(VideoGame vgame);
    public Task<VideoGame> UpdateGame(VideoGame vgame);

    Task<bool> DeleteGame(int id);

    Task<IEnumerable<VideoGame>> GetAllGames(); // Get all games
    Task<VideoGame?> GetGameById(int id);       // Get a game by ID

}