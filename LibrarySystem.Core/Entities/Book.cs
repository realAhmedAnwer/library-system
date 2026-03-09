using LibrarySystem.Core.Interfaces;
namespace LibrarySystem.Core.Entities;

public class Book(string ISBN, string title, string authorName, string category, int publicationYear) : ISummarizable
{
    internal static int CopiesCounter { get; set; } = 0;
    public string ISBN { get; private set; } = ISBN;
    public string Title { get; private set; } = title;
    public string AuthorName { get; private set; } = authorName;
    public string Category { get; private set; } = category;
    public int PublicationYear { get; private set; } = publicationYear;
    public Book(string ISBN, string title) : this(ISBN, title, "Unkown", "General", 0) { }
    public string GetSummary() =>
        $"""
        ISBN             : {ISBN}
        Title            : {Title}
        Author           : {AuthorName}
        Category         : {Category}
        Publication Year : {PublicationYear}
        """;
}
