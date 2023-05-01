using LessonProj.Modal;
using System.Net.Http.Json;

namespace LessonProj.Service
{
    public class BookService
    {

#if WINDOWS
        private const string _path = "http://localhost:8080/book";
#else 
        private const string _path = "http://192.168.31.100:8080/book";
#endif

        private HttpClient _httpClient;
        public List<Book> Backup { get; private set; }
        public bool IsBackup => Backup.Count > 0;
        public BookService ()
        {
            Backup = new List<Book>();
            _httpClient = new HttpClient();
        }

        public bool GetBookByUuid (string uuid, out Book book)
        {
            book = Backup.Find(book => book.Uuid == uuid);
            return book != null;
        }

        public async Task<Book> GetBookByUuidAsync (string uuid)
        {
            Book book = new Book();
            if (GetBookByUuid(uuid, out book))
            {
                return book;
            }

            var response = await _httpClient.GetAsync($"{_path}/{uuid}");
            if (response.IsSuccessStatusCode)
            {
                book = await response.Content.ReadFromJsonAsync<Book>();
                Backup.Add(book);
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get books",
                    $"Failed to get books by uuid. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }

            return book;
        }

        public async Task<List<Book>> GetBooksAsync ()
        {
            var response = await _httpClient.GetAsync($"{_path}/all");
            if (response.IsSuccessStatusCode)
            {
                Backup = await response.Content.ReadFromJsonAsync<List<Book>>();
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get all books",
                    $"Failed to get all books. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }

            return Backup;
        }

        public async Task<List<Book>> GetBooksByPageAsync (int page, int amount = 10)
        {
            List<Book> books = new List<Book>();
            var response = await _httpClient.GetAsync($"{_path}/all?page={page}&&amount={amount}");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    books = await response.Content.ReadFromJsonAsync<List<Book>>();
                    Backup.AddRange(books);
                }
                catch
                {
                }
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get all books",
                    $"Failed to get all books. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }

            return books;
        }

        public async void PostBook (Book book)
        {
            if (book == null)
                return;

            var response = await _httpClient.PostAsJsonAsync<Book>($"{_path}/add", book);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Post Book",
                    $"Failed post Book. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }
        }
    }
}

