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
        public OrdersViewModal (Orders orders, string userName,string bookName, string librarianName)
        {
            Orders = orders;     
            UserName = userName;
            BookName = bookName;
            LibrarianName = librarianName;
        }       
    }
}
