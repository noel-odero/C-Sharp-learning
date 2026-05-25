using LibraryAPI.Endpoints;
using LibraryAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var books = new List<Book>
{
    new Book { Id = 1, Title = "Things Fall Apart", Author = "Chinua Achebe", PublishedYear = 1958, CopiesAvailable = 3 },
    new Book { Id = 2, Title = "Half of a Yellow Sun", Author = "Chimamanda Ngozi Adichie", PublishedYear = 2006, CopiesAvailable = 5 },
    new Book { Id = 3, Title = "Weep Not, Child", Author = "Ngũgĩ wa Thiong'o", PublishedYear = 1964, CopiesAvailable = 2 }
};

app.MapBooksEndpoints(books);

app.Run();