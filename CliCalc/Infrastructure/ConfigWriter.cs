// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

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
