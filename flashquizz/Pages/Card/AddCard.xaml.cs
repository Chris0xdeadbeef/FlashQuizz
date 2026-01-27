using flashquizz.Services;

namespace flashquizz.Pages.Card;

public partial class AddCard : ContentPage
{
    private readonly DeckService _deckService;
    private readonly Models.Deck _deck;

    /// <summary>
    /// Initialise la page permettant d'ajouter une nouvelle carte à un deck.
    /// </summary>
    public AddCard(Models.Deck deck, DeckService deckService)
    {
        _deck = deck;
        _deckService = deckService;

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
    /// Valide les champs, crée une nouvelle carte et l'ajoute au deck.
    /// </summary>
    private async void OnCreateCardClicked(object sender, EventArgs e)
    {
        string question = QuestionEntry.Text?.Trim() ?? "";
        string answer = AnswerEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(question))
        {
            await DisplayAlert("Erreur", "Veuillez entrer une question.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(answer))
        {
            await DisplayAlert("Erreur", "Veuillez entrer une réponse.", "OK");
            return;
        }

        Models.Card newCard = new()
        {
            Question = question,
            Answer = answer
        };

        // Ajout via le service global
        _deckService.AddCard(_deck, newCard);

        await Navigation.PopAsync();
    }
}
