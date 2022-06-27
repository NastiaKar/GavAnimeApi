using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Algolia.Search.Clients;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GavAnimeAPI.Models;
using GavAnimeAPI.Constant;

namespace GavAnimeAPI.Clients;

public class AnimeClient : IDisposable
{
    private HttpClient _httpClient;
    private static string _address;
    private static string _accept = "application/vnd.api+json";
    private static string _contentType = "application/vnd.api+json";

    public AnimeClient()
    {
        _address = Constants.baseAddress;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_address);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_accept)); //ACCEPT
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", _contentType);
    }

    public async Task<AnimeModel> GetAnimeById(string id)
    {
        var response = await _httpClient.GetAsync($"anime/{id}");
        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<AnimeModel>(content);
        return result;
    }

    public async Task<AnimeModelArray> GetGenreByAnimeId(string id)
    {
        var response = await _httpClient.GetAsync($"anime/{id}/genres");
        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<AnimeModelArray>(content);
        return result;
    }

    public async Task<AnimeModelArray> GetAnimeList()
    {
        var response = await _httpClient.GetAsync($"trending/anime");
        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<AnimeModelArray>(content);
        return result;
    }

    public async Task<AnimeModelArray> GetListByCategory(string category)
    {
        var response = await _httpClient.GetAsync($"anime?filter[categories]={category}&sort=-userCount" + 
                                                  "&page[limit]=20");
        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<AnimeModelArray>(content);
        return result;
    }

    public async Task<AnimeModelArray> GetAnimeByTitle(string title)
    {
        var response = await _httpClient.GetAsync($"anime?filter[text]={title}");
        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<AnimeModelArray>(content);
        return result;
    }

    public async Task<AnimeModelArray> GetAnimeByRating(string rating)
    {
        var response = await _httpClient.GetAsync($"anime?filter[ageRating]={rating.ToUpper()}&sort=-userCount" +
                                                  "&page[limit]=20");
        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<AnimeModelArray>(content);
        return result;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}