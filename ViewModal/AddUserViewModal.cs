using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;
using MvvmHelpers.Commands;

namespace LessonProj.ViewModal
{
    public partial class AddUserViewModal : ObservableObject
    {
        public HeaderViewModal HeaderViewModal { get; }
        public UserLogIn User { get; } = new UserLogIn();
        public IRelayCommand Add { get; }

        private UserService _userService;

        private const int MIN_LENGHT_PASSWORD = 6;
        private const int MIN_LENGHT_LOGIN = 5;
        private const int MIN_LENGHT_PHONE = 10;
        public AddUserViewModal (UserService userService)
        {
            _userService = userService;
            var add = new ShowButton("Додати", new AsyncRelayCommand(PostUserAsync));
            HeaderViewModal = new(add, true);
        }

        [RelayCommand]
        public async Task PostUserAsync ()
        {
            if (string.IsNullOrWhiteSpace(User.Name) || string.IsNullOrWhiteSpace(User.Lastname)
                || string.IsNullOrWhiteSpace(User.Phone) || string.IsNullOrWhiteSpace(User.Login)
                || string.IsNullOrWhiteSpace(User.Password) || User.Phone.Length < MIN_LENGHT_PHONE
                || User.Password.Length < MIN_LENGHT_PASSWORD || User.Login.Length < MIN_LENGHT_LOGIN)
            {
                await Shell.Current.DisplayAlert("Validation fail",
                   "No information was entered in some fields",
                   "Ok");
                return;
            }

            await _userService.PostUserAsync(User);
            await Shell.Current.Navigation.PopAsync();           
        }
    }
}
