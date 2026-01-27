namespace flashquizz.Pages.Card;

public partial class ShowCard : ContentPage
{
    private readonly Models.Deck _deck;
    private readonly AddCard _cardAdd;
    private bool _isSwiping = false;

    /// <summary>
    /// Initialise la page d'affichage des cartes d'un deck.
    /// </summary>
    public ShowCard(AddCard addCard, Models.Deck deck)
    {
        _cardAdd = addCard;
        _deck = deck;

        BindingContext = _deck;

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
    /// Ouvre la page permettant d'ajouter une nouvelle carte au deck.
    /// </summary>
    private async void OnClickedAddCard(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_cardAdd);
    }

    /// <summary>
    /// Désactive la sélection lorsque l'utilisateur commence un swipe.
    /// </summary>
    private void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        _isSwiping = true;
        CardCollection.SelectionMode = SelectionMode.None;
    }

    /// <summary>
    /// Réactive la sélection lorsque le swipe est terminé.
    /// </summary>
    private void OnSwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        _isSwiping = false;
        CardCollection.SelectionMode = SelectionMode.Single;
    }

    /// <summary>
    /// Gère la sélection d'une carte, sauf si un swipe est en cours.
    /// </summary>
    private async void OnCardSelected(object sender, SelectionChangedEventArgs e)
    {
        if (_isSwiping)
        {
            ((CollectionView)sender).SelectedItem = null;
            return;
        }

        if (e.CurrentSelection.FirstOrDefault() is Models.Card selectedCard)
        {
            //TODO Aller sur la page modifier card
        }

        ((CollectionView)sender).SelectedItem = null;
    }

    /// <summary>
    /// Supprime la carte sélectionnée après confirmation de l'utilisateur.
    /// </summary>
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
