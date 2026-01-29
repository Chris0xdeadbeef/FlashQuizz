using flashquizz.Pages.Card;
using flashquizz.Services;
using System.Collections.ObjectModel;

namespace flashquizz.Pages.Play;

public partial class DeckChoice : ContentPage
{
    private readonly DeckService _deckService;

    /// <summary>
    /// Liste observable des decks disponibles pour le mode "Play".
    /// </summary>
    public ObservableCollection<Models.Deck> Decks => _deckService.Decks;

    /// <summary>
    /// Initialise la page permettant de choisir un deck pour jouer.
    /// </summary>
    public DeckChoice(DeckService deckService)
    {
        _deckService = deckService;

        BindingContext = this;

        InitializeComponent();
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
    /// Ouvre la page pour jouer le deck sélectionné
    /// </summary>
    private async void OnDeckSelected(object sender, SelectionChangedEventArgs e)
    {      
        if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is Models.Deck selectedDeck)
        {
            await Navigation.PushAsync(new CardPlay(selectedDeck));
        }
        // Désélectionne l’item pour éviter qu’il reste surligné
        ((CollectionView)sender).SelectedItem = null;
    }
}
