using LessonProj.Page;
using LessonProj.Service;
using LessonProj.ViewModal;
using Microsoft.Extensions.Logging;

namespace LessonProj;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp ()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        
        builder.Services.AddSingleton<BookService>();
        builder.Services.AddSingleton<LibrarianService>();
        builder.Services.AddSingleton<LibraryService>();
        builder.Services.AddSingleton<OrdersService>();
        builder.Services.AddSingleton<UserService>();
       
        builder.Services.AddTransient<BookView>();
        builder.Services.AddTransient<UserListView>();
        builder.Services.AddTransient<OrderView>();
        builder.Services.AddTransient<AddLibrary>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}