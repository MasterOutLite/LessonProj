using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonProj.Modal
{
    public partial class PostOrders : ObservableObject
    {
        [ObservableProperty]
        private string _bookUuid = "";
        [ObservableProperty]
        private string _userUuid = "";
        [ObservableProperty]
        private string _librarianUuid = "";
        public PostOrders (string bookUuid, string userUuid)
        {
            BookUuid = bookUuid;
            UserUuid = userUuid;
        }
    }
}
