using GavAnimeAPI.Controllers;
using GavAnimeAPI.Models;

namespace GavAnimeAPI.Clients;

public interface IDynamoDbClient
{
    public Task<AnimeDbRepository> GetDataById(string id, string userId);
    public Task<List<AnimeDbRepository>> GetFavoriteAnimeList(string userId);
    public Task<bool> PutAnime(AnimeDbRepository anime);
    public Task<bool> DeleteAnime(AnimeDbRepository anime, string userId);
}