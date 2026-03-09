using LibrarySystem.Core.Entities;
using LibrarySystem.Core.Results;

namespace LibrarySystem.Core.Interfaces;

public interface IBorrowable
{
    bool IsAvailable();
    Result Borrow(Member member, int loanDays = 14);
    Result<decimal> Return();
}
