namespace CliCalc.Domain;

internal class HashMarkResult
{
    public string Content { get; }

    public override string ToString()
        => Content;

    public bool Success { get; }

    public HashMarkResult(string content)
    {
        Success = false;
        Content = content;
    }

    public HashMarkResult() 
    {
        Success = true;
        Content = string.Empty;
    }
}
