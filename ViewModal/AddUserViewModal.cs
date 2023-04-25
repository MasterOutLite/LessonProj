using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;
using MvvmHelpers.Commands;

namespace LessonProj.ViewModal
{
    public partial class AddUserViewModal : ObservableObject
    {
        public AddUserViewModal (UserService userService)
        {
            _userService = userService;
            HeaderViewModal = new(new AsyncCommand(async () => await PostUserAsync()));
        }

        public HeaderViewModal HeaderViewModal { get; }
        public UserLogIn User { get; } = new UserLogIn();
        public IRelayCommand Add { get; }

        private UserService _userService;

        [RelayCommand]
        public async Task PostUserAsync ()
        {
            if (string.IsNullOrWhiteSpace(User.Name) || string.IsNullOrWhiteSpace(User.Lastname)
                || string.IsNullOrWhiteSpace(User.Phone) || string.IsNullOrWhiteSpace(User.Login)
                || string.IsNullOrWhiteSpace(User.Password) || User.Password.Length < 6 || User.Login.Length < 5)
            {
                await Shell.Current.DisplayAlert("Validation fail",
                   "No information was entered in some fields",
                   "Ok");
                return;
            }

            await _userService.PostUserAsync(User);
        }
    }
}
