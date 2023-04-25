using Newtonsoft.Json;
using System.ComponentModel;

namespace LessonProj.Modal
{
    public class Orders : INotifyPropertyChanged
    {

        private string _uuid = "";
        private string _bookUuid = "";
        private string _userUuid = "";
        private string _librarianUuid = "";
        private DateTimeOffset _tookInDate = DateTimeOffset.Now;       
        private DateTimeOffset _returnDate;
        private bool _returned = false;

        public string UUID { get => _uuid; set { _uuid = value; OnPropertyChanged(nameof(UUID)); } }
        public string BookUuid { get => _bookUuid; set { _bookUuid = value; OnPropertyChanged(nameof(BookUuid)); } }
        public string UserUuid { get => _userUuid; set { _userUuid = value; OnPropertyChanged(nameof(UserUuid)); } }
        public string LibrarianUuid { get => _librarianUuid; set { _librarianUuid = value; OnPropertyChanged(nameof(LibrarianUuid)); } }
        public DateTimeOffset TookInDate { get => _tookInDate; set { _tookInDate = value; OnPropertyChanged(nameof(TookInDate)); } }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ReturnDate { get => _returnDate; set { _returnDate = value; OnPropertyChanged(nameof(ReturnDate)); } }
        public bool Returned { get => _returned; set { _returned = value; OnPropertyChanged(nameof(Returned)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged (string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
