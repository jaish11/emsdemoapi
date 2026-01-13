
using Dapper;
using emsdemoapi.Data.Entities; 
using emsdemoapi.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System.Data;

namespace emsdemoapi.Data.Services
{//this is implementation of in-memory caching
    //public class BookService : IBook
    //{

    //    private readonly IConfiguration _configuration;
    //    private readonly string _connectionString;
    //    private readonly ILogger<BookService> _logger;
    //    private readonly IMemoryCache _cache;

    //    private const string AllBooksKey = "books:all";
    //    private string GetBookKey(int id) => $"books:{id}";
    //    public BookService(IConfiguration configuration, ILogger<BookService> logger, IMemoryCache cache)
    //    {
    //        _configuration = configuration;
    //        _connectionString = _configuration.GetConnectionString("SqlCon");
    //        _logger = logger;
    //        _cache = cache;
    //    }

    //    public bool AddBook(Book book)
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
    //            _logger.LogInformation("Adding a new book to the database {Book}", book);
    //            var parameter = new DynamicParameters();
    //            parameter.Add("@Name", book.Name);

    //            connection.Execute("sp_AddBooks", parameter, commandType: CommandType.StoredProcedure);
    //            _cache.Remove(AllBooksKey);
    //            return true;
    //        }
    //    }

    //    public bool DeleteBook(int id)
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
    //            _logger.LogInformation("Deleting book with id {Id} from the database", id);
    //            var parameter = new DynamicParameters();
    //            parameter.Add("@Id", id);

    //            connection.Execute("sp_DeleteBookByID", parameter, commandType: CommandType.StoredProcedure);
    //        }
    //        _cache.Remove(AllBooksKey);
    //        _cache.Remove(GetBookKey(id));
    //        return true;
    //    }

    //    public List<Book> GetAllBooks()
    //    {
    //        if (_cache.TryGetValue(AllBooksKey, out List<Book> cachedBooks))
    //        {
    //            _logger.LogInformation("Retrieved all books from cache");
    //            return cachedBooks;
    //        }
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
    //            _logger.LogInformation("Retrieving all books from the database");
    //            var book = connection.Query<Book>("GetAllBooks", commandType: CommandType.StoredProcedure).ToList();
    //            return book;
    //        }
    //    }

    //    public Book GetBookById(int id)
    //    {
    //        var key = GetBookKey(id);
    //        if (_cache.TryGetValue(key, out Book cachedBook))
    //        {
    //            _logger.LogInformation("Retrieved book with id {Id} from cache", id);
    //            return cachedBook;
    //        }
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
    //            _logger.LogInformation("Retrieving book with id {Id} from the database", id);
    //            var parameter = new DynamicParameters();
    //            parameter.Add("@Id", id);

    //            var book = connection.QueryFirstOrDefault<Book>("sp_GetBooksById", parameter, commandType: CommandType.StoredProcedure);
    //            return book;
    //        }
    //    }

    //    public bool UpdateBook(Book book)
    //    {
    //        using (var connection = new SqlConnection(_connectionString))
    //        {
    //            _logger.LogInformation("Updating book with id {Book} in the database", book);
    //            var parameter = new DynamicParameters();
    //            parameter.Add("@Id", book.Id);
    //            parameter.Add("@Name", book.Name);

    //            connection.Execute("sp_UpdateBooks", parameter, commandType: CommandType.StoredProcedure);
    //        }
    //        _cache.Remove(AllBooksKey);
    //        _cache.Remove(GetBookKey(book.Id));
    //        return true;
    //    }
    //}
    public class BookService : IBook
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ILogger<BookService> _logger;

        public BookService(IConfiguration configuration, ILogger<BookService> logger)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlCon");
            _logger = logger;
        }

        public bool AddBook(Book book)
        {

            using var connection = new SqlConnection(_connectionString);
            _logger.LogInformation("Adding a new book to the database {Book}", book);
                var parameter = new DynamicParameters();
            parameter.Add("@Name", book.Name);

            connection.Execute("sp_AddBooks", parameter, commandType: CommandType.StoredProcedure);
            return true;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    _logger.LogInformation("Adding a new book to the database {Book}", book);
            //    var parameter = new DynamicParameters();
            //    parameter.Add("@Name", book.Name);

            //    connection.Execute("sp_AddBooks", parameter, commandType: CommandType.StoredProcedure);
            //    return true;
            //}
        }

        public bool DeleteBook(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            _logger.LogInformation("Deleting book with id {Id} from the database", id);
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);

            connection.Execute("sp_DeleteBookByID", parameter, commandType: CommandType.StoredProcedure);
            return true;
            /*using (var connection = new SqlConnection(_connectionString))
            {
                _logger.LogInformation("Deleting book with id {Id} from the database", id);
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id);

                connection.Execute("sp_DeleteBookByID", parameter, commandType: CommandType.StoredProcedure);
                return true;
            }*/
        }

        public List<Book> GetAllBooks()
        {
            using var connection = new SqlConnection(_connectionString);
            _logger.LogInformation("Retrieving all books from the database");
            var book = connection.Query<Book>("GetAllBooks", commandType: CommandType.StoredProcedure).ToList();
            return book;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    _logger.LogInformation("Retrieving all books from the database");
            //    var book = connection.Query<Book>("GetAllBooks", commandType: CommandType.StoredProcedure).ToList();
            //    return book;
            //}
        }

        public Book GetBookById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            _logger.LogInformation("Retrieving book with id {Id} from the database", id);
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);

            var book = connection.QueryFirstOrDefault<Book>("sp_GetBooksById", parameter, commandType: CommandType.StoredProcedure);
            return book;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    _logger.LogInformation("Retrieving book with id {Id} from the database", id);
            //    var parameter = new DynamicParameters();
            //    parameter.Add("@Id", id);

            //    var book = connection.QueryFirstOrDefault<Book>("sp_GetBooksById", parameter, commandType: CommandType.StoredProcedure);
            //    return book;
            //}
        }

        public bool UpdateBook(Book book)
        {
            using var connection = new SqlConnection(_connectionString);
            _logger.LogInformation("Updating book with id {Book} in the database", book);
            var parameter = new DynamicParameters();
            parameter.Add("@Id", book.Id);
            parameter.Add("@Name", book.Name);

            connection.Execute("sp_UpdateBooks", parameter, commandType: CommandType.StoredProcedure);
            return true;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    _logger.LogInformation("Updating book with id {Book} in the database", book);
            //    var parameter = new DynamicParameters();
            //    parameter.Add("@Id", book.Id);
            //    parameter.Add("@Name", book.Name);

            //    connection.Execute("sp_UpdateBooks", parameter, commandType: CommandType.StoredProcedure);
            //    return true;
            //}
        }
    }

}
