using flashquizz.Services;

namespace flashquizz.Pages.Deck;

/// <summary>
/// Page permettant d'ajouter un nouveau deck.
/// L'utilisateur saisit un titre, puis le deck est ajouté via le DeckService.
/// </summary>
public partial class DeckAdd : ContentPage
{
    private readonly DeckService _deckService;

    /// <summary>
    /// Constructeur principal.
    /// Initialise la page d'ajout de deck et stocke le service global.
    /// </summary>
    /// <param name="deckService">Service de gestion des decks.</param>
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
    /// Valide le titre saisi, crée un nouveau deck et l'ajoute via le service global.
    /// Affiche un message d'erreur si le champ est vide.
    /// </summary>
    private async void OnCreateDeckClicked(object sender, EventArgs e)
    {
        string titre = TitreEntry.Text?.Trim() ?? "";

        // Vérification du champ
        if (string.IsNullOrWhiteSpace(titre))
        {
            await DisplayAlert("Erreur", "Veuillez entrer un titre.", "OK");
            return;
        }

        // Création du deck
        Models.Deck nouveauDeck = new()
        {
            Title = titre
        };

        // Ajout via le service global
        _deckService.AddDeck(nouveauDeck);

        // Retour à la page précédente
        await Navigation.PopAsync();
    }

    /// <summary>
    /// Retire le focus du champ lorsque l'utilisateur tape en dehors.
    /// </summary>
    private void OnBackgroundTapped(object sender, TappedEventArgs e)
    {
        TitreEntry.EntryControl.Unfocus();
    }
}
