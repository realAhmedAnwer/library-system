using LibrarySystem.Core.Entities;
using LibrarySystem.Core.Enums;

namespace LibrarySystem.Application.Data;

public static class DataSeeder
{
    public static LibraryBranch Seed()
    {
        Librarian manager = new Librarian("Sara Ahmed", "01012345678", 8500m, new DateOnly(2019, 3, 15));
        
        LibraryBranch branch = new("BR-01", "City Library – Nasr City Branch", "15 Nasr Road, Nasr City, Cairo", "01012345678", "Sat–Thu: 09:00 AM – 09:00 PM", manager);

        branch.RegisterMember("Ahmed Kamal", "ahmed@email.com", "01098765432", new DateOnly(2023, 1, 20));
        branch.RegisterMember("Nour Hassan", "nour@email.com", "01155556677", new DateOnly(2024, 3, 5));
        branch.RegisterMember("Omar Samir", "01234567890");

        Book b1 = new("978-0-13-468599-1", "Clean Code", "Robert C. Martin", "Software Engineering", 2008);
        Book b2 = new("978-0-13-235088-4", "The Pragmatic Programmer", "David Thomas", "Software Engineering", 1999);
        Book b3 = new("978-0-06-112008-4", "To Kill a Mockingbird");

        BookCopy c1 = new(b1, BookCopyCondition.Good, BookCopyStatus.Available);
        BookCopy c2 = new(b1, BookCopyCondition.Fair, BookCopyStatus.Available);
        BookCopy c3 = new(b2, BookCopyCondition.Excellent, BookCopyStatus.Available);
        BookCopy c4 = new(b3, BookCopyCondition.Good, BookCopyStatus.Available);

        branch.AddBookCopy(c1);
        branch.AddBookCopy(c2);
        branch.AddBookCopy(c3);
        branch.AddBookCopy(c4);

        return branch;
    }
}
