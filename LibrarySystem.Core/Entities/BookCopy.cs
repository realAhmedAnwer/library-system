using LibrarySystem.Core.Enums;
using LibrarySystem.Core.Interfaces;
using LibrarySystem.Core.Results;

namespace LibrarySystem.Core.Entities;

public class BookCopy(Book book, BookCopyCondition condition, BookCopyStatus status) : IBorrowable, ISummarizable
{
    public static class Errors
    {
        public static readonly Error NotAvailable = new("BookCopy.NotAvailable", "This Book Copy is not Available!");
        public static readonly Error NotBorrowed = new("BookCopy.NotBorrowed", "This Book Copy is not currently Borrowed!");
    }
    public string Id { get; init; } = $"COPY-{++Book.CopiesCounter:D3}";
    public Book Book { get; set; } = book;
    public BookCopyCondition Condition { get; set; } = condition;
    public BookCopyStatus Status { get; set; } = status;
    public BorrowTransaction? ActiveBorrowTransaction { get; set; }
    public bool IsAvailable() => Status == BookCopyStatus.Available;
    public Result Borrow(Member member, int loanDays = 14)
    {
        if (!IsAvailable()) return Result.Failure(Errors.NotAvailable);
        Status = BookCopyStatus.Borrowed;
        ActiveBorrowTransaction = new BorrowTransaction(member, this, loanDays);
        member.AddBorrowTransaction(ActiveBorrowTransaction);
        return Result.Success();
    }
    public Result<decimal> Return()
    {
        if (Status is not BookCopyStatus.Borrowed) return Result<decimal>.Failure(Errors.NotBorrowed);
        ActiveBorrowTransaction!.MarkReturned(DateOnly.FromDateTime(DateTime.Today));
        decimal fine = ActiveBorrowTransaction.CalculateFine();
        Status = BookCopyStatus.Available;
        ActiveBorrowTransaction = null;
        return Result<decimal>.Success(fine);
    }
    public string GetSummary() => $"Copy [{Id}] - {Book.Title} | Condition: {Condition} | {Status}";

}
