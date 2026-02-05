using flashquizz.Services;
using System.Collections.ObjectModel;

namespace flashquizz.Pages.Play;

/// <summary>
/// Page permettant au joueur de sélectionner un deck avant de lancer une session.
/// L'utilisateur doit obligatoirement renseigner un nombre dans "Connaissance"
/// pour pouvoir commencer une partie.
/// </summary>
public partial class DeckChoice : ContentPage
{
    private readonly DeckService _deckService;

    /// <summary>
    /// Liste observable des decks disponibles pour le mode "Play".
    /// </summary>
    public ObservableCollection<Models.Deck> Decks => _deckService.Decks;

    /// <summary>
    /// Indique si le champ Connaissance est vide ou égal à zéro.
    /// Utilisé pour valider la sélection d'un deck.
    /// </summary>
    public bool IsConnaissanceZero =>
        string.IsNullOrWhiteSpace(Connaissance.Text) || Connaissance.Text == "0";

    /// <summary>
    /// Constructeur principal de la page de choix des decks.
    /// Initialise le BindingContext et attache un listener pour détecter
    /// les changements dans le champ Connaissance.
    /// </summary>
    public DeckChoice(DeckService deckService)
    {
        _deckService = deckService;

        BindingContext = this;

        InitializeComponent();

        // Mise à jour automatique de IsConnaissanceZero lorsque l'utilisateur tape du texte
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
    /// Gère la sélection d'un deck dans la liste.
    /// Vérifie :
    /// 1) Que le champ Connaissance contient un nombre valide (> 0)
    /// 2) Que le deck n'est pas vide
    /// Si tout est correct, ouvre la page CardPlay.
    /// </summary>
    private void OnDeckSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Deck deck)
        {
            // 1) Vérifier que Connaissance est renseigné et valide
            if (!int.TryParse(Connaissance.Text, out int connaissance) || connaissance <= 0)
            {
                DisplayAlert("Erreur",
                    "Veuillez entrer un nombre valide dans le champ Connaissance.",
                    "OK");

                ((CollectionView)sender).SelectedItem = null;
                return;
            }

            // 2) Vérifier que le deck contient au moins une carte
            if (deck.Cards.Count == 0)
            {
                ((CollectionView)sender).SelectedItem = null;
                return;
            }

            // 3) Navigation vers la page de jeu
            Navigation.PushAsync(new CardPlay(deck, connaissance));
        }

        // Réinitialise la sélection visuelle dans la CollectionView
        ((CollectionView)sender).SelectedItem = null;
    }

    /// <summary>
    /// Permet de retirer le focus du champ Connaissance lorsque l'utilisateur
    /// clique en dehors.
    /// </summary>
    private void OnBackgroundTapped(object sender, TappedEventArgs e)
    {
        Connaissance.EntryControl.Unfocus();
    }
}
