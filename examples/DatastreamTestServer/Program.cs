using Microsoft.Extensions.Options;
using SchematicHQ.Client;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Cache;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

string apiKey = Environment.GetEnvironmentVariable("SCHEMATIC_API_KEY") ?? 
    throw new InvalidOperationException("SCHEMATIC_API_KEY environment variable is not set");

var redisConfig = new RedisCacheConfig
{
    Endpoints = new List<string> { "localhost:6379" }, // Replace with your Redis connection string
    KeyPrefix = "schematic-test:" // Optional key prefix

};

var datastreamOptions = new DatastreamOptions
{
    CacheTTL = TimeSpan.FromMilliseconds(5000),
};

datastreamOptions.WithRedisCache(redisConfig);

var options = new ClientOptions
{
    BaseUrl = "https://datastream.schematichq.com",
    UseDatastream = true,
    DatastreamOptions = datastreamOptions
};

Schematic schematic = new Schematic(apiKey, options);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "Welcome to the Schematic Datastream Test Server!");

app.MapPost("/checkflag", async (CheckFlagRequestBody request) =>
{
    var result = await schematic.CheckFlag(request.FlagKey, request.Company, request.User);
    return Results.Ok(result);
}).WithName("CheckFlag");

app.Run();

record CheckFlagRequestBody
{
    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; init; } = null;
    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; init; } = null;
    [JsonPropertyName("flag-key")]
    public string FlagKey { get; init; } = string.Empty;
};