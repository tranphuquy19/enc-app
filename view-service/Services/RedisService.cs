using StackExchange.Redis;

public interface IRedisService
{
    Task<long> IncrementViewCountAsync();
    Task<long> GetViewCountAsync();
}

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redis;
    private const string ViewCountKey = "view:count";

    public RedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<long> IncrementViewCountAsync()
    {
        var db = _redis.GetDatabase();
        return await db.StringIncrementAsync(ViewCountKey);
    }

    public async Task<long> GetViewCountAsync()
    {
        var db = _redis.GetDatabase();
        var value = await db.StringGetAsync(ViewCountKey);
        return value.HasValue ? (long)value : 0;
    }
}
