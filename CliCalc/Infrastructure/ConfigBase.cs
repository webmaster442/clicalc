﻿// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Text.Json;

namespace CliCalc.Infrastructure;

internal abstract class ConfigBase
{
    protected readonly JsonSerializerOptions _options;

    public static readonly string ConfigPath
        = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "clicalc.json");

    public ConfigBase()
    {
        _options = new JsonSerializerOptions
        {
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
    }
}
