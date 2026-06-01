using System.ComponentModel.DataAnnotations;
using LibraryAPI.Models;

namespace LibraryAPI.Endpoints;

public static class BooksEndpoints
{
    public static void MapBooksEndpoints(this WebApplication app, List<Book> books)
    {
        app.MapGet("/books", () =>
        {
            return Results.Ok(books);
        });

        app.MapGet("/books/{id}", (int id) =>
        {
            var book = books.Find(book => book.Id == id);
            if (book is null)
            {
                return Results.NotFound($"Book with ID {id} was not found");
            }
            return Results.Ok(book);
        });

        app.MapPost("/books", (Book book) =>
        {
            if (!IsValid(book, out var errors))
            {
                return Results.BadRequest(errors);
            }
            Book newBook = new Book
            {
                Id = books.Max(b => b.Id) + 1,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                CopiesAvailable = book.CopiesAvailable
            };
            books.Add(newBook);
            return Results.Created($"/books/{newBook.Id}", newBook);
        });

        app.MapPut("/books/{id}", (int id, Book updatedBook) =>
        {
            if (!IsValid(updatedBook, out var errors))
            {
                return Results.BadRequest(errors);
            }
            var bookIndex = books.FindIndex(book => book.Id == id);
            if (bookIndex == -1)
            {
                return Results.NotFound($"Book with id {id} not found");
            }
            books[bookIndex] = new Book
            {
                Id = id,
                Title = updatedBook.Title,
                Author = updatedBook.Author,
                PublishedYear = updatedBook.PublishedYear,
                CopiesAvailable = updatedBook.CopiesAvailable
            };
            return Results.NoContent();
        });

        app.MapDelete("/books/{id}", (int id) =>
        {
            var book = books.Find(book => book.Id == id);
            if (book is null)
            {
                return Results.NotFound($"Book with ID {id} was not found");
            }
            books.Remove(book);
            return Results.NoContent();
        });
    }

    private static bool IsValid(object obj, out List<string> errors)
    {
        var context = new ValidationContext(obj);
        var results = new List<ValidationResult>();
        errors = new List<string>();

        bool isValid = Validator.TryValidateObject(obj, context, results, true);

        if(!isValid)
        {
            errors = results.Select(r => r.ErrorMessage ?? "Invalid field").ToList();
        }

        return isValid;
    }
}