using System.ComponentModel;

namespace LessonProj.Modal
{
    public class User : INotifyPropertyChanged
    {
        private string _uuid = "";
        private string _name = "";
        private string _lastname = "";
        private string _phone = "";
        private string _mail = "";

        public string UUID { get => _uuid; set { _uuid = value; OnPropertyChanged(nameof(UUID)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string LastName { get => _lastname; set { _lastname = value; OnPropertyChanged(nameof(LastName)); } }
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(nameof(Phone)); } }
        public string Mail { get => _mail; set { _mail = value; OnPropertyChanged(nameof(Mail)); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged (string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
