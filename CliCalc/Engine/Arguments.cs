// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System.Collections;

namespace CliCalc.Engine;

internal sealed class Arguments : IReadOnlyList<string>
{
    private readonly List<string> _args;

    public string CommandName { get; private set; }

    public int Count => _args.Count;

    public string this[int index] => _args[index];

    public Arguments(string input)
    {
        CommandName = string.Empty;
        _args = new List<string>();

        if (string.IsNullOrEmpty(input))
        {
            return;
        }

        int length = input.Length;
        bool inQuotes = false;
        int start = 0;

        for (int i = 0; i < length; i++)
        {
            if (input[i] == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (input[i] == ' ' && !inQuotes)
            {
                if (i > start)
                {
                    Store(input[start..i]);
                }
                start = i + 1; // Move to the next token
            }
        }

        if (length > start)
        {
            Store(input[start..]);
        }
    }

    private void Store(string item)
    {
        if (string.IsNullOrEmpty(CommandName))
            CommandName = item;
        else
            _args.Add(item);
    }

    public IEnumerator<string> GetEnumerator()
        => _args.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _args.GetEnumerator();
}
