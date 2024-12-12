using System.Text.Json;

using CliCalc.Domain;

namespace CliCalc.Infrastructure;

internal class ConfigReader : ConfigBase
{
    public async Task<Configuration> ReadAsync()
    {
        if (!File.Exists(ConfigPath))
            return new Configuration();

        using var configStream = File.OpenRead(ConfigPath);

        var result = await JsonSerializer.DeserializeAsync<Configuration>(configStream, _options);
        if (result == null)
            throw new InvalidOperationException("Deserialize failed");

        return result;
    }
}
