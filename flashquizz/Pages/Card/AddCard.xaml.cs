using flashquizz.Services;

namespace flashquizz.Pages.Card;

public partial class AddCard : ContentPage
{
    private readonly DeckService _deckService;
    private readonly Models.Deck _deck;

    private Models.Card? _editingCard = null;

    /// <summary>
    /// Mode création : ajouter une nouvelle carte.
    /// </summary>
    public AddCard(Models.Deck deck, DeckService deckService)
    {
        _deck = deck;
        _deckService = deckService;

        InitializeComponent();

        // Mode création
        CreateButton.IsVisible = true;
        EditButton.IsVisible = false;
    }

    /// <summary>
    /// Mode modification : modifier une carte existante.
    /// </summary>
    public AddCard(Models.Deck deck, DeckService deckService, Models.Card cardToEdit)
    {
        _deck = deck;
        _deckService = deckService;
        _editingCard = cardToEdit;

        InitializeComponent();

        // Pré-remplir les champs
        QuestionEntry.Text = cardToEdit.Question;
        AnswerEntry.Text = cardToEdit.Answer;

        // Mode édition
        CreateButton.IsVisible = false;
        EditButton.IsVisible = true;
        TitleLabel.Text = "Modifier la carte";
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    /// <summary>
    /// Crée une nouvelle carte.
    /// </summary>
    private async void OnCreateCardClicked(object sender, EventArgs e)
    {
        string question = QuestionEntry.Text?.Trim() ?? "";
        string answer = AnswerEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(answer))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        Models.Card newCard = new()
        {
            Question = question,
            Answer = answer
        };

        _deckService.AddCard(_deck, newCard);

        await Navigation.PopAsync();
    }

    /// <summary>
    /// Modifie la carte existante.
    /// </summary>
    private async void OnEditCardClicked(object sender, EventArgs e)
    {
        if (_editingCard == null)
            return;

        string question = QuestionEntry.Text?.Trim() ?? "";
        string answer = AnswerEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(answer))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs.", "OK");
            return;
        }

        _editingCard.Question = question;
        _editingCard.Answer = answer;

        await Navigation.PopAsync();
    }
    private void AnimateWidth(BoxView target, double from, double to)
    {
        var animation = new Animation(v =>
        {
            target.WidthRequest = v;
        }, from, to);

        animation.Commit(this, "UnderlineAnimation", 16, 250, Easing.CubicOut);
    }

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender == QuestionEntry)
            AnimateWidth(QuestionUnderline, QuestionUnderline.WidthRequest, 300);
        else if (sender == AnswerEntry)
            AnimateWidth(AnswerUnderline, AnswerUnderline.WidthRequest, 300);
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (sender == QuestionEntry)
            AnimateWidth(QuestionUnderline, QuestionUnderline.WidthRequest, 100);
        else if (sender == AnswerEntry)
            AnimateWidth(AnswerUnderline, AnswerUnderline.WidthRequest, 100);
    }
    private void OnBackgroundTapped(object sender, TappedEventArgs e)
    {
        // Enlève le focus des champs
        QuestionEntry.Unfocus();
        AnswerEntry.Unfocus();
    }
}
