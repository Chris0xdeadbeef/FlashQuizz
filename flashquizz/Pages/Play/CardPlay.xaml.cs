namespace flashquizz.Pages.Play;

public partial class CardPlay : ContentPage
{
	public CardPlay()
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