using LibrarySystem.Core.Extensions;
using LibrarySystem.Core.Entities;
using LibrarySystem.Application.Helpers;
namespace LibrarySystem.Application.Services;

public class LibraryService(LibraryBranch branch, DisplayService display)
{
    private readonly LibraryBranch _branch = branch;
    private readonly DisplayService _display = display;

    public void HandleBorrow()
    {
        string memberId = ThemeHelper.Prompt("Member ID").NormalizeId();
        var resultMember = _branch.FindMemberById(memberId);
        if (resultMember.IsFailure)
        {
            _display.ShowBorrowFailure(resultMember.Error.Message);
            return;
        }
        _display.ShowAvailableCopies(_branch);
        string copyId = ThemeHelper.Prompt("Copy ID to borrow").NormalizeId();
        var resultCopy = _branch.FindCopyByCopyId(copyId);
        if (resultCopy.IsFailure) { 
            _display.ShowBorrowFailure(resultCopy.Error.Message);
            return;
        }
        resultCopy.Value!.Borrow(resultMember.Value!);
        _display.ShowBorrowSuccess(resultCopy.Value, resultMember.Value!);

    }

    public void HandleReturn()
    {
        string copyId = ThemeHelper.Prompt("Copy ID to return").NormalizeId();
        var resultCopy = _branch.FindCopyByCopyId(copyId);
        if (resultCopy.IsFailure) { 
            _display.ShowReturnFailure(resultCopy.Error.Message);
            return;
        }
        var resultFine = resultCopy.Value!.Return();
        if (resultFine.IsFailure)
        {
            _display.ShowReturnFailure(resultFine.Error.Message);
            return;
        }
        _display.ShowReturnSuccess(resultCopy.Value, resultFine.Value);
    }

    public void HandleHistory()
    {
        string memberId = ThemeHelper.Prompt("Member ID").NormalizeId();
        var resultMember = _branch.FindMemberById(memberId);
        if (resultMember.IsFailure)
        {
            _display.ShowMemberHistoryFailure(resultMember.Error.Message);
            return;
        }
        _display.ShowMemberHistory(resultMember.Value!);
    }

    public void HandleRegisterMember()
    {
        string name = ThemeHelper.Prompt("Full Name");
        string phone = ThemeHelper.Prompt("Phone Number");
        if (!phone.ContainDigit())
        {
            _display.ShowRegistrationFailure("Phone number must contain at least one digit.");
            return;
        }
        string email = ThemeHelper.Prompt("Email Address");
        if (!email.IsValidEmail())
        {
            _display.ShowRegistrationFailure("Invalid email format. Must contain '@' and domain.");
            return;
        }
        Member member = _branch.RegisterMember(name, phone);
        _display.ShowRegistrationSuccess(member);
    }
}