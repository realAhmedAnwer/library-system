namespace LibrarySystem.Core.Entities;

public class Librarian(string name, string phone, decimal salary, DateOnly hireDate) : User($"LIB{++_counter:D2}", name, phone)
{
    private static int _counter = 0;
    public decimal Salary { get; private set; } = salary;
    public DateOnly HireDate { get; private set; } = hireDate;

    public override string GetSummary() => 
        $"""
        ID     : {Id}
        Name   : {Name}
        Phone  : {Phone}
        Salary : {Salary:C}
        Hired  : {HireDate:dd/MM/yyyy}
        """;
}
