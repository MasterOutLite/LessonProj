using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class AddUser : ContentPage
    {
        public AddUserViewModal Modal { get; }
        public AddUser (UserService userService)
        {
            Modal = new AddUserViewModal (userService);
            BindingContext = Modal;
            InitializeComponent();
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }       
    }
}
