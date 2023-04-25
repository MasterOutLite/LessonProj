using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonProj.Modal
{
    public partial class UserLogIn:ObservableObject
    {
        [ObservableProperty]
        private string _name = "";
        [ObservableProperty]
        private string _lastname = "";
        [ObservableProperty]
        private string _phone = "";
        [ObservableProperty]
        private string _mail = "";
        [ObservableProperty]
        private string _login = "";
        [ObservableProperty]
        private string _password = "";
    }
}
