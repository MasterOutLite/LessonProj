using CommunityToolkit.Mvvm.ComponentModel;
using LessonProj.Modal;
using LessonProj.Service;

namespace LessonProj.ViewModal
{
    public partial class OrdersViewModal : ObservableObject
    {
        public Orders Orders { get; private set; }

        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _librarianName;
        [ObservableProperty]
        private string _bookName;

        private BookService _bookService;
        private UserService _userService;
        private LibrarianService _librarianService;
        public OrdersViewModal (Orders orders, BookService bookService,
            UserService userService, LibrarianService librarianService)
        {
            Orders = orders;     
            _bookService = bookService;
            _userService = userService;
            _librarianService = librarianService;
            GetProperty();
        }

        private async void GetProperty ()
        {
            var book = await _bookService.GetBookByUuidAsync(Orders.BookUuid);
            var user = await _userService.GetUserByUuidAsync(Orders.UserUuid);
            var librarian = await _librarianService.GetLibrarianByUuidAsync(Orders.LibrarianUuid);

            BookName = book.Name;
            UserName = user.Name;
            LibrarianName = librarian.Name;            
        }
    }
}
