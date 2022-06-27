using System.Net;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using GavAnimeAPI.Models;
using GavAnimeAPI.Constant;
using GavAnimeAPI.Extensions;

namespace GavAnimeAPI.Clients;

public class DynamoDbClient : IDynamoDbClient, IDisposable
{
    public string _tableName;
    private readonly IAmazonDynamoDB _dynamoDb;
    
    public DynamoDbClient(IAmazonDynamoDB dynamoDb)
    {
        _dynamoDb = dynamoDb;
        _tableName = Constants.TableName;
    }

    public async Task<AnimeDbRepository> GetDataById(string id, string userId)
    {
        var item = new GetItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                {"Id", new AttributeValue {S = id} },
                {"UserId", new AttributeValue {S = userId} }
            }
        };
        
        var response = await _dynamoDb.GetItemAsync(item);
        var result = response.Item.ToClass<AnimeDbRepository>();
        return result;
    }

    public async Task<List<AnimeDbRepository>> GetFavoriteAnimeList(string userId)
    {
        var request = new ScanRequest
        {
            TableName = _tableName,
            ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                {":id", new AttributeValue { S = userId }}
            },
            FilterExpression = "UserId = :id"
        };

        var response = await _dynamoDb.ScanAsync(request);
        if (response.Items == null || response.Items.Count == 0)
            return null;

        return response.Items.Select(item => item.ToClass<AnimeDbRepository>()).ToList();
    }

    public async Task<bool> PutAnime(AnimeDbRepository anime)
    {
        var request = new PutItemRequest
        {
            TableName = _tableName,
            Item = new Dictionary<string, AttributeValue>
            {
                {"Id", new AttributeValue {S = anime.Id}},
                {"EnJpTitle", new AttributeValue {S = anime.EnJpTitle}},
                {"JaJpTitle", new AttributeValue {S = anime.JaJpTitle}},
                {"Synopsis", new AttributeValue {S = anime.Synopsis}},
                {"UserId", new AttributeValue {S = anime.UserId}}
            }
        };

        try
        {
            var response = await _dynamoDb.PutItemAsync(request);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Error message:\n {e}");
            return false;
        }
    }

    public async Task<bool> DeleteAnime(AnimeDbRepository anime, string userId)
    {
        var request = new DeleteItemRequest
        {
            TableName = _tableName,
            Key = new Dictionary<string, AttributeValue>
            {
                {"Id", new AttributeValue {S = anime.Id}},
                {"UserId", new AttributeValue {S = userId}}
            }
        };

        try
        {
            var response = await _dynamoDb.DeleteItemAsync(request);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Error message:\n {e}");
            return false;
        }
    }

    public async Task<AnimeDbRepository> ModelToRep(AnimeModel anime, string userId)
    {
        AnimeDbRepository rep = new AnimeDbRepository
        {
            Id = anime.Data.Id,
            EnJpTitle = anime.Data.Attributes.Titles.En_Jp,
            JaJpTitle = anime.Data.Attributes.Titles.Ja_Jp,
            Synopsis = anime.Data.Attributes.Synopsis,
            UserId = userId
        };

        return rep;
    }

    public void Dispose()
    {
        _dynamoDb.Dispose();
    }
}