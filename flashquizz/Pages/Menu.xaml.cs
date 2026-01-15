namespace flashquizz.Pages;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}

    private async void OnClickedGestionDeck(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Menu());
    }

    private async void OnClickedPlay(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Menu());
    }
}