namespace LessonProj.Page
{
    public partial class AddOrders : ContentPage
    {
        public AddOrders()
        {
            InitializeComponent();
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
