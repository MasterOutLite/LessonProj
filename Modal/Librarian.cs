using CommunityToolkit.Mvvm.ComponentModel;

namespace LessonProj.Modal
{
    public partial class Librarian : ObservableObject
    {
        [ObservableProperty]
        private string _uuid = "";
        [ObservableProperty]
        private string _name = "";
        [ObservableProperty]
        private string _lastname = "";
        [ObservableProperty]
        private string _position = "";
        [ObservableProperty]
        private string _libraryUuid = "";
        [ObservableProperty]
        private bool _working = false;
    }
}
