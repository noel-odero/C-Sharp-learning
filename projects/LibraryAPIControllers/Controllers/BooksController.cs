using LibraryAPIControllers.Data;
using LibraryAPIControllers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPIControllers.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }


    // ActionResult<Book>   "I return HTTP responses, and when successful the body is a Book"
    // IActionResult        "I return HTTP responses, body shape unspecified"
    // GET api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _context.BooksTable.AsNoTracking().ToListAsync();
        return Ok(books);
    }

    // GET api/books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.BooksTable.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
            return NotFound();

        return Ok(book);
    }

    // POST api/books
    [HttpPost]
    public async Task<ActionResult<Book>> AddBook(Book book)
    {
        _context.BooksTable.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT api/books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.BooksTable.FindAsync(id);

        if (book is null)
            return NotFound();

        _context.BooksTable.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}