using flashquizz.Services;
using System.Collections.ObjectModel;

namespace flashquizz.Pages.Deck;

public partial class DeckGestion : ContentPage
{
    private readonly DeckService _deckService;
    private readonly DeckAdd _deckAdd;

    public DeckGestion(DeckAdd deckAdd, DeckService deckService)
    {        
        _deckAdd = deckAdd;
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
    private async void OnClickedAddDeck(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_deckAdd);
    }
    
    public ObservableCollection<Models.Deck> Decks => _deckService.Decks;

}
