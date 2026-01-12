# Anatomie d'une Application MAUI (.NET Multi-platform App UI)

## Introduction
MAUI (Multi-platform App UI) est un framework Microsoft pour créer des applications multiplateformes (Windows, Android, iOS, macOS) à partir d'une base de code unique. Ce guide résume sa structure fondamentale et les rôles de ses composants principaux.

## Structure de base d'une application MAUI

### 1. `MauiProgram.cs` - Point d'entrée
- Contient la classe `MauiProgram` avec la méthode statique `CreateMauiApp()`.
- Configure l'application (polices, services, logging…).
- Généralement, peu de modifications nécessaires sauf pour configurations spécifiques.

```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });
        #if DEBUG
        builder.Logging.AddDebug();
        #endif
        return builder.Build();
    }
}
