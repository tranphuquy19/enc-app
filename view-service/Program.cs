using StackExchange.Redis;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var redisConnection = builder.Configuration["REDIS_CONNECTION"] ?? "localhost:6379";
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(redisConnection));
builder.Services.AddScoped<IRedisService, RedisService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", async (IRedisService redisService) =>
{
    try
    {
        var newCount = await redisService.IncrementViewCountAsync();
        return Results.Ok(new { views = newCount });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error accessing Redis: {ex.Message}");
    }
});

app.MapGet("/count", async (IRedisService redisService) =>
{
    try
    {
        #region stress block
        async Task<double> GenerateLoadAsync()
        {
            var memoryEater = new ConcurrentBag<byte[]>();
            var result = 0.0;

            for (var j = 0; j < 1000000; j++)
            {
                result += Math.Sqrt(j) * Math.Sin(j);
            }

            memoryEater.Add(new byte[1024 * 1024]); // 1MB
            await Task.Delay(100); // keep 100ms

            return result;
        }

        var loadTasks = Enumerable.Range(0, 10)
            .Select(_ => GenerateLoadAsync())
            .ToList();

        #endregion

        var count = await redisService.GetViewCountAsync();
        return Results.Ok(new { views = count });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error accessing Redis: {ex.Message}");
    }
});

app.MapGet("/health", async (IConnectionMultiplexer redis) =>
{
    try
    {
        var db = redis.GetDatabase();
        await db.PingAsync();
        return Results.Ok(new { status = "healthy", redis = "connected" });
    }
    catch
    {
        return Results.StatusCode(503);
    }
});

app.Run();
