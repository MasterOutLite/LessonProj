using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Page;
using LessonProj.Service;
using System.Collections.ObjectModel;

namespace LessonProj.ViewModal
{
    public partial class BookListViewModal : ObservableObject
    {
        public ObservableCollection<BookViewModal> Books { get; } = new();
        private List<Book> _books { get; } = new();
        public BookViewModal SelectedBook { get; set; }
        public HeaderViewModal HeaderViewModal { get; }
        public MoreBtnViewModal MoreBtnViewModal { get; }

        [ObservableProperty]
        private bool _showFooter = false;

        private BookService _bookService;
        private LibraryService _libraryService;
        private IServiceProvider _provider;
        private Action<BookViewModal> _takeBook;
        private int _page = 1;
        private List<Library> libraries = new();
        public BookListViewModal (BookService bookService,
                                  IServiceProvider provider, Action<BookViewModal> takeBook = null)
        {
            _provider = provider;
            _takeBook = takeBook;
            _bookService = bookService;
            _libraryService = _provider.GetService<LibraryService>();
            var update = new ShowButton("Оновити", new AsyncRelayCommand(GetAllBook));
            var add = new ShowButton("Додати", new AsyncRelayCommand(AddBook));
            HeaderViewModal = new(update, add, takeBook != null);
            MoreBtnViewModal = new(new AsyncRelayCommand(GetMoreBookAsync));

            if (_bookService.IsBackup)
            {
                AddBook(_bookService.Backup).Wait(500);
            }
        }

        [RelayCommand]
        public async Task GetAllBook ()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            try
            {
                var response = await _bookService.GetBooksByPageAsync(1, _page * 10);
                Books.Clear();
                await AddBook(response);
            }
            catch
            {
                await Shell.Current.DisplayAlert("Fail update",
                    "Check network connection or this fail server", "Ok");
            }
        }

        [RelayCommand]
        public async Task GetMoreBookAsync ()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            try
            {
                _page++;
                var response = await _bookService.GetBooksByPageAsync(_page);
                await AddBook(response);
            }
            catch
            {
                _page--;
                await Shell.Current.DisplayAlert("Fail update",
                    "Check network connection or this fail server", "Ok");
            }
        }

        [RelayCommand]
        public async Task AddBook ()
        {
            var service = _provider.GetService<LibraryService>();
            await Shell.Current.Navigation.PushAsync(new AddBook(service, _bookService));
        }

        [RelayCommand]
        public async Task SelectionBook ()
        {
            if (_takeBook != null)
            {
                _takeBook?.Invoke(SelectedBook);
                await Shell.Current.Navigation.PopAsync();
            }

            SelectedBook = null;
        }

        [RelayCommand]
        public async void SearchBookByName (string name)
        {
            name = name.ToLower();
            if (string.IsNullOrWhiteSpace(name))
            {
                await SetBook(_books);
                return;
            }
            var search = _books.FindAll(book => book.Name.ToLower().Contains(name));
            await SetBook(search);
        }

        [RelayCommand]
        public void SearchAll ()
        {
             SearchBookByName("");
        }

        private async Task<string> GetLibraryNameAsync (string libraryUuid)
        {
            if (FindLocalLibrary(libraryUuid, out var library))
                return library.Name;
            library = await _libraryService.GetLibraryByUuidAsync(libraryUuid);
            libraries.Add(library);
            return library.Name;
        }

        private async Task AddBook (List<Book> booksList)
        {
            foreach (Book book in booksList)
            {
                string nameLibrary = await GetLibraryNameAsync(book.LibraryUuid);
                _books.Add(book);
                Books.Add(new BookViewModal(book, nameLibrary));
            }
            ShowFooter = true;
        }

        private async Task SetBook (List<Book> booksList)
        {
            Books.Clear();
            foreach (Book book in booksList)
            {
                string nameLibrary = await GetLibraryNameAsync(book.LibraryUuid);
                Books.Add(new BookViewModal(book, nameLibrary));
            }            
        }

        private bool FindLocalLibrary (string libraryUuid, out Library library)
        {
            library = libraries.Find(librarie => librarie.Uuid.Contains(libraryUuid));
            return library != null;
        }
    }
}
