using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class BookView : ContentPage
    {
        private BookListViewModal BookListViewModal { get; }
        public BookView (BookService bookService,IServiceProvider serviceProvider,Action<BookViewModal> takeBook = null)
        {
            BookListViewModal = new BookListViewModal(bookService, serviceProvider, takeBook);
            BindingContext = BookListViewModal;
            InitializeComponent();           
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
