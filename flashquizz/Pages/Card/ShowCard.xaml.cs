namespace flashquizz.Pages.Card;

public partial class ShowCard : ContentPage
{
    private readonly Models.Deck _deck;
    private readonly AddCard _cardAdd;
    private bool _isSwiping = false;

    public ShowCard(AddCard addCard, Models.Deck deck)
    {
        _cardAdd = addCard;
        _deck = deck;

        BindingContext = _deck;

        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    private async void OnClickedAddCard(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_cardAdd);
    }

    private void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        _isSwiping = true;
        CardCollection.SelectionMode = SelectionMode.None;
    }

    private void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        _isSwiping = false;
        CardCollection.SelectionMode = SelectionMode.Single;
    }
    private async void OnCardSelected(object sender, SelectionChangedEventArgs e)
    {
        // Empêche la sélection si un swipe est en cours
        if (_isSwiping)
        {
            ((CollectionView)sender).SelectedItem = null;
            return;
        }

        if (e.CurrentSelection.FirstOrDefault() is Models.Card selectedCard)
        {
            // Aller sur la page modifier card
        }

        // Désélectionne l’item pour éviter qu’il reste surligné
        ((CollectionView)sender).SelectedItem = null;
    }

    private async void OnDeleteCard(object sender, EventArgs e)
    {
        SwipeItem? swipeItem = sender as SwipeItem;
        Models.Card? card = swipeItem?.CommandParameter as Models.Card;

        if (card == null)
            return;

        bool confirm = await DisplayAlert(
            "Supprimer la carte",
            $"Supprimer {card.Question} ?",
            "Oui",
            "Annuler");

        if (!confirm)
            return;

        _deck.Cards.Remove(card);
    }
}
