namespace BankAccountsApp.DTOs;

public record class Record(
    int id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
