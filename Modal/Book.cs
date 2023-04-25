using System.ComponentModel;

namespace LessonProj.Modal
{
    public class Book : INotifyPropertyChanged
    {
        public Book ()
        {
        }

        public Book (string uuid, string name, string libraryUuid, string ganre, string author, bool inLibrary)
        {
            _uuid = uuid;
            _name = name;
            _libraryUuid = libraryUuid;
            _ganre = ganre;
            _author = author;
            _inLibrary = inLibrary;
        }

        private string _uuid = "";
        private string _name = "";
        private string _libraryUuid = "";
        private string _ganre = "";
        private string _author = "";
        private bool _inLibrary = false;

        public string UUID { get => _uuid; set { _uuid = value; OnPropertyChanged(nameof(UUID)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); } }
        public string LibraryUuid { get => _libraryUuid; set { _libraryUuid = value; OnPropertyChanged(nameof(LibraryUuid)); } }
        public string Genre { get => _ganre; set { _ganre = value; OnPropertyChanged(nameof(Genre)); } }
        public string Author { get => _author; set { _author = value; OnPropertyChanged(nameof(Author)); } }
        public bool InLibrary { get => _inLibrary; set { _inLibrary = value; OnPropertyChanged(nameof(InLibrary)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged (string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
