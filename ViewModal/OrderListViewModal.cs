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
        public HeaderViewModal HeaderViewModal { get; }      
        public ObservableCollection<OrdersViewModal> Orders { get; } = new();
        public OrdersViewModal SelectedOrders { get; set; }

        public OrderListViewModal (OrdersService ordersService, IServiceProvider serviceProvider)
        {
            _ordersService = ordersService;
            _serviceProvider = serviceProvider;
            var update = new ShowButton("Оновити", new AsyncRelayCommand(GetOrders));
            var add = new ShowButton("Додати", new AsyncRelayCommand(OpenAddOrders));
            HeaderViewModal = new(null, update, add);
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
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Fail update",
                    "Check network connection or this fail server", "Ok");

            }
        }

        [RelayCommand]
        public async Task OpenAddOrders ()
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
