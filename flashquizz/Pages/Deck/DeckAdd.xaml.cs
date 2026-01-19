namespace flashquizz.Pages.Deck;

public partial class DeckAdd : ContentPage
{
	public DeckAdd()
	{
		InitializeComponent();
	}
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // revenir en arrière
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
}