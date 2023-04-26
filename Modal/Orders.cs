using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.ComponentModel;

namespace LessonProj.Modal
{
    public partial class Orders : ObservableObject
    {
        [ObservableProperty]
        private string _uuid = "";
        [ObservableProperty]
        private string _bookUuid = "";
        [ObservableProperty]
        private string _userUuid = "";
        [ObservableProperty]
        private string _librarianUuid = "";
        [ObservableProperty]
        private DateTimeOffset _tookInDate = DateTimeOffset.Now;
        [ObservableProperty]
        private DateTimeOffset _returnDate;
        [ObservableProperty]
        private bool _returned = false;
    }
}
