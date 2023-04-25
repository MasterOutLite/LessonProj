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
        private OrdersService _ordersService;
        private IServiceProvider _serviceProvider;
        public OrderListViewModal (OrdersService ordersService, IServiceProvider serviceProvider)
        {
            _ordersService = ordersService;
            _serviceProvider = serviceProvider;
        }

        public ObservableCollection<OrdersViewModal> Orders { get; } = new();
        public OrdersViewModal SelectedOrders { get; set; }

        [RelayCommand]
        public async Task GetOrders ()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            var response = await _ordersService.GetOrdersListAsync();

            var bookService = _serviceProvider.GetService<BookService>();
            var userService = _serviceProvider.GetService<UserService>();
            var librarianService = _serviceProvider.GetService<LibrarianService>();

            Orders.Clear();
            foreach (Orders orders in response)
            {
                await bookService.GetBookByUuidAsync(orders.BookUuid);
                await librarianService.GetLibrarianByUuidAsync(orders.LibrarianUuid);
                await userService.GetUserByUuidAsync(orders.UserUuid);

                Orders.Add(new OrdersViewModal(orders, bookService, userService, librarianService));
            }
        }

        [RelayCommand]
        public async Task ShowAddOrders ()
        {
            await Shell.Current.Navigation.PushAsync(new AddOrders());
        }

        [RelayCommand]
        public async Task SelectionOrders ()
        {
           await Shell.Current.DisplayAlert("Selected Orders", "Selected Orders", "Ok");
        }

    }
}
