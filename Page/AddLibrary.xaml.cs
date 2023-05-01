using LessonProj.Service;
using LessonProj.ViewModal;

namespace LessonProj.Page
{
    public partial class AddLibrary : ContentPage
    {
        private AddLibraryViewModal Modal;
        public AddLibrary(LibraryService librarianService)
        {
            InitializeComponent();
            Modal = new AddLibraryViewModal(librarianService);
            BindingContext = Modal;           
#if WINDOWS
            Shell.SetNavBarIsVisible(this, false);
#endif
        }
    }
}
