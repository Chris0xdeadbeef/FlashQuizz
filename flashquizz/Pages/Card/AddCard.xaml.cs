using flashquizz.Services;

namespace flashquizz.Pages.Card;

public partial class AddCard : ContentPage
{
    private readonly DeckService _deckService;
    private readonly Models.Deck _deck;

    public AddCard(Models.Deck deck, DeckService deckService)
    {
        _deck = deck;
        _deckService = deckService;

        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Revenir à la page précédente
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
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

        // Retour à la page précédente
        await Navigation.PopAsync();
    }
}