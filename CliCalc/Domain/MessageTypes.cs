﻿using System.Globalization;

namespace CliCalc.Domain;

internal sealed class MessageTypes
{
    public sealed class ResetMessage;

    public sealed class ExitMessage;

    public sealed class CultureChange
    {
        public required CultureInfo CultureInfo { get; init; }
    }

    public static class DataSets
    {
        public const string Variables = "Variables";
    }
}
