using System.ComponentModel.DataAnnotations;
using LibraryAPI.Data;
using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Endpoints;

public static class BooksEndpoints
{
    public static void MapBooksEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (LibraryContext context) =>
        {
            var books = context.Books
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    PublishedYear = b.PublishedYear,
                    CopiesAvailable = b.CopiesAvailable
                })
                .ToList();

            return Results.Ok(books);
        });

        app.MapGet("/books/{id}", (int id, LibraryContext context) =>
        {
            var book = context.Books.Find(id);
            if (book is null)
            {
                return Results.NotFound($"Book with ID {id} was not found");
            }

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                CopiesAvailable = book.CopiesAvailable
            };

            return Results.Ok(bookDto);
        });

        app.MapPost("/books", (CreateBookDto dto, LibraryContext context) =>
        {
            if (!IsValid(dto, out var errors))
            {
                return Results.BadRequest(errors);
            }

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                PublishedYear = dto.PublishedYear,
                CopiesAvailable = dto.CopiesAvailable
            };

            context.Books.Add(book);
            context.SaveChanges();

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                CopiesAvailable = book.CopiesAvailable
            };

            return Results.Created($"/books/{book.Id}", bookDto);
        });

        app.MapPut("/books/{id}", (int id, UpdateBookDto dto, LibraryContext context) =>
        {
            if (!IsValid(dto, out var errors))
            {
                return Results.BadRequest(errors);
            }

            var book = context.Books.Find(id);
            if (book is null)
            {
                return Results.NotFound($"Book with id {id} not found");
            }

            // Only update fields that were sent
            if (dto.Title is not null) book.Title = dto.Title;
            if (dto.Author is not null) book.Author = dto.Author;
            if (dto.PublishedYear is not null) book.PublishedYear = dto.PublishedYear.Value;
            if (dto.CopiesAvailable is not null) book.CopiesAvailable = dto.CopiesAvailable.Value;

            context.SaveChanges();
            return Results.NoContent();
        });

        app.MapDelete("/books/{id}", (int id, LibraryContext context) =>
        {
            var book = context.Books.Find(id);
            if (book is null)
            {
                return Results.NotFound($"Book with ID {id} was not found");
            }
            context.Books.Remove(book);
            context.SaveChanges();
            return Results.NoContent();
        });
    }

    private static bool IsValid(object obj, out List<string> errors)
    {
        var context = new ValidationContext(obj);
        var results = new List<ValidationResult>();
        errors = new List<string>();

        bool isValid = Validator.TryValidateObject(obj, context, results, true);

        if (!isValid)
        {
            errors = results.Select(r => r.ErrorMessage ?? "Invalid field").ToList();
        }

        return isValid;
    }
}