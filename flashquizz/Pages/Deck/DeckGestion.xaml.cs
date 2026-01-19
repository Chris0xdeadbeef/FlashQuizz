namespace flashquizz.Pages.Deck;

public partial class DeckGestion : ContentPage
{
    public DeckGestion()
    {
        InitializeComponent();
    }
    
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Revenir à la page précédente
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
    private async void OnClickedAddDeck(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeckAdd());
    }
}
