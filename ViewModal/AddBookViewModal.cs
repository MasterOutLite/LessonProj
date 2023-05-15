using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;
using System.Collections.ObjectModel;

namespace LessonProj.ViewModal
{
    public partial class AddBookViewModal : ObservableObject
    {
        public HeaderViewModal HeaderModal { get; }
        public Book Book { get; } = new Book();

        public ObservableCollection<string> GenreList { get; }

        public ObservableCollection<string> LibraryList { get; }

        [ObservableProperty]
        private int _selectLibraryIndex = -1;
        [ObservableProperty]
        private int _selectGenreIndex = -1;

        private List<Library> _libraryList;

        private LibraryService _libraryService;
        private BookService _bookService;

        public AddBookViewModal (LibraryService libraryService, BookService bookService)
        {
            _libraryService = libraryService;
            _bookService = bookService;
            GenreList = new ObservableCollection<string> { "Roman", "History", "Fantasi" };
            LibraryList = new ObservableCollection<string>();
            var add = new ShowButton("Додати", new AsyncRelayCommand(PostBook));
            HeaderModal = new HeaderViewModal(add, true);
            Book.InLibrary = true;
            GetLibrary().Wait(500);
        }

        public async Task GetLibrary ()
        {
            try
            {
                if (!_libraryService.IsBackup)
                    await _libraryService.GetLibraryAsync();
            }
            catch
            {
                await Shell.Current.DisplayAlert("Get Library fail",
                   "Error connection to server.",
                   "Ok");
            }
            _libraryList = _libraryService.Backup;
            LibraryList.Clear();
            foreach (Library library in _libraryList)
                LibraryList.Add(library.Name);
        }

        [RelayCommand]
        public async Task PostBook ()
        {
            if (SelectLibraryIndex < 0 || SelectGenreIndex < 0
                || string.IsNullOrWhiteSpace(Book.Name) || string.IsNullOrWhiteSpace(Book.Author))
            {
                await Shell.Current.DisplayAlert("Validation fail",
                    "No information was entered in some fields",
                    "Ok");
                return;
            }

            Library selectLibrary = _libraryList[SelectLibraryIndex];
            Book.LibraryUuid = selectLibrary.Uuid;
            Book.Genre = GenreList[SelectGenreIndex];
            await _bookService.PostBookAsync(Book);
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
