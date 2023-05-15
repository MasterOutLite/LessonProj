using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;

namespace LessonProj.ViewModal
{
    public partial class OrdersViewModal : ObservableObject
    {
        public Orders Orders { get; private set; }

        public IRelayCommand ReturnedBook { get; }
        private Action<Orders> PutOrders { get; }

        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _librarianName;
        [ObservableProperty]
        private string _bookName;
        
        public string Returned => Orders.Returned ? "Так" : "Ні";

        [ObservableProperty]
        public bool _visibleReturned;

        private BookService _bookService;
        private UserService _userService;
        private LibrarianService _librarianService;
        public OrdersViewModal (Orders orders, string userName, string bookName, string librarianName, Action<Orders> PutCommand)
        {
            Orders = orders;
            UserName = userName;
            BookName = bookName;
            LibrarianName = librarianName;
            VisibleReturned = !Orders.Returned;
           ReturnedBook = new RelayCommand(OnReturnedBook);
            this.PutOrders = PutCommand;
        }

        public void OnReturnedBook ()
        {
            VisibleReturned = false;
            Orders.Returned = true;
            PutOrders?.Invoke(Orders);
        }
    }
}
