using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using GavAnimeAPI.Clients;
using GavAnimeAPI.Constant;

var builder = WebApplication.CreateBuilder(args);

var credentials = new BasicAWSCredentials(Constants.AccessKey, Constants.SecretKey);
var config = new AmazonDynamoDBConfig()
{
    RegionEndpoint = RegionEndpoint.EUNorth1
};
var client = new AmazonDynamoDBClient(credentials, config);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAmazonDynamoDB>(client);
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddSingleton<IDynamoDbClient, DynamoDbClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();