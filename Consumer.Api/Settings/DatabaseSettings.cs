namespace Consumer.Api.Settings;

public sealed class DatabaseSettings
{
    public string ConnectionString { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}