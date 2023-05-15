
using LessonProj.Service;
using LessonProj.ViewModal;


namespace LessonProj.Page
{
    public partial class AddBook : ContentPage
    {
        private AddBookViewModal Modal { get; }
       
        public AddBook (LibraryService libraryService,BookService bookService)
        {
            InitializeComponent();
            Modal = new AddBookViewModal(libraryService, bookService);
            BindingContext = Modal;                   
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
