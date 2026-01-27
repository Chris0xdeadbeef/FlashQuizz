using flashquizz.Services;

namespace flashquizz.Pages.Deck;

public partial class DeckAdd : ContentPage
{
    private readonly DeckService _deckService;

    /// <summary>
    /// Initialise la page permettant d'ajouter un nouveau deck.
    /// </summary>
    public DeckAdd(DeckService deckService)
    {
        InitializeComponent();
        _deckService = deckService;
    }

    /// <summary>
    /// Retourne à la page précédente si possible.
    /// </summary>
    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    /// <summary>
    /// Valide le titre, crée un nouveau deck et l'ajoute via le service global.
    /// </summary>
    private async void OnCreateDeckClicked(object sender, EventArgs e)
    {
        string titre = TitreEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(titre))
        {
            await DisplayAlert("Erreur", "Veuillez entrer un titre.", "OK");
            return;
        }

        Models.Deck nouveauDeck = new()
        {
            Title = titre
        };

        // Ajout via le service global
        _deckService.AddDeck(nouveauDeck);

        // Retour à la page précédente
        await Navigation.PopAsync();
    }
}
