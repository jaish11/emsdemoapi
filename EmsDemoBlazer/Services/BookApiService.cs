using EmsDemoBlazer.Models;

namespace EmsDemoBlazer.Services
{
    public class BookApiService
    {
        private readonly HttpClient _http;
        public BookApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _http.GetFromJsonAsync<List<Book>>("api/Books");
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _http.GetFromJsonAsync<Book>($"api/Books/{id}");
        }

        public async Task AddBook(Book book)
        {
            await _http.PostAsJsonAsync("api/Books", book);
        }

        public async Task UpdateBook(Book book)
        {
            await _http.PutAsJsonAsync("api/Books", book);
        }
        public async Task DeleteBook(int id)
        {
            await _http.DeleteAsync($"api/Books/{id}");
        }
    }
}
