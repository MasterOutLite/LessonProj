using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Page;
using LessonProj.Service;
using System.Collections.ObjectModel;

namespace LessonProj.ViewModal
{
    public partial class UserListViewModal : ObservableObject
    {      

        public ObservableCollection<User> Users { get; } = new();

        public User SelectedUser { get; set; }
        public CollectionView Selection { get; }

        [ObservableProperty]
        private string _findString;

        private UserService _userService;
        private Action<User> _takeUser;
        public UserListViewModal (UserService userService,CollectionView collectionView)
        {
            _userService = userService;
            Selection = collectionView;
        }

        public UserListViewModal (UserService userService,Action<User> takeUser)
        {
            _userService = userService;
            _takeUser = takeUser;
        }

        [RelayCommand]
        public async Task GetAllUser ()
        {
            var response = await _userService.GetUsersAsync();

            Users.Clear();
            foreach (var user in response)
                Users.Add(user);
        }

        [RelayCommand]
        public async Task OpenAddUser ()
        {
            await Shell.Current.Navigation.PushAsync(new AddUser(_userService));
        }


        [RelayCommand]
        public async Task SelectionUser ()
        {            
            if(_takeUser != null)
            {
                _takeUser.Invoke(SelectedUser);
                await Shell.Current.Navigation.PopAsync();
            }

            await Shell.Current.DisplayAlert("SelectionUser", $"SelectionUser.", "Ok");
            Selection.SelectedItem = null;
        }
    }
}
