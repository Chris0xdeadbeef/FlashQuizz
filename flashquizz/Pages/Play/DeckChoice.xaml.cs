using flashquizz.Services;
using System.Collections.ObjectModel;

namespace flashquizz.Pages.Play;

public partial class DeckChoice : ContentPage
{
    private readonly DeckService _deckService;
    public DeckChoice(DeckService deckService)
	{        
        _deckService = deckService;

        BindingContext = this;

        InitializeComponent();
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Revenir à la page précédente
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
    public ObservableCollection<Models.Deck> Decks => _deckService.Decks;
}