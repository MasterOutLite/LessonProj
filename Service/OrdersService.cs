using LessonProj.Modal;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http.Json;

namespace LessonProj.Service
{
    public class OrdersService
    {
        private readonly string _path;

        private HttpClient _httpClient;
        public List<Orders> Backup { get; private set; }
        public bool IsBackup => Backup.Count > 0;
        public OrdersService (PropertyService propertyService)
        {
            _path = propertyService.URL + "/orders";
            _httpClient = propertyService.HttpClient;
            Backup = new();
        }

        public bool GetOrdersByUuid (string uuid, out Orders orders)
        {
            orders = Backup.Find(orders => orders.Uuid == uuid);
            return orders != null;
        }

        public async Task<List<Orders>> GetOrdersListAsync ()
        {
            List<Orders> responseOrders = new();
            var response = await _httpClient.GetAsync($"{_path}/all");

            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get all Orders", $"Failed to get all Orders. Code: {exception.Code}. Msg: {exception.Msg}", "Ok");
            }
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                var content = await response.Content.ReadAsStringAsync();

                 responseOrders = JsonConvert.DeserializeObject<List<Orders>>(content, settings);
            }

            Backup.AddRange(responseOrders);
            return responseOrders;
        }

        public async Task PostOrdersAsync (PostOrders orders)
        {
            if (orders is null)
                return;

            var response = await _httpClient.PostAsJsonAsync($"{_path}/add", orders);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Post Orders",
                    $"Failed post Orders. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
                Debug.WriteLine(exception.Msg);
            }
        }

        public async void PutOrdersAsync (Orders orders)
        {
            if (orders is null)
                return;

            var response = await _httpClient.PutAsJsonAsync(_path, orders);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Post Orders",
                    $"Failed put Orders. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
                Debug.WriteLine(exception.Msg);
            }
        }
    }
}
