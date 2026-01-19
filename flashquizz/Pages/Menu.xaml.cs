namespace flashquizz.Pages;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
        
	}

    private async void OnClickedGestionDeck(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeckGestion());

    }

    private async void OnClickedPlay(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("play");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Vérifie si on peut revenir en arrière
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
}