
using GameStore.DTOs;
using GameStore.Entities;
using GameStore.Interface;
using GameStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.EndPoints;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{

    private readonly IGameRepo _GameRepo;

    public GameController(IGameRepo GameRepo)
    {
        _GameRepo = GameRepo;
    }

    [HttpPost("AddGames")]
    public async Task<IActionResult> AddGame(VideoGame vgame)
    {

        await _GameRepo.AddGame(vgame);
        return Ok(vgame);
    }

    [HttpPut
    ("UpdateGames")]
    public async Task<IActionResult> UpdateGame(VideoGame vgame){


        await _GameRepo.UpdateGame(vgame);
        return Ok(vgame);
    }

    [HttpDelete("DeleteGames/{id}")]
public async Task<IActionResult> DeleteGame(int id)
{
    var result = await _GameRepo.DeleteGame(id);

    if (!result)
    {
        return NotFound(new { message = "Game not found with the provided ID." });
    }

    return Ok(new { message = "Game deleted successfully." });
}

[HttpGet("GetAllGames")]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _GameRepo.GetAllGames();
        return Ok(games);
    }

    [HttpGet("GetGameById/{id}")]
    public async Task<IActionResult> GetGameById(int id)
    {
        var game = await _GameRepo.GetGameById(id);

        if (game == null)
        {
            return NotFound(new { message = "Game not found with the provided ID." });
        }

        return Ok(game);
    }

}