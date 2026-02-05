using CommunityToolkit.Maui;
using flashquizz.Services;
using flashquizz.Pages;
using flashquizz.Pages.Deck;
using flashquizz.Pages.Play;
using flashquizz.Pages.Card;

namespace flashquizz
{
    /// <summary>
    /// Classe statique responsable de la configuration et de la création
    /// de l'application MAUI. C'est ici que sont enregistrés les services,
    /// les pages et les dépendances nécessaires au fonctionnement global.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Point d'entrée principal pour construire l'application MAUI.
        /// Configure les polices, les services, les pages et les outils utilisés.
        /// </summary>
        /// <returns>Une instance entièrement configurée de <see cref="MauiApp"/>.</returns>
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            // --- Configuration de base de l'application ---
            builder
                .UseMauiApp<App>()                 // Définit la classe App comme racine
                .UseMauiCommunityToolkit()         // Active les outils CommunityToolkit.Maui
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // --- Enregistrement des services ---

            /// <summary>
            /// DeckService est enregistré en Singleton pour garantir
            /// une instance unique partagée dans toute l'application.
            /// Cela évite les doublons et permet une gestion cohérente des decks.
            /// </summary>
            builder.Services.AddSingleton<DeckService>();

            // --- Enregistrement des pages ---
            // Transient = une nouvelle instance est créée à chaque navigation

            builder.Services.AddTransient<Menu>();
            builder.Services.AddTransient<DeckGestion>();
            builder.Services.AddTransient<DeckAdd>();
            builder.Services.AddTransient<DeckChoice>();
            builder.Services.AddTransient<AddCard>();
            builder.Services.AddTransient<ShowCard>();
            builder.Services.AddTransient<CardPlay>();
            builder.Services.AddTransient<EndGameStats>();

            // Construction finale de l'application
            return builder.Build();
        }
    }
}
