using System.ComponentModel;

namespace LessonProj.Modal
{
    public class StringProp : INotifyPropertyChanged
    {
        private string _text = "Default";

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));  
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
                   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
