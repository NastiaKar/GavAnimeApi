using Microsoft.AspNetCore.Mvc;
using GavAnimeAPI.Models;
using GavAnimeAPI.Clients;

namespace GavAnimeAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class MainAnimeController : ControllerBase
{
    [HttpGet("byId")]
    public async Task<ActionResult<AnimeModel>> GetAnimeById(string id)
    {
        AnimeClient animeClient = new AnimeClient();
        var result = await animeClient.GetAnimeById(id);

        if (result.Data == null)
            return BadRequest("Not found!");

        return Ok(result);
    }
    
    [HttpGet("byRating")]
    public async Task<ActionResult<AnimeModelArray>> GetAnimeByRating(string rating)
    {
        AnimeClient animeClient = new AnimeClient();
        var result = await animeClient.GetAnimeByRating(rating);

        if (result.Data == null)
            return BadRequest("Not found!");

        return Ok(result);
    }
    
    [HttpGet("byTitle")]
    public async Task<ActionResult<AnimeModelArray>> GetAnimeByTitle(string title)
    {
        AnimeClient animeClient = new AnimeClient();
        var result = await animeClient.GetAnimeByTitle(title);

        if (result.Data == null)
            return BadRequest("Not found!");

        return Ok(result);
    }
    
    [HttpGet("list")]
    public async Task<ActionResult<AnimeModelArray>> GetAnimeList()
    {
        AnimeClient animeClient = new AnimeClient();
        var result = await animeClient.GetAnimeList();

        if (result.Data == null)
            return BadRequest("Not found!");

        return Ok(result);
    }
    
    [HttpGet("genreById")]
    public async Task<ActionResult<AnimeModelArray>> GetGenreAnimeById(string id)
    {
        AnimeClient animeClient = new AnimeClient();
        var result = await animeClient.GetGenreByAnimeId(id);

        if (result.Data == null)
            return BadRequest("Not found!");

        return Ok(result);
    }
    
    [HttpGet("listByCategory")]
    public async Task<ActionResult<AnimeModelArray>> GetListByCategory(string category)
    {
        AnimeClient animeClient = new AnimeClient();
        var result = await animeClient.GetListByCategory(category);

        if (result.Data == null)
            return BadRequest("Not found!");

        return Ok(result);
    }
}