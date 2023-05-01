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
        public HeaderViewModal HeaderViewModal { get; }
        public MoreBtnViewModal MoreBtnViewModal { get; }
        public User SelectedUser { get; set; }
        public CollectionView Selection { get; }

        [ObservableProperty]
        private string _findString;
        [ObservableProperty]
        private bool _showFooter = false;

        private UserService _userService;
        private Action<User> _takeUser;
        public UserListViewModal (UserService userService, 
                                  CollectionView collectionView, Action<User> takeUser = null)
        {            
            _userService = userService;
            Selection = collectionView;
            _takeUser = takeUser;
            var update = new ShowButton("Оновити", new AsyncRelayCommand(GetAllUser));
            var add = new ShowButton("Додати", new AsyncRelayCommand(OpenAddUser));
            HeaderViewModal = new(update, add, takeUser != null);
            MoreBtnViewModal = new(new AsyncRelayCommand(GetAllUser));

            if (_userService.IsBackup)
            {
                UpdateUser(_userService.Backup);
            }
        }     

        [RelayCommand]
        public async Task GetAllUser ()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            try
            {
                var response = await _userService.GetUsersAsync();
                UpdateUser(response);
            }
            catch 
            {
                await Shell.Current.DisplayAlert("Fail update",
                    "Check network connection or this fail server", "Ok");
            }
        }

        [RelayCommand]
        public async Task OpenAddUser ()
        {
            await Shell.Current.Navigation.PushAsync(new AddUser(_userService));
        }


        [RelayCommand]
        public async Task SelectionUser ()
        {
            if (_takeUser != null)
            {
                _takeUser.Invoke(SelectedUser);
                await Shell.Current.Navigation.PopAsync();
            }
            Selection.SelectedItem = null;
        }

        private void UpdateUser(List<User> usersList)
        {
            Users.Clear();
            foreach (var user in usersList)
                Users.Add(user);
            ShowFooter = true;
        }        
    }
}
