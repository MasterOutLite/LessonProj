using CommunityToolkit.Mvvm.Input;
using MvvmHelpers.Commands;
using System.Windows.Input;

namespace LessonProj.ViewModal
{  
    public partial class HeaderViewModal
    {
        public IRelayCommand RelayCommand { get; private set; }
        public ICommand Preview { get; }
        public ICommand Add { get; }
        public object Navigation { get; private set; }

        public HeaderViewModal (ICommand Add)
        {
            RelayCommand = new AsyncRelayCommand(async () => await ToPrevious());
            this.Add = Add;
            Preview = new AsyncCommand(async () => await ToPrevious());
        }

        [RelayCommand]
        public async Task ToPrevious ()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
