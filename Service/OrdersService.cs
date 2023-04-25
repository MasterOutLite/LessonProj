using LessonProj.Modal;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace LessonProj.Service
{
    public class OrdersService
    {

#if WINDOWS
        public const string _path = "http://localhost:8080/orders";
#else
        public const string _path = "http://192.168.31.100:8080/orders";
#endif

        private HttpClient _httpClient;
        public List<Orders> Backup { get; private set; }
        public OrdersService ()
        {
            Backup = new();
            _httpClient = new();
        }

        public bool GetOrdersByUuid (string uuid, out Orders orders)
        {
            orders = Backup.Find(orders => orders.UUID == uuid);
            return orders != null;
        }

        public async Task<List<Orders>> GetOrdersListAsync ()
        {
            var response = await _httpClient.GetAsync($"{_path}/all");

            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get all books", $"Failed to get all books. Code: {exception.Code}. Msg: {exception.Msg}", "Ok");
            }
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                var content = await response.Content.ReadAsStringAsync();

                Backup = JsonConvert.DeserializeObject<List<Orders>>(content, settings);
            }

            return Backup;
        }

        public async Task PostOrdersAsync (Orders orders)
        {
            if (orders == null)
                return;

            var response = await _httpClient.PostAsJsonAsync($"{_path}/add", orders);
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
