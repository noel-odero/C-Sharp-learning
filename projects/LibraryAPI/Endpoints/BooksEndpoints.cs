using System.ComponentModel.DataAnnotations;
using LibraryAPI.Models;
using LibraryAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Endpoints;

public static class BooksEndpoints
{
    public static void MapBooksEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (LibraryContext context) =>
        {
            var books = context.Books.ToList();
            return Results.Ok(books);
        });

        app.MapGet("/books/{id}", (int id, LibraryContext context) =>
        {
            var book = context.Books.Find(id);
            if (book is null)
            {
                return Results.NotFound($"Book with ID {id} was not found");
            }
            return Results.Ok(book);
        });

        app.MapPost("/books", (Book book, LibraryContext context) =>
        {
            if (!IsValid(book, out var errors))
            {
                return Results.BadRequest(errors);
            }
            context.Books.Add(book);
            context.SaveChanges();
            return Results.Created($"/books/{book.Id}", book);
        });

        app.MapPut("/books/{id}", (int id, Book updatedBook, LibraryContext context) =>
        {
            if (!IsValid(updatedBook, out var errors))
            {
                return Results.BadRequest(errors);
            }
            var book = context.Books.Find(id); 
            if (book is null)
            {
                return Results.NotFound($"Book with id {id} not found");
            }
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishedYear = updatedBook.PublishedYear;
            book.CopiesAvailable = updatedBook.CopiesAvailable;
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

        if(!isValid)
        {
            errors = results.Select(r => r.ErrorMessage ?? "Invalid field").ToList();
        }

        return isValid;
    }
}