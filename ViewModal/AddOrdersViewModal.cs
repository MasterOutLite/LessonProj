using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Page;
using LessonProj.Service;

namespace LessonProj.ViewModal
{
    public partial class AddOrdersViewModal : ObservableObject
    {
        [ObservableProperty]
        private User _user;
        [ObservableProperty]
        private BookViewModal _book;

        [ObservableProperty]
        private bool _showUser = false;
        [ObservableProperty]
        private bool _showBook = false;
        public HeaderViewModal HeaderModal { get; }

        private IServiceProvider _serviceProvider;
        private OrdersService _ordersService;
        private UserService _userService;
        private BookService _bookService;
        public AddOrdersViewModal (OrdersService ordersService, UserService userService, BookService bookService, IServiceProvider serviceProvider)
        {
            _ordersService = ordersService;
            _userService = userService;
            _bookService = bookService;
            _serviceProvider = serviceProvider;
            var addBtn = new ShowButton("Додати", new AsyncRelayCommand(PostOrders));
            HeaderModal = new HeaderViewModal(addBtn, true);
        }

        [RelayCommand]
        public async Task TakeUser ()
        {
            await Shell.Current.Navigation.PushAsync(new UserListView(_userService, SetUser));
        }

        [RelayCommand]
        public async Task TakeBook ()
        {
            await Shell.Current.Navigation.PushAsync(new BookView(_bookService, _serviceProvider, SetBook));
        }

        [RelayCommand]
        public async Task PostOrders ()
        {
            if (User == null || Book == null)
            {
                await Shell.Current.DisplayAlert("Validation fail",
                  "No information was entered in some fields",
                  "Ok");
                return;
            }

            PostOrders orders = new PostOrders(Book.Book.Uuid, User.Uuid);
            await _ordersService.PostOrdersAsync(orders);
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        public void ClearUser ()
        {
            User = null;
            ShowUser = false;
        }

        private void SetUser (User user)
        {
            User = user;
            ShowUser = true;
        }

        [RelayCommand]
        public void ClearBook ()
        {
            Book = null;
            ShowBook = false;
        }
        private void SetBook (BookViewModal book)
        {
            Book = book;
            ShowBook = true;
        }
    }
}
