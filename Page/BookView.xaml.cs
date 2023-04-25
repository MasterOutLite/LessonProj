using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class BookView : ContentPage
    {
        private BookListViewModal BookListViewModal { get; }
        public BookView (BookService bookService,IServiceProvider serviceProvider)
        {
            BookListViewModal = new BookListViewModal(bookService, serviceProvider);
            BindingContext = BookListViewModal;
            InitializeComponent();           
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
