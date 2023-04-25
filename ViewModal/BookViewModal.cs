using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;

namespace LessonProj.ViewModal
{
    public partial class BookViewModal : ObservableObject
    {
        public BookViewModal (Book book, LibraryService libraryService)
        {
            Book = book;
            _libraryService = libraryService;
            GetLibraryNameAsync();
        }

        private LibraryService _libraryService;
        public Book Book { get; private set; }

        [ObservableProperty]
        private string _libraryName;

        [RelayCommand]
        public async void OpenBook ()
        {
            Library library = await _libraryService.GetLibraryByUuidAsync(Book.LibraryUuid);

            await Shell.Current.DisplayAlert("BookViewModal",
                $"BookViewModal. " +
                $"Name: {Book.Name}.Author: {Book.Author}.Library name {library.Name}", "Ok");
            //await Shell.Current.GoToAsync("..");
            
        }

        private async void GetLibraryNameAsync ()
        {
            Library library = await _libraryService.GetLibraryByUuidAsync(Book.LibraryUuid);
            LibraryName = library.Name.Substring(0, 10);
        }
    }
}
