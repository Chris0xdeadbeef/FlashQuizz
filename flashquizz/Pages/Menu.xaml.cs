using flashquizz.Pages.Play;
using Microsoft.Maui.Layouts;

namespace flashquizz.Pages;

public partial class Menu : ContentPage
{
    private bool _starsCreated;
    public Menu()
    {
        InitializeComponent();
        SizeChanged += OnSizeChanged;
    }

    private void OnSizeChanged(object? sender, EventArgs e)
    {
        if (_starsCreated || Width <= 0 || Height <= 0)
            return;

        _starsCreated = true;
        CreateStars(40);
    }

    private void CreateStars(byte count)
    { 
        Random random = new();

        for (byte i = 0; i < count; ++i)
        {
            float size = 0.005f;

            BoxView star = new()
            {
                Color = Colors.White,
                Opacity = random.NextDouble() * 0.6f + 0.2f,
                CornerRadius = 50
            };

            AbsoluteLayout.SetLayoutBounds(star, new Rect(
                random.NextDouble(),   
                random.NextDouble(),  
                size,       
                size        
            ));
            AbsoluteLayout.SetLayoutFlags(star, AbsoluteLayoutFlags.All);

            StarsLayer.Children.Add(star);

            AnimateStar(star);
        }
    }

    private async void AnimateStar(View star)
    {
        var random = new Random();

        while (true)
        {
            // Fade vers 1 (apparition)
            await star.FadeTo(1, (uint)random.Next(500, 1500), Easing.SinInOut);

            // Pause courte (optionnel)
            await Task.Delay(random.Next(100, 500));

            // Fade vers 0 (disparition)
            await star.FadeTo(0, (uint)random.Next(500, 1500), Easing.SinInOut);

            // Nouvelle position aléatoire
            double size = AbsoluteLayout.GetLayoutBounds(star).Width; // taille proportionnelle conservée
            double newX = random.NextDouble();
            double newY = random.NextDouble();

            AbsoluteLayout.SetLayoutBounds(star, new Rect(newX, newY, size, size));
        }
    }

    private async void OnClickedGestionDeck(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeckGestion());
    }

    private async void OnClickedPlay(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CardPlay());
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Vérifie si on peut revenir en arrière
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
}