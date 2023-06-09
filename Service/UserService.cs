﻿using LessonProj.Modal;
using System.Net.Http.Json;

namespace LessonProj.Service
{
    public class UserService
    {
        private readonly string _path;

        private HttpClient _httpClient;
        public List<User> Backup { get; private set; }
        public bool IsBackup => Backup.Count > 0;

        public UserService (PropertyService propertyService)
        {
            _path = propertyService.URL + "/user";
            _httpClient = propertyService.HttpClient;
            Backup = new();
        }

        public bool GetUserByUuid (string uuid, out User user)
        {
            user = Backup.Find(user => user.Uuid == uuid);
            return user != null;
        }

        public async Task<User> GetUserByUuidAsync (string uuid)
        {
            User user = new User();
            if (GetUserByUuid(uuid, out user))
            {
                return user;
            }

            var response = await _httpClient.GetAsync($"{_path}/{uuid}");
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<User>();
                Backup.Add(user);
            }
            else
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Get User",
                    $"Failed to get User by uuid. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }

            return user;
        }
        public async Task<List<User>> GetUsersAsync ()
        {
            var response = await _httpClient.GetAsync($"{_path}/all");
            if (response.IsSuccessStatusCode)
            {
                Backup = await response.Content.ReadFromJsonAsync<List<User>>();
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

        public async Task PostUserAsync (UserLogIn user)
        {
            if (user == null)
                return;

            var response = await _httpClient.PostAsJsonAsync($"{_path}/add", user);
            if (!response.IsSuccessStatusCode)
            {
                var exception = await response.Content.ReadFromJsonAsync<ExceptionMsg>();
                await Shell.Current.DisplayAlert("Post User",
                    $"Failed post User. Code: {exception.Code}. Msg: {exception.Msg}",
                    "Ok");
            }
        }
    }
}
