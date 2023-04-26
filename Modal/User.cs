using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace LessonProj.Modal
{
    public partial class User : ObservableObject
    {
        [ObservableProperty]
        private string _uuid = "";
        [ObservableProperty]
        private string _name = "";
        [ObservableProperty]
        private string _lastname = "";
        [ObservableProperty]
        private string _phone = "";
        [ObservableProperty]
        private string _mail = "";
    }
}
