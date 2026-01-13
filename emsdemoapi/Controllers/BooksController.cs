using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace emsdemoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [IgnoreAntiforgeryToken]
    public class BooksController : ControllerBase
    {
        private readonly IBook _book;

        public BooksController(IBook book)
        {
            _book = book;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_book.GetAllBooks());
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            return Ok(_book.GetBookById(id));
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            _book.AddBook(book);
            return Ok("Book Added Successfully!");
        }

        [HttpPut]
        public IActionResult UpdateBook([FromBody] Book book)
        {
            _book.UpdateBook(book);
            return Ok("Book Updated Successfully!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _book.DeleteBook(id);
            return Ok("Book Deleted Successfully!");
        }
    }
}



// private readonly IGeneric<Book> _book;
// public readonly ILogger<BooksController> _logger;
// public BooksController(IGeneric<Book> book, ILogger<BooksController> logger)
// {
//     _book = book;
//     _logger = logger;
// }
// [HttpGet]
// public async Task<IActionResult> Get() {
//     _logger.LogInformation("Get request Getting all books");
//     return Ok(await _book.GetAllAsync());
// }
// [HttpGet("{id}")]
// public async Task<IActionResult> GetById(int id) { 
//     _logger.LogInformation($"GetById request Getting book with id: {id}");
//     return Ok(await _book.GetByIdAsync(id));
// }
// [HttpPost]
// public async Task<IActionResult> Add(Book book)
// {
//     _logger.LogInformation("Post request Adding a new book");
//     await _book.AddAsync(book);
//     await _book.SaveAsync();
//     return Ok("Added!");
// }

// [HttpPut]
// public async Task<IActionResult> Update(Book book)
// {   
//     _logger.LogInformation($"Put request Updating book with id: {book.Id}");
//     await _book.UpdateAsync(book);  
//     await _book.SaveAsync();
//     return Ok("Updated!");

// }
// [HttpDelete("{id}")]
//public async Task<IActionResult> Delete(int id)
// {
//     _logger.LogInformation($"Delete request Deleting book with id: {id}");
//     await _book.DeleteAsync(id);
//     await _book.SaveAsync();
//     return Ok("Deleted");
// }
/*
 * 1.this is needed Controller usage (useful for simple GETs):
 * [ResponseCache(Duration = 120,
 Location = ResponseCacheLocation.Any)]*/

/*        
 *        this is for .NET 8 and above only in Program.cs add Output Caching
 *        will not show in responce headers but it will work you can check in logger
 *        [OutputCache(Duration = 120, VaryByQueryKeys = new[] {"*"})]
*/

/*        //Client & Proxy caching:-it doesn’t guarantee server caches anything.Use it to instruct clients/proxies:
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)] // instruct client to cache for 60s
        */