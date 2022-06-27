using GavAnimeAPI.Clients;
using GavAnimeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GavAnimeAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class AnimeDbController : ControllerBase
{
    private readonly IDynamoDbClient _dbClient;
    
    public AnimeDbController(IDynamoDbClient dynamoDbClient)
    {
        _dbClient = dynamoDbClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavAnimeById(string id, string userId)
    {
        var result = await _dbClient.GetDataById(id, userId);

        if (result == null)
            return NotFound("This anime is not found in Database!");
        
        var animeModel = new AnimeModel
        {
            Data = new Data
            {
                Id = result.Id,
                Attributes = new Attributes
                {
                    Synopsis = result.Synopsis,
                    Titles = new Titles
                    {
                        En_Jp = result.EnJpTitle,
                        Ja_Jp = result.JaJpTitle
                    }
                }
            }
        };
        return Ok(animeModel);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllItems(string userId)
    {
        var response = await _dbClient.GetFavoriteAnimeList(userId);

        if (response == null)
            return NotFound("No items in Database.");

        var result = response
            .Select(x => new AnimeModel
            {
                Data = new Data
                {
                    Id = x.Id,
                    Attributes = new Attributes
                    {
                        Synopsis = x.Synopsis,
                        Titles = new Titles
                        {
                            En_Jp = x.EnJpTitle,
                            Ja_Jp = x.JaJpTitle
                        }
                    }
                }
            })
            .ToList();

        return Ok(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> PutDataToDb(AnimeDbRepository anime, string userId)
    {
        var data = new AnimeDbRepository
        {
            Id = anime.Id,
            EnJpTitle = anime.EnJpTitle,
            JaJpTitle = anime.JaJpTitle,
            Synopsis = anime.Synopsis,
            UserId = userId
        };

        var result = await _dbClient.PutAnime(data);

        if (result == false)
        {
            return BadRequest("Cannot insert data to database. See console log.");
        }
        
        return Ok("Data has been successfully added.");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteDataFromDb(AnimeDbRepository anime, string userId)
    {
        var data = new AnimeDbRepository
        {
            Id = anime.Id,
            EnJpTitle = anime.EnJpTitle,
            JaJpTitle = anime.JaJpTitle,
            Synopsis = anime.Synopsis,
            UserId = userId
        };

        var result = await _dbClient.DeleteAnime(data, userId);

        if (result == false)
            return BadRequest("Couldn't delete data from database. See console log.");
        return Ok("Data has been successfully deleted.");
    }
}