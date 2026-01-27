using flashquizz.Pages.Card;
using flashquizz.Services;
using System.Collections.ObjectModel;

namespace flashquizz.Pages.Deck;

public partial class DeckGestion : ContentPage
{
    private readonly DeckService _deckService;
    private readonly DeckAdd _deckAdd;
    private bool _isSwiping = false;

    public static class DeckNavigation
    {
        public static Models.Deck? SelectedDeck { get; set; }
    }

    /// <summary>
    /// Liste observable des decks affichés dans la CollectionView.
    /// </summary>
    public ObservableCollection<Models.Deck> Decks => _deckService.Decks;

    /// <summary>
    /// Initialise la page de gestion des decks.
    /// </summary>
    public DeckGestion(DeckAdd deckAdd, DeckService deckService)
    {
        _deckAdd = deckAdd;
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
    /// Ouvre la page permettant d'ajouter un nouveau deck.
    /// </summary>
    private async void OnClickedAddDeck(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_deckAdd);
    }

    /// <summary>
    /// Ouvre la page des cartes du deck sélectionné, sauf si un swipe est en cours.
    /// </summary>
    private async void OnDeckSelected(object sender, SelectionChangedEventArgs e)
    {
        if (_isSwiping)
        {
            ((CollectionView)sender).SelectedItem = null;
            return;
        }

        if (e.CurrentSelection.FirstOrDefault() is Models.Deck selectedDeck)
        {
            await Navigation.PushAsync(
                new ShowCard(
                    new AddCard(selectedDeck, _deckService),
                    selectedDeck
                )
            );
        }
        // Désélectionne l’item pour éviter qu’il reste surligné
        ((CollectionView)sender).SelectedItem = null;
    }

    /// <summary>
    /// Supprime le deck sélectionné après confirmation de l'utilisateur.
    /// </summary>
    private async void OnDeleteDeck(object sender, EventArgs e)
    {
        SwipeItem? swipeItem = sender as SwipeItem;
        Models.Deck? deck = swipeItem?.CommandParameter as Models.Deck;

        if (deck == null)
            return;

        bool confirm = await DisplayAlert(
            "Supprimer le deck",
            $"Supprimer {deck.Title} ?",
            "Oui",
            "Annuler");

        if (!confirm)
            return;

        Decks.Remove(deck);
    }

    /// <summary>
    /// Désactive la sélection lorsque l'utilisateur commence un swipe.
    /// </summary>
    private void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        _isSwiping = true;
        DeckCollection.SelectionMode = SelectionMode.None;
    }

    /// <summary>
    /// Réactive la sélection lorsque le swipe est terminé.
    /// </summary>
    private void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        _isSwiping = false;
        DeckCollection.SelectionMode = SelectionMode.Single;
    }
}
