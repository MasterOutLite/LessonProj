using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace LessonProj.Modal
{
    public partial class Book : ObservableObject
    {
        [ObservableProperty]
        private string _uuid = "";
        [ObservableProperty]
        private string _name = "";
        [ObservableProperty]
        private string _libraryUuid = "";
        [ObservableProperty]
        private string _genre = "";
        [ObservableProperty]
        private string _author = "";
        [ObservableProperty]
        private bool _inLibrary = false;       
    }
}
