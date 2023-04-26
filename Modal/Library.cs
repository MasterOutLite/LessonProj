using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace LessonProj.Modal
{
    public partial class Library : ObservableObject
    {
        [ObservableProperty]
        private string _uuid = "";
        [ObservableProperty]
        private string _address = "";
        [ObservableProperty]
        private string _name = "";       
    }
}
