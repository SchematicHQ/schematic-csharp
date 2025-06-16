using Microsoft.Extensions.Options;
using SchematicHQ.Client;
using SchematicHQ.Client.Cache;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

string apiKey = Environment.GetEnvironmentVariable("SCHEMATIC_API_KEY") ?? 
    throw new InvalidOperationException("SCHEMATIC_API_KEY environment variable is not set");

var options = new ClientOptions
{
    BaseUrl = "http://localhost:8080",
    UseDatastream = true,
    DatastreamOptions = new SchematicHQ.Client.Datastream.DatastreamOptions
    {
        CacheTTL = TimeSpan.FromHours(24)
    }
};

options.WithRedisCache(
    new List<string> { "localhost:6379" }, // Redis connection string
    keyPrefix: "schematic-test:", // Optional key prefix
    cacheTtl: TimeSpan.FromHours(24)// Optional cache TTL
);

options.WithHttpClient(new HttpClient
{
    BaseAddress = new Uri("http://localhost:8080")
});

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