using flashquizz.Services;
using System.Collections.ObjectModel;

namespace flashquizz.Pages.Play;

public partial class DeckChoice : ContentPage
{
    private readonly DeckService _deckService;

    /// <summary>
    /// Liste observable des decks disponibles pour le mode "Play".
    /// </summary>
    public ObservableCollection<Models.Deck> Decks => _deckService.Decks;
    public bool IsConnaissanceZero => string.IsNullOrWhiteSpace(Connaissance.Text) || Connaissance.Text == "0";


    /// <summary>
    /// Initialise la page permettant de choisir un deck pour jouer.
    /// </summary>
    public DeckChoice(DeckService deckService)
    {
        _deckService = deckService;

        BindingContext = this;

        InitializeComponent();
        Connaissance.EntryControl.TextChanged += (s, e) =>
        {
            OnPropertyChanged(nameof(IsConnaissanceZero));
        };
    }

    /// <summary>
    /// Retourne à la page précédente si possible.
    /// </summary>
    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    /// <summary>
    /// Ouvre la page pour jouer le deck sélectionné
    /// </summary>
    private void OnDeckSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Deck deck)
        {
            // 1) Vérifier que Connaissance est renseigné
            if (!int.TryParse(Connaissance.Text, out int connaissance) || connaissance <= 0)
            {
                DisplayAlert("Erreur", "Veuillez entrer un nombre valide dans le champ Connaissance.", "OK");
                ((CollectionView)sender).SelectedItem = null;
                return;
            }

            // 2) Vérifier que le deck n'est pas vide
            if (deck.Cards.Count == 0)
            {
                ((CollectionView)sender).SelectedItem = null;
                return;
            }

            // 3) Navigation OK
            Navigation.PushAsync(new CardPlay(deck, connaissance));
        }

        ((CollectionView)sender).SelectedItem = null;
    }


    private void OnBackgroundTapped(object sender, TappedEventArgs e)
    {
        Connaissance.EntryControl.Unfocus();
    }
}
