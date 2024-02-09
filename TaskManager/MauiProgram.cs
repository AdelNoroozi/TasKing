using TaskManager.Services;
using TaskManager.ViewModel;
using TaskManager.Views;

namespace TaskManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

////#if DEBUG
////            builder.Logging.AddDebug();
////#endif

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<DetailViewModel>();


            builder.Services.AddTransient<AddPage>();
            builder.Services.AddTransient<AddViewModel>();
                       
            builder.Services.AddTransient<RecycleBinPage>();
            builder.Services.AddTransient<RecycleBinViewModel>();            
            
            builder.Services.AddTransient<FilterPage>();
            builder.Services.AddTransient<FilterViewModel>();

            builder.Services.AddSingleton<ITaskService, TaskService>();

            return builder.Build();
        }
    }
}