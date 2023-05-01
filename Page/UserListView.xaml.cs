using LessonProj.Modal;
using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class UserListView : ContentPage
    {
        public UserListViewModal Modal { get; private set; }
        public UserListView (UserService userService, Action<User> takeUser = null)
        {
            InitializeComponent();
            Modal = new UserListViewModal(userService, Selection, takeUser);
            BindingContext = Modal;
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
