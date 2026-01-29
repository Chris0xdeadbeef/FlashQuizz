namespace flashquizz.Pages.Play;

public partial class CardPlay : ContentPage
{
    private readonly Models.Deck _deck;

    public CardPlay(Models.Deck deck)
    {
        InitializeComponent();
        _deck = deck;              

        BindingContext = this;   

        Title = _deck.Title;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
}
