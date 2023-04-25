using LessonProj.Modal;

using System.Net.Http.Json;

namespace LessonProj.Service
{
    public class LibraryService
    {

#if WINDOWS
        private const string _path = "http://localhost:8080/library";
#else
        private const string _path = "http://192.168.31.100:8080/library";
#endif

        private HttpClient _httpClient;
        public List<Library> Backup { get; private set; }
        public bool IsBackup => Backup != null && Backup.Count > 0;
        public LibraryService ()
        {
            _httpClient = new();
            Backup = new();           
        }

        public bool GetLibraryByUuid (string uuid, out Library library)
        {
            library = Backup.Find(library => library.UUID == uuid);
            return library != null;
        }

        public async Task<Library> GetLibraryByUuidAsync (string uuid)
        {
            Library library = new();
            if (GetLibraryByUuid(uuid, out library))
            {
                return library;
            }

            var response = await _httpClient.GetAsync($"{_path}/{uuid}");
            if(response.IsSuccessStatusCode)
            {
                library = await response.Content.ReadFromJsonAsync<Library>();
                Backup.Add(library);
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get Library by uuid",
                    $"Failed to get Library by uuid. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }

            return library;
        }
        public async Task<List<Library>> GetLibraryAsync ()
        {
            var response = await _httpClient.GetAsync($"{_path}/all");
            if (response.IsSuccessStatusCode)
            {
                Backup = await response.Content.ReadFromJsonAsync<List<Library>>();
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Post Library",
                    $"Failed to get list Library. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }
            return Backup;
        }

        public async Task PostLibraryAsync (Library library)
        {
            if (library == null)
                return;

            var response = await _httpClient.PostAsJsonAsync($"{_path}/add", library);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Post Library",
                    $"Failed post Library. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }
        }
    }
}
