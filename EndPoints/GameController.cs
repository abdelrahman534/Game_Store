
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
    public async Task<IActionResult> AddGame([FromBody] VideoGame vgame)
    {
        if (vgame == null)
        {
            return BadRequest(new { message = "Invalid game data provided." });
        }

        try
        {
            await _GameRepo.AddGame(vgame); // AddGame modifies `vgame` directly
            return CreatedAtAction(nameof(AddGame), new { id = vgame.Id }, vgame);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }





    // [HttpPut
    // ("UpdateGames")]
    // public async Task<IActionResult> UpdateGame(VideoGame vgame)
    // {


    //     await _GameRepo.UpdateGame(vgame);
    //     return Ok(vgame);
    // }

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


[HttpPut("{id}")]
public async Task<IActionResult> UpdateGame(int id, [FromBody] VideoGameUpdateDto updatedGame)
{

    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState); // Return validation errors
    }

    try
    {
        // Call the repository's update method
        var result = await _GameRepo.UpdateGame(id, updatedGame);

        return Ok(result); // Return the updated game
    }
    catch (KeyNotFoundException ex)
    {
        // Handle case where the game was not found
        return NotFound(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        // Handle unexpected errors
        return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
    }
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

    [HttpGet("GETAllGamesInStock")]
    public async Task<IActionResult> GetAllInStockGames()
{
    var gamesInStock = await _GameRepo.GetAllInStock();
    return Ok(gamesInStock);
}

[HttpGet("GetGameInStockId/{id}")]
public async Task<IActionResult> GetGameInStockById(int id)
{
    var game = await _GameRepo.GetInStockById(id);
    if (game == null)
    {
        return NotFound(new { message = "Game not found or out of stock." });
    }
    return Ok(game);
}

}