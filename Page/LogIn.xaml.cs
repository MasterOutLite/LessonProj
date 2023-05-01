using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class LogIn : ContentPage
    {
        public LogIn (LibrarianService librarianService)
        {
            Modal = new LogInViewModal (librarianService);
            BindingContext = Modal;
            InitializeComponent();
        }

        public LogInViewModal Modal { get; }
    }
}
