using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Page;
using LessonProj.Service;
using System.Collections.ObjectModel;

namespace LessonProj.ViewModal
{
    public partial class OrderListViewModal : ObservableObject
    {
        public HeaderViewModal HeaderViewModal { get; }
        public MoreBtnViewModal MoreBtnViewModal { get; }
        public ObservableCollection<OrdersViewModal> Orders { get; } = new();
        public OrdersViewModal SelectedOrders { get; set; }

        [ObservableProperty]
        private bool _showFooter = false;

        private OrdersService _ordersService;
        private IServiceProvider _serviceProvider;
        private CollectionView _collectionView;
        private List<Librarian> librarians = new();
        private List<Book> books = new();
        private List<User> users = new();
        public OrderListViewModal (OrdersService ordersService,
                                   IServiceProvider serviceProvider, CollectionView collectionView)
        {
            ShowFooter = false;
            _ordersService = ordersService;
            _serviceProvider = serviceProvider;
            _collectionView = collectionView;
            var update = new ShowButton("Оновити", new AsyncRelayCommand(GetOrders));
            var add = new ShowButton("Додати", new AsyncRelayCommand(OpenAddOrders));
            HeaderViewModal = new(null, update, add);
            MoreBtnViewModal = new(new AsyncRelayCommand(GetOrders));

            if (_ordersService.IsBackup)
            {
                UpdateOrders(_ordersService.Backup).Wait();
            }
        }

        [RelayCommand]
        public async Task GetOrders ()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            try
            {
                var response = await _ordersService.GetOrdersListAsync();
                await UpdateOrders(response);
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlert("Fail update",
                    "Check network connection or this fail server" + ex.Message, "Ok");
            }
        }

        [RelayCommand]
        public async Task OpenAddOrders ()
        {
            var userService = _serviceProvider.GetService<UserService>();
            var bookService = _serviceProvider.GetService<BookService>();
            await Shell.Current.Navigation.PushAsync(new AddOrders(_ordersService, userService, bookService, _serviceProvider));
        }

        [RelayCommand]
        public async Task SelectionOrders ()
        {
            _collectionView.SelectedItem = null;
        }

        private async Task UpdateOrders (List<Orders> ordersList)
        {
            var bookService = _serviceProvider.GetService<BookService>();
            var userService = _serviceProvider.GetService<UserService>();
            var librarianService = _serviceProvider.GetService<LibrarianService>();

            Orders.Clear();
            foreach (Orders orders in ordersList)
            {
                Librarian librarian = await librarianService.GetLibrarianByUuidAsync(orders.LibrarianUuid);
                Book book = await bookService.GetBookByUuidAsync(orders.BookUuid);
                User user = await userService.GetUserByUuidAsync(orders.UserUuid);

                Orders.Add(new OrdersViewModal(orders, user.Name + " " + user.Lastname, book.Name,
                    librarian.Name + " " + librarian.Lastname, _ordersService.PutOrdersAsync));
            }
            ShowFooter = true;
        }
    }
}
