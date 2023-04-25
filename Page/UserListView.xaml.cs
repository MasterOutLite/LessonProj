using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class UserListView : ContentPage
    {
        public UserListViewModal Modal { get; private set; }
        public UserListView (UserService userService)
        {
            InitializeComponent();
            Modal = new UserListViewModal(userService,Selection);
            BindingContext = Modal;
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
