using CommunityToolkit.Mvvm.Input;

namespace LessonProj.ViewModal
{
    public partial class HeaderViewModal
    {
        public ShowButton Preview { get; }
        public ShowButton Update { get; }
        public ShowButton Add { get; }
        public bool ShowPreview => Preview != null;
        public bool ShowUpdate => Update != null;
        public bool ShowAdd => Add != null;
        public HeaderViewModal (ShowButton preview, ShowButton update, ShowButton add)
        {
            Preview = preview;
            Update = update;
            Add = add;
            if (Preview != null)
                Preview = new ShowButton("Назад", new AsyncRelayCommand(ToPrevious));

        }

        [RelayCommand]
        public async Task ToPrevious ()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }

    public class ShowButton
    {
        public string Title { get; }
        public IRelayCommand Command { get; }

        public ShowButton (string title, IRelayCommand command)
        {
            Title = title;
            Command = command;
        }
        public ShowButton ()
        {
        }
    }
}
