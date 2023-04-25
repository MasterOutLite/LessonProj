using System.ComponentModel;

namespace LessonProj.Modal
{
    public class ExceptionMsg : INotifyPropertyChanged
    {
        private string _msg = "";
        private int _code = 0;

        public string Msg { get => _msg; set { _msg = value; OnPropertyChanged(nameof(Msg)); } }
        public int Code { get => _code; set { _code = value; OnPropertyChanged(nameof(Code)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged (string propertyName) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
