// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Globalization;

namespace CliCalc.Domain;

internal sealed class MessageTypes
{
    public sealed class ResetMessage;

    public sealed class ExitMessage;

    public sealed class CultureChange
    {
        public required CultureInfo CultureInfo { get; init; }
    }

    public sealed class ChangeWorkdir
    {
        public required string Path { get; init; }
    }

    public static class DataSets
    {
        public const string Variables = "Variables";
        public const string VariablesWithTypes = "VariablesWithTypes";
        public const string GlobalDocumentation = "GlobalDocumentation";
        public const string HasmarksDocumentation = "HasmarksDocumentation";
        public const string AngleMode = "AngleMode";
        public const string Workdir = "Workdir";
    }
}
