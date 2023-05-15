using CommunityToolkit.Mvvm.ComponentModel;
using LessonProj.Modal;

namespace LessonProj.ViewModal
{
    public partial class BookViewModal : ObservableObject
    {
        public Book Book { get; private set; }
        [ObservableProperty]
        private string _libraryName;        
        public string InLibrary=>Book.InLibrary ? "Так" : "Ні";

        public BookViewModal (Book book, string libraryName)
        {
            Book = book;
            if (libraryName.Length > 28)
                LibraryName = libraryName.Substring(0, 30);
            else
                LibraryName = libraryName;
        }
    }
}
