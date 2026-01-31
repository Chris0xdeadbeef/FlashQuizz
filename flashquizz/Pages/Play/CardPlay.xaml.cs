namespace flashquizz.Pages.Play;

public partial class CardPlay : ContentPage
{
    private readonly Models.Deck _deck;
    private bool _showingQuestion = true;
    private Models.Card _currentCard;
    public CardPlay(Models.Deck deck)
    {
        InitializeComponent();
        _deck = deck;
        _currentCard = _deck.Cards.First();
        CardText.Text = _currentCard.Question;

        Title = _deck.Title;
        BindingContext = this;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    private async void OnCardTapped(object sender, TappedEventArgs e)
    {
        // Animation flip 1 : disparaître
        await CardContainer.RotateYTo(90, 150, Easing.CubicIn);

        // Changer le texte
        if (_showingQuestion)
            CardText.Text = _currentCard.Answer;
        else
            CardText.Text = _currentCard.Question;

        _showingQuestion = !_showingQuestion;

        // Animation flip 2 : réapparaître
        await CardContainer.RotateYTo(0, 150, Easing.CubicOut);
    }
}
