using LibrarySystem.Core.Interfaces;
using System.Transactions;
namespace LibrarySystem.Core.Entities;

public class BorrowTransaction(Member member, BookCopy bookCopy, int loanDays) : ISummarizable
{
    private static int _counter = 1000;
    private const decimal FinePerDay = 10m;
    public string Id { get; private set; } = $"#{++_counter}";
    public Member Member { get; set; } = member;
    public BookCopy BookCopy { get; set; } = bookCopy;
    public DateOnly BorrowDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(loanDays));
    public DateOnly? ReturnDate { get; set; } = null;
    public bool IsReturned() => ReturnDate.HasValue;
    public void MarkReturned(DateOnly returnDate) => ReturnDate = returnDate;
    public decimal CalculateFine()
    {
        int overDueDays = (ReturnDate ?? DateOnly.FromDateTime(DateTime.Today)).DayNumber - DueDate.DayNumber;
        return overDueDays > 0 ? overDueDays * FinePerDay : 0;
    }
    public string GetSummary() =>   
        $"""
        ── Transaction {Id} ──────────────
        Book     : {BookCopy.Book.Title}
        Copy ID  : {BookCopy.Id}
        Borrowed : {BorrowDate:dd/MM/yyyy}
        Due      : {DueDate:dd/MM/yyyy}
        Returned : {ReturnDate?.ToString("dd/MM/yyyy") ?? "Not returned yet!"}
        Status   : {(IsReturned() ? "Returned" : "Active")}
        Fine     : {(CalculateFine())}
        """;
    
}
