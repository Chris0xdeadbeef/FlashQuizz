namespace flashquizz.Pages;

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
}
