using Microsoft.AspNetCore.Mvc;
using PB.MeetUp.AITools.Models;
using PB.MeetUp.AITools.Mongo;

namespace PB.MeetUp.AITools.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : Controller
{
    
    private readonly IMongoProvider<Book, string> _bookProvider;

    public BookController(IMongoProvider<Book, string> bookProvider)
    {
        _bookProvider = bookProvider;
    }

    [HttpGet("GetTestBook")]
    public async Task<IActionResult> GetTestBook()
    {
        var techTask = new Book()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Tech Task 001",
            Description = "Example Tech Task",
        };
        
        return Ok(techTask);
    }
    
    // create method to get book by id with name "GetBook"
    [HttpGet("GetBook/{id}")]
    public async Task<IActionResult> GetBook(string id)
    {
        var book = await _bookProvider.FindSingle(record => record.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    // create async get endpoint "GetBooksByAuthor" to get books by author book property
    [HttpGet("GetBooksByAuthor")]
    public async Task<IActionResult> GetBooksByAuthor(string author)
    {
        var books = await _bookProvider.FindMany(record => record.Author == author);
        if (books == null)
            return NotFound();
        
        return Ok(books);
    }

    // create method to create new book
    [HttpPost("CreateBook")]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        await _bookProvider.Create(book);
        return Created("GetBook", book);
    }
}