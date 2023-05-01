using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonProj.ViewModal
{
    public partial class MoreBtnViewModal
    {
        public IRelayCommand Command { get; }
        public string Text { get; }
        public MoreBtnViewModal (IRelayCommand command, string text = "Більше")
        {
            Command = command;
            Text = text;
        }
    }
}
