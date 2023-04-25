using System.ComponentModel;

namespace LessonProj.Modal
{
    public class Library : INotifyPropertyChanged
    {
        private string _uuid = "";
        private string _address = "";
        private string _name = "";

        public string UUID { get => _uuid; set { _uuid = value; OnPropertyChanged(nameof(UUID)); } }
        public string Address { get => _address; set { _address = value; OnPropertyChanged(nameof(Address)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged (string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
