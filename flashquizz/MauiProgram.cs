using CommunityToolkit.Maui;
using flashquizz.Services;
using flashquizz.Pages;
using flashquizz.Pages.Deck;
using flashquizz.Pages.Play;

namespace flashquizz
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Enregistrement du DeckService en Singleton pour éviter des erreurs de doublons
            builder.Services.AddSingleton<DeckService>();

            builder.Services.AddTransient<Menu>();
            builder.Services.AddTransient<DeckGestion>();
            builder.Services.AddTransient<DeckAdd>();
            builder.Services.AddTransient<DeckChoice>();

            return builder.Build();
        }
    }
}
