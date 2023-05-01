using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;

namespace LessonProj.ViewModal
{
    public partial class LogInViewModal : ObservableObject
    {
        [ObservableProperty] private string _login = "koko";
        [ObservableProperty] private string _password = "kyrk";
        private LibrarianService _librarianService;

        public LogInViewModal (LibrarianService librarianService)
        {
            _librarianService = librarianService;
        }

        [RelayCommand]
        public async Task LogIn ()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            var auth = await _librarianService.CheckAuth(new RequestAuth(Login, Password));
            await Shell.Current.DisplayAlert("Auth", $"token: {auth.Token}|. IsSuccess: {auth.IsSuccess}. UUID: {auth.Uuid}", "Ok");
            if (auth.IsSuccess)
                await Shell.Current.GoToAsync("//BookView");

        }

        [RelayCommand]
        public async Task Skip ()
        {
            await Shell.Current.GoToAsync("//BookView");
        }
    }
}
