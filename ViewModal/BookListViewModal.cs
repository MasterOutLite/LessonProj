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
        public BookViewModal SelectedBook { get; set; }

        public HeaderViewModal HeaderViewModal { get; }

        private BookService _bookService;
        private IServiceProvider _provider;
        public BookListViewModal (BookService bookService, IServiceProvider provider)
        {
            _provider = provider;
            _bookService = bookService;
            var update = new ShowButton("Оновити", new AsyncRelayCommand(GetAllBook));
            var add = new ShowButton("Додати", new AsyncRelayCommand(AddBook));
            HeaderViewModal = new(null, update, add);
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
                var response = await _bookService.GetBooksAsync();

                var libraryService = _provider.GetService<LibraryService>();

                Books.Clear();
                foreach (Book book in response)
                {
                    await libraryService.GetLibraryByUuidAsync(book.LibraryUuid);
                    Books.Add(new BookViewModal(book, libraryService));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Fail update",
                    "Check network connection or this fail server", "Ok");
            }
        }

        [RelayCommand]
        public async Task AddBook ()
        {
            var service = _provider.GetService<LibraryService>();
            await Shell.Current.Navigation.PushAsync(new AddBook(service));
        }

        [RelayCommand]
        public async Task SelectionBook ()
        {
            if (SelectedBook != null)
                await Shell.Current.DisplayAlert("SelectionBook", $"SelectionBook. Selected Name: {SelectedBook.Book}. Library: {SelectedBook.LibraryName}", "Ok");
            else
                await Shell.Current.DisplayAlert("SelectionBook", $"SelectionBook. Select null", "Ok");

            SelectedBook = null;
        }
    }
}
