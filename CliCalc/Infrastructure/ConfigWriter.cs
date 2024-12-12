using System.Text.Json;

using CliCalc.Domain;

namespace CliCalc.Infrastructure;

internal sealed class ConfigWriter : ConfigBase
{
    public async Task WriteAsync(Configuration configuration)
    {
        using var configStream = File.Create(ConfigPath);
        await JsonSerializer.SerializeAsync(configStream, configuration, _options);
    }
}
