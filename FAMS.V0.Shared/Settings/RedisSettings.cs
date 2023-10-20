namespace FAMS.V0.Shared.Settings;

public class RedisSettings
{
    public string Host { get; init; }
    public string Port { get; init; }
    public bool AllowAdmin { get; init; }

    public string GetConnectionString()
    {
        return $"{Host}:{Port}";
    }
}