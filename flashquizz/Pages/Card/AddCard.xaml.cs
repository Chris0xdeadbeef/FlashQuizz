using flashquizz.Services;

namespace flashquizz.Pages.Card;

/// <summary>
/// Page permettant d'ajouter ou de modifier une carte dans un deck.
/// Gère deux modes : création et édition.
/// </summary>
public partial class AddCard : ContentPage
{
    private readonly DeckService _deckService;
    private readonly Models.Deck _deck;

    // Carte en cours d'édition (null si on est en mode création)
    private Models.Card? _editingCard = null;

    /// <summary>
    /// Constructeur pour le mode création.
    /// Permet d'ajouter une nouvelle carte à un deck existant.
    /// </summary>
    /// <param name="deck">Deck dans lequel ajouter la carte.</param>
    /// <param name="deckService">Service de gestion des decks.</param>
    public AddCard(Models.Deck deck, DeckService deckService)
    {
        _deck = deck;
        _deckService = deckService;

        InitializeComponent();

        // Mode création : bouton "Créer" visible, bouton "Modifier" caché
        CreateButton.IsVisible = true;
        EditButton.IsVisible = false;
    }

    /// <summary>
    /// Constructeur pour le mode édition.
    /// Permet de modifier une carte existante.
    /// </summary>
    /// <param name="deck">Deck contenant la carte.</param>
    /// <param name="deckService">Service de gestion des decks.</param>
    /// <param name="cardToEdit">Carte à modifier.</param>
    public AddCard(Models.Deck deck, DeckService deckService, Models.Card cardToEdit)
    {
        _deck = deck;
        _deckService = deckService;
        _editingCard = cardToEdit;

        InitializeComponent();

        // Pré-remplissage des champs
        QuestionInput.Text = cardToEdit.Question;
        AnswerInput.Text = cardToEdit.Answer;

        // Mode édition : bouton "Modifier" visible, bouton "Créer" caché
        CreateButton.IsVisible = false;
        EditButton.IsVisible = true;
        TitleLabel.Text = "Modifier la carte";
    }

    /// <summary>
    /// Retourne à la page précédente.
    /// </summary>
    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    /// <summary>
    /// Crée une nouvelle carte et l'ajoute au deck.
    /// Vérifie que les champs sont remplis avant validation.
    /// </summary>
    private async void OnCreateCardClicked(object sender, EventArgs e)
    {
        string question = QuestionInput.Text?.Trim() ?? "";
        string answer = AnswerInput.Text?.Trim() ?? "";

        // Validation des champs
        if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(answer))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        // Création de la carte
        Models.Card newCard = new()
        {
            Question = question,
            Answer = answer
        };

        _deckService.AddCard(_deck, newCard);

        await Navigation.PopAsync();
    }

    /// <summary>
    /// Enregistre les modifications apportées à une carte existante.
    /// </summary>
    private async void OnEditCardClicked(object sender, EventArgs e)
    {
        if (_editingCard == null)
            return;

        string question = QuestionInput.Text?.Trim() ?? "";
        string answer = AnswerInput.Text?.Trim() ?? "";

        // Validation des champs
        if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(answer))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        // Mise à jour de la carte
        _editingCard.Question = question;
        _editingCard.Answer = answer;

        await Navigation.PopAsync();
    }

    /// <summary>
    /// Retire le focus des champs pour fermer le clavier
    /// lorsque l'utilisateur tape en dehors.
    /// </summary>
    private void OnBackgroundTapped(object sender, TappedEventArgs e)
    {
        QuestionInput.EntryControl.Unfocus();
        AnswerInput.EntryControl.Unfocus();
    }
}
