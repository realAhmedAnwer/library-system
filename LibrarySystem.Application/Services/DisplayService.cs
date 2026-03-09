using LibrarySystem.Core.Entities;
using LibrarySystem.Application.Helpers;
namespace LibrarySystem.Application.Services;

public class DisplayService
{
    public void ShowBranchInfo(LibraryBranch branch)
    {
        ThemeHelper.PrintHeader("LIBRARY BRANCH INFO");
        Console.WriteLine(branch.GetSummary());
    }

    public void ShowAllUsers(LibraryBranch branch)
    {
        ThemeHelper.PrintHeader("ALL REGISTERED USERS");

        IReadOnlyList<User> users = branch.Users;

        for (int i = 0; i < users.Count; i++)
        {
            string header =
                users[i] is Librarian
                ? "LIBRARIAN PROFILE"
                : "MEMBER PROFILE";

            ThemeHelper.PrintSectionTitle(header);
            Console.WriteLine(users[i].GetSummary());
        }
    }

    public void ShowAvailableCopies(LibraryBranch branch)
    {
        ThemeHelper.PrintHeader("AVAILABLE BOOK COPIES");

        List<BookCopy> available = branch.GetAvailableBookCopies();

        if (available.Count == 0)
        {
            ThemeHelper.PrintWarning("No available book copies found.");
            return;
        }

        for (int i = 0; i < available.Count; i++)
            Console.WriteLine(available[i].GetSummary());
    }

    public void ShowAllCopies(LibraryBranch branch)
    {
        ThemeHelper.PrintHeader("ALL BOOK COPIES");

        if (branch.BookCopies.Count == 0)
        {
            ThemeHelper.PrintWarning("No book copies found.");
            return;
        }

        for (int i = 0; i < branch.BookCopies.Count; i++)
            Console.WriteLine(branch.BookCopies[i].GetSummary());
    }

    public void ShowMemberHistory(Member member)
    {
        ThemeHelper.PrintSectionTitle($"Borrowing History for {member.Name}:");
        Console.WriteLine(member.GetTransactionsHistory());
    }
    public void ShowMemberHistoryFailure(string reason)
    {
        ThemeHelper.PrintError($"Failed to retrieve member history: {reason}");
    }
    public void ShowBorrowSuccess(BookCopy copy, Member member)
    {
        ThemeHelper.PrintSuccess(
            $"Copy [{copy.Id}] \"{copy.Book.Title}\" borrowed by {member.Name}."
        );

        ThemeHelper.PrintSuccess(
            $"Due date: {copy.ActiveBorrowTransaction!.DueDate:dd/MM/yyyy}"
        );
    }
    public void ShowBorrowFailure(string reason)
    {
        ThemeHelper.PrintError($"Failed to borrow book copy: {reason}");
    }
    public void ShowReturnSuccess(BookCopy copy, decimal fine)
    {
        ThemeHelper.PrintSuccess(
            $"Copy [{copy.Id}]: {copy.Book.Title} returned."
        );

        if (fine > 0)
            ThemeHelper.PrintWarning($"Late return fine: {fine:F2} EGP");
        else
            ThemeHelper.PrintSuccess("Returned on time. No fine.");
    }
    public void ShowReturnFailure(string reason)
    {
        ThemeHelper.PrintError($"Failed to return book copy: {reason}");
    }
    public void ShowRegistrationSuccess(Member member)
    {
        ThemeHelper.PrintSuccess(
            $"Member: {member.Name} - [{member.Id}] registered successfully."
        );
    }
    public void ShowRegistrationFailure(string reason)
    {
        ThemeHelper.PrintError($"Failed to register member: {reason}");
    }
    public void ShowAddCopySuccess(BookCopy copy)
    {
        ThemeHelper.PrintSuccess(
            $"Copy [{copy.Id}] - {copy.Book.Title}: added."
        );
    }
}
