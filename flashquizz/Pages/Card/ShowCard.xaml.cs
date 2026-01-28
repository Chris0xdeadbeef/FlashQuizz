using flashquizz.Services;

namespace flashquizz.Pages.Card;

public partial class ShowCard : ContentPage
{
    private readonly Models.Deck _deck;
    private readonly DeckService _deckService;
    private bool _isSwiping = false;

    /// <summary>
    /// Initialise la page d'affichage des cartes d'un deck.
    /// </summary>
    public ShowCard(AddCard addCard, Models.Deck deck)
    {
        // On récupère le service depuis addCard
        _deckService = addCard.GetType()
                              .GetField("_deckService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                              ?.GetValue(addCard) as DeckService
                              ?? throw new Exception("DeckService introuvable");

        _deck = deck;

        BindingContext = _deck;

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Force le rafraîchissement de la liste
        CardCollection.ItemsSource = null;
        CardCollection.ItemsSource = _deck.Cards;
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
        await Navigation.PushAsync(new AddCard(_deck, _deckService));
    }

    /// <summary>
    /// Désactive la sélection lorsque l'utilisateur commence un swipe.
    /// </summary>
    private void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        _isSwiping = true;
        CardCollection.SelectionMode = SelectionMode.None;
        CardCollection.SelectedItem = null;
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
    /// Ouvre la page AddCard en mode modification.
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
            await Navigation.PushAsync(
                new AddCard(_deck, _deckService, selectedCard)
            );
        }

        ((CollectionView)sender).SelectedItem = null;
    }


    /// <summary>
    /// Supprime la carte sélectionnée après confirmation de l'utilisateur.
    /// </summary>
    private async void OnDeleteCard(object sender, EventArgs e)
    {
        CardCollection.SelectedItem = null;

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
