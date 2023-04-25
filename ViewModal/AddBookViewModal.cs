using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;
using MvvmHelpers.Commands;
using System.Collections.ObjectModel;

namespace LessonProj.ViewModal
{
    public partial class AddBookViewModal : ObservableObject
    {
        public HeaderViewModal HeaderModal { get; }
        public Book Book { get;  } = new Book();

        public ObservableCollection<string> GenreList { get; }

        public ObservableCollection<string> LibraryList { get;  }

        [ObservableProperty]
        private int _selectLibraryIndex = -1;
        [ObservableProperty]
        private int _selectGenreIndex = -1;

        private List<Library> _libraryList;

        private LibraryService _libraryService;
        public AddBookViewModal (LibraryService libraryService)
        {
            _libraryService = libraryService;
            GenreList = new ObservableCollection<string> { "Roman", "History", "Fantasi" };
            LibraryList = new ObservableCollection<string>();
            HeaderModal = new HeaderViewModal(new AsyncCommand(async () => await PostBook()));
            Book.InLibrary = true;
            GetLibrary();
        }

        public async void GetLibrary ()
        {
            if (!_libraryService.IsBackup)
                await _libraryService.GetLibraryAsync();

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
            Book.LibraryUuid = selectLibrary.Name;

            await Shell.Current.DisplayAlert("Book", $"Name '{Book.Name}'. Genre '{Book.Genre}'. InLibrary '{Book.InLibrary}'. Author '{Book.Author}'. Library '{Book.LibraryUuid}'", "Ok");
        }
    }
}
