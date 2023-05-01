using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class AddOrders : ContentPage
    {
        public AddOrdersViewModal Modal { get; }
        public AddOrders (OrdersService ordersService, UserService userService, BookService bookService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Modal = new AddOrdersViewModal(ordersService, userService, bookService, serviceProvider);
            BindingContext = Modal;
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }

    }
}
