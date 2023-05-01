using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class OrderView : ContentPage
    {
        private OrderListViewModal Modal;
        public OrderView (OrdersService ordersService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Modal = new OrderListViewModal (ordersService, serviceProvider,Selection);
            BindingContext = Modal;            
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
