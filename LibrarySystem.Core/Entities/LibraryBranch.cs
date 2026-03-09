using LibrarySystem.Core.Extensions;
using LibrarySystem.Core.Interfaces;
using LibrarySystem.Core.Results;
namespace LibrarySystem.Core.Entities;

public class LibraryBranch(string id, string name, string address, string phone, string openingHours, Librarian manager) : ISummarizable
{
    public static class Errors
    {
        public static readonly Error InvalidMemberId = new("LibraryBranch.InvalidMemberId", "Cannot find Member with this id!");
        public static readonly Error InvalidBookCopyId = new("LibraryBranch.InvalidBookCopyId", "Cannot find Book Copy with this id!");
    }
    public string Id { get; private set; } = id;

    public string Name { get; private set; } = name;

    public string Address { get; private set; } = address;

    public string Phone { get; private set; } = phone;

    public string OpeningHours { get; private set; } = openingHours;

    public Librarian Manager { get; private set; } = manager;

    private readonly List<BookCopy> _copies = [];

    private readonly List<Member> _members = [];
    public IReadOnlyList<Member> Members => _members;
    public IReadOnlyList<BookCopy> BookCopies => _copies;
    public IReadOnlyList<User> Users
    {
        get
        {
            List<User> users = [];
            users.Add(Manager);
            users.AddRange(Members);
            return users;
        }
    }
    public Member RegisterMember(string name, string email, string phone, DateOnly memberShipDate)
    {
        var member = new Member(name, phone, email, memberShipDate);
        _members.Add(member);
        return member;
    }
    public Member RegisterMember(string name, string phone)
    {
        var member = new Member(name, phone);
        _members.Add(member);
        return member;
    }
    public Result<Member> FindMemberById(string id)
    {
        foreach (var member in Members)
        {
            if (member.Id.Equals(id.NormalizeId(), StringComparison.OrdinalIgnoreCase)) return Result<Member>.Success(member);
        }
        return Result<Member>.Failure(Errors.InvalidMemberId);
    }
    public void AddBookCopy(BookCopy bookCopy) => _copies.Add(bookCopy);
    public Result<BookCopy> FindCopyByCopyId(string id)
    {
        foreach (var bookCopy in BookCopies)
        {
            if (bookCopy.Id.Equals(id.NormalizeId(), StringComparison.OrdinalIgnoreCase)) return Result<BookCopy>.Success(bookCopy);
        }
        return Result<BookCopy>.Failure(Errors.InvalidBookCopyId);
    }
    public List<BookCopy> GetAvailableBookCopies()
    {
        List<BookCopy> availableBookCopies = [];
        foreach (var bookCopy in BookCopies)
        {
            if (bookCopy.IsAvailable()) availableBookCopies.Add(bookCopy);
        }
        return availableBookCopies;
    }
    public string GetSummary() =>
        $"""
        ID                : {Id}
        Name              : {Name}
        Address           : {Address}
        Phone             : {Phone}
        Opening Hours     : {OpeningHours}
        Manager           : {Manager.Name}
        Total Members     : {_members.Count}
        Total Book Copies : {_copies.Count}
        """;
}
