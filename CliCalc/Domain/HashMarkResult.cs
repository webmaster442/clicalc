namespace CliCalc.Domain;

internal class HashMarkResult
{
    public string Content { get; }

    public override string ToString()
        => Content;

    public bool Success { get; }

    public HashMarkResult(bool success, string content)
    {
        Success = success;
        Content = content;
    }

    public HashMarkResult() 
    {
        Success = true;
        Content = string.Empty;
    }
}
