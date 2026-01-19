namespace flashquizz.Pages.Play;

public partial class DeckChoice : ContentPage
{
	public DeckChoice()
	{
        InitializeComponent();
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Revenir à la page précédente
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
}