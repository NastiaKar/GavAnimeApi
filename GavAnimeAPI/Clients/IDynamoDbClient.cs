using GavAnimeAPI.Controllers;
using GavAnimeAPI.Models;

namespace GavAnimeAPI.Clients;

public interface IDynamoDbClient
{
    Task<AnimeDbRepository> GetDataById(string id, string userId);
    Task<List<AnimeDbRepository>> GetFavoriteAnimeList(string userId);
    Task<bool> PutAnime(AnimeDbRepository anime);
    Task<bool> DeleteAnime(AnimeDbRepository anime, string userId);
}
