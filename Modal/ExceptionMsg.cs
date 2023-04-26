using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace LessonProj.Modal
{
    public partial class ExceptionMsg : ObservableObject
    {
        [ObservableProperty]
        private string _msg = "";
        [ObservableProperty]
        private int _code = 0;
    }
}
