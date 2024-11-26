namespace CliCalc.Domain;

internal class HashMarkResult
{
    public string Content { get; }

    public override string ToString()
        => Content;

    public HashMarkResult(string content)
    {
        Content = content;
    }

    public HashMarkResult() 
    {
        Content = string.Empty;
    }
}
