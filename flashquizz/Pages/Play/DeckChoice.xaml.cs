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
    private void OnDeckSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Deck deck)
        {
            // deck vide → on annule la sélection et on ne fait rien
            if (deck.Cards.Count == 0)
            {
                ((CollectionView)sender).SelectedItem = null;
                return;
            }

            // deck valide → navigation
            Navigation.PushAsync(new CardPlay(deck));
        }
        ((CollectionView)sender).SelectedItem = null;
    }

}
