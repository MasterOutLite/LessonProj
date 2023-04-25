using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class OrderView : ContentPage
    {
        private OrderListViewModal Modal;
        public OrderView (OrdersService ordersService, IServiceProvider serviceProvider)
        {
            Modal = new OrderListViewModal (ordersService, serviceProvider);
            BindingContext = Modal;
            InitializeComponent();
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
