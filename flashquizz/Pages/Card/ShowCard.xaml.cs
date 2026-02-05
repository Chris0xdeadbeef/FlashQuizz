using flashquizz.Services;

namespace flashquizz.Pages.Card;

/// <summary>
/// Page permettant d'afficher toutes les cartes d'un deck.
/// Permet également d'ajouter, modifier ou supprimer une carte.
/// </summary>
public partial class ShowCard : ContentPage
{
    private readonly Models.Deck _deck;
    private readonly DeckService _deckService;

    // Indique si un swipe est en cours pour éviter les conflits avec la sélection
    private bool _isSwiping = false;

    /// <summary>
    /// Constructeur principal.
    /// Initialise la page d'affichage des cartes d'un deck.
    /// Récupère le DeckService depuis la page AddCard (injection indirecte).
    /// </summary>
    /// <param name="addCard">Page AddCard utilisée pour récupérer le DeckService.</param>
    /// <param name="deck">Deck dont les cartes doivent être affichées.</param>
    public ShowCard(AddCard addCard, Models.Deck deck)
    {
        // Récupération du DeckService via réflexion (car AddCard le possède déjà)
        _deckService = addCard.GetType()
                              .GetField("_deckService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                              ?.GetValue(addCard) as DeckService
                              ?? throw new Exception("DeckService introuvable");

        _deck = deck;

        // Le BindingContext est le deck lui-même (utile pour le XAML)
        BindingContext = _deck;

        InitializeComponent();
    }

    /// <summary>
    /// Rafraîchit la liste des cartes à chaque apparition de la page.
    /// Permet de refléter les modifications faites dans AddCard.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Force le rafraîchissement de la CollectionView
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
    /// Déclenché lorsque l'utilisateur commence un swipe.
    /// Désactive temporairement la sélection pour éviter les conflits.
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
    /// Gère la sélection d'une carte.
    /// Si aucun swipe n'est en cours, ouvre la page AddCard en mode édition.
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

        // Réinitialise la sélection visuelle
        ((CollectionView)sender).SelectedItem = null;
    }

    /// <summary>
    /// Supprime une carte après confirmation de l'utilisateur.
    /// Appelé depuis un SwipeItem.
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
