namespace LibrarySystem.Core.Results;

public class Error(string code, string message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public string Code { get; init; } = code;
    public string Message { get; init; } = message;

}
