using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LessonProj.Modal;
using LessonProj.Service;
using MvvmHelpers.Commands;

namespace LessonProj.ViewModal
{
    public partial class AddLibraryViewModal : ObservableObject
    {
        public Library Library { get; } = new Library();
        public HeaderViewModal HeaderViewModal { get; }
        private LibraryService _libraryService;

        public AddLibraryViewModal (LibraryService librarianService)
        {
            HeaderViewModal = new(new AsyncCommand(async () => await PostLibraryAsync()));
            _libraryService = librarianService;
        }

        [RelayCommand]
        public async Task PostLibraryAsync ()
        {
            if (string.IsNullOrWhiteSpace(Library.Name) || string.IsNullOrWhiteSpace(Library.Address))
                return;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Network Error", "Check network connection!", "Ok");
                return;
            }

            await _libraryService.PostLibraryAsync(Library);
        }
    }
}
