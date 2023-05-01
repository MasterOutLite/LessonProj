using LessonProj.Modal;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;

namespace LessonProj.Service
{
    public class LibrarianService
    {

#if WINDOWS
        private const string _path = "http://localhost:8080/librarian";
#else
        private const string _path = "http://192.168.31.100:8080/librarian";
#endif

        private HttpClient _httpClient;
        public List<Librarian> Backup { get; private set; }

        public bool IsBackup => Backup.Count > 0;
        public LibrarianService ()
        {
            _httpClient = new();
            Backup = new();
        }

        public bool GetLibrarianByUuid (string uuid, out Librarian librarian)
        {
            librarian = Backup.Find(librarian => librarian.Uuid == uuid);
            return librarian != null;
        }

        public async Task<ResponseAuth> CheckAuth (RequestAuth auth)
        {
            ResponseAuth responseAuth = new ResponseAuth();

            string requestJson = JsonConvert.SerializeObject(auth);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_path}/login"),
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json"),
            };
            var response = await _httpClient.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                responseAuth = await response.Content.ReadFromJsonAsync<ResponseAuth>();
            }

            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("LogIn fail",
                    $"Failed LogIn. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }

            return responseAuth;
        }

        public async Task<Librarian> GetLibrarianByUuidAsync (string uuid)
        {

            Librarian librarian = new();
            if (string.IsNullOrWhiteSpace(uuid))
                return librarian;

            if (GetLibrarianByUuid(uuid, out librarian))
            {
                return librarian;
            }

            var response = await _httpClient.GetAsync($"{_path}/{uuid}");
            if (response.IsSuccessStatusCode)
            {
                librarian = await response.Content.ReadFromJsonAsync<Librarian>();
                Backup.Add(librarian);
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get Library by uuid",
                    $"Failed to get Library by uuid. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }

            return librarian;
        }

        public async Task<List<Librarian>> GetLibrariansAsync ()
        {
            var response = await _httpClient.GetAsync($"{_path}/all");
            if (response.IsSuccessStatusCode)
            {
                Backup = await response.Content.ReadFromJsonAsync<List<Librarian>>();
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get all books", $"Failed to get all books. Code: {exception.Code}. Msg: {exception.Msg}", "Ok");
            }

            return Backup;
        }

        public async Task PostLibrarianAsync (Librarian librarian)
        {
            if (librarian == null)
                return;

            var response = await _httpClient.PostAsJsonAsync($"{_path}/add", librarian);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Post Librarian",
                    $"Failed post Librarian. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }
        }

    }
}
