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

    public ObservableCollection<Models.Deck> Decks => _deckService.Decks;

    public DeckGestion(DeckAdd deckAdd, DeckService deckService)
    {        
        _deckAdd = deckAdd;
        _deckService = deckService;

        BindingContext = this;

        InitializeComponent();
    }
    
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Revenir à la page précédente
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
    private async void OnClickedAddDeck(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_deckAdd);
    }

    private async void OnDeckSelected(object sender, SelectionChangedEventArgs e)
    {
        // Empêche la navigation si un swipe est en cours
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

    private void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        _isSwiping = true;
        DeckCollection.SelectionMode = SelectionMode.None;
    }

    private void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        _isSwiping = false;
        DeckCollection.SelectionMode = SelectionMode.Single;
    }
}
