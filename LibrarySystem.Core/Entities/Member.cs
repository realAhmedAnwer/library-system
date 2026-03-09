using System.Text;

namespace LibrarySystem.Core.Entities;

public class Member(string name, string phone, string email, DateOnly joinDate) : User($"MEM-{++_counter:D3}", name, phone)
{
    private static int _counter = 0;
    private readonly List<BorrowTransaction> _borrowTransactions = [];
    public string Email { get; private set; } = email;
    public DateOnly JoinDate { get; private set; } = joinDate;
    public IReadOnlyList<BorrowTransaction> BorrowTransactions => _borrowTransactions;
    public Member(string name, string phone) : this(name, phone, "N/A", DateOnly.FromDateTime(DateTime.Now)) { }
    public void AddBorrowTransaction(BorrowTransaction transaction) => _borrowTransactions.Add(transaction);
    public string GetTransactionsHistory()
    {
        if (BorrowTransactions.Count == 0) return "No Transactions Found!";
        var transactionHistory = new StringBuilder();
        foreach (var transaction in BorrowTransactions)
        {
            transactionHistory.Append(transaction.GetSummary());
        }
        return transactionHistory.ToString();
    }
    public override string GetSummary() =>
        $"""
        ID      : {Id}
        Name    : {Name}
        Joined  : {JoinDate:dd/MM/yyyy}
        Phone   : {Phone}
        Email   : {Email ?? "N/A"}
        Borrows : {BorrowTransactions.Count}
        """;
}
