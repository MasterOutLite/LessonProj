using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;

namespace LessonProj.ViewModal
{
    public partial class AddLibraryViewModal : ObservableObject
    {
        public Library Library { get; } = new Library();
        public HeaderViewModal HeaderViewModal { get; }
        private LibraryService _libraryService;

        public AddLibraryViewModal (LibraryService librarianService)
        {
            var add = new ShowButton("Додати", new AsyncRelayCommand(PostLibraryAsync));
            HeaderViewModal = new(add, true);
            _libraryService = librarianService;
        }

        [RelayCommand]
        public async Task PostLibraryAsync ()
        {
            if (string.IsNullOrWhiteSpace(Library.Name) || string.IsNullOrWhiteSpace(Library.Address))
            {
                await Shell.Current.DisplayAlert("Validation fail",
                    "No information was entered in some fields",
                    "Ok");
                return;
            }
                

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            await _libraryService.PostLibraryAsync(Library);
        }
    }
}
