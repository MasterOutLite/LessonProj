using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class AddLibrary : ContentPage
    {
        private AddLibraryViewModal Modal;
        public AddLibrary(LibraryService librarianService)
        {
            Modal = new AddLibraryViewModal(librarianService);
            BindingContext = Modal;
            InitializeComponent();
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
