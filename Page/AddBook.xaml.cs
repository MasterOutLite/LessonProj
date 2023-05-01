
using LessonProj.Service;
using LessonProj.ViewModal;


namespace LessonProj.Page
{
    public partial class AddBook : ContentPage
    {
        private AddBookViewModal Modal { get; }
       
        public AddBook (LibraryService libraryService)
        {
            InitializeComponent();
            Modal = new AddBookViewModal(libraryService);
            BindingContext = Modal;                   
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
