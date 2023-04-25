using LessonProj.Modal;
using LessonProj.Service;
using System.ComponentModel;
using System.Diagnostics;

namespace LessonProj;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    public MainPage ()
    {
        InitializeComponent(); 
        BindingContext = this;       
    }

    public void ChangeText (object sender, EventArgs args)
    {
       
    }

    public async void OnPostRequestBook (object sender, EventArgs args)
    {
        var service = new LibrarianService();
        var response = await service.GetLibrariansAsync();
        Debug.WriteLine(response[0].Name);
    }
}

