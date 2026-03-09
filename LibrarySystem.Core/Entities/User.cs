using LibrarySystem.Core.Interfaces;
namespace LibrarySystem.Core.Entities;

public abstract class User(string id, string name, string phone) : ISummarizable
{
    public string Id { get; init; } = id;
    public string Name { get; protected set; } = name;
    public string Phone { get; protected set; } = phone;

    public abstract string GetSummary();
}
