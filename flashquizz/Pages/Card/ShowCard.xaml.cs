namespace flashquizz.Pages.Card;

public partial class ShowCard : ContentPage
{
    private readonly Models.Deck _deck;
    private readonly AddCard _cardAdd;
    public string DeckTitle => _deck.Title;

    public ShowCard(AddCard addCard, Models.Deck deck)
	{
        _cardAdd = addCard;
        _deck = deck;

        BindingContext = _deck;

		InitializeComponent();
	}
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Revenir à la page précédente
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
    private async void OnClickedAddCard(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_cardAdd);
    }
}