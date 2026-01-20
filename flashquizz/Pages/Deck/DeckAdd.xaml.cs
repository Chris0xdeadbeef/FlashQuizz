using flashquizz.Services;

namespace flashquizz.Pages.Deck;

public partial class DeckAdd : ContentPage
{
    private readonly DeckService _deckService;
	public DeckAdd(DeckService deckService)
	{
		InitializeComponent();
        _deckService = deckService;
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // revenir en arrière
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

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
            Titre = titre
        };

        // Ajout via le service global
        _deckService.AddDeck(nouveauDeck);

        // Retour à la page précédente
        await Navigation.PopAsync();
    }
}