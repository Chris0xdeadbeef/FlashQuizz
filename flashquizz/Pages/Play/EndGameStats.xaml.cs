namespace flashquizz.Pages.Play;

/// <summary>
/// Page d’affichage des statistiques de fin de partie.
/// Montre : temps total, carte la plus difficile, nombre de cartes maîtrisées,
/// et pourcentage global de mémorisation.
/// </summary>
public partial class EndGameStats : ContentPage
{
    private readonly Models.Deck _deck;
    private readonly Dictionary<Models.Card, int> _successCount;
    private readonly TimeSpan _elapsed;
    private readonly int _connaissanceRequired;

    /// <summary>
    /// Constructeur principal de la page de statistiques.
    /// </summary>
    /// <param name="deck">Le deck joué.</param>
    /// <param name="successCount">Dictionnaire des réussites par carte.</param>
    /// <param name="elapsed">Temps total de la session.</param>
    /// <param name="connaissanceRequired">Nombre de réussites nécessaires pour maîtriser une carte.</param>
    public EndGameStats(Models.Deck deck, Dictionary<Models.Card, int> successCount, TimeSpan elapsed, int connaissanceRequired)
    {
        InitializeComponent();
        SetRandomBackground();

        _deck = deck;
        _successCount = successCount;
        _elapsed = elapsed;
        _connaissanceRequired = connaissanceRequired;

        LoadStats();
    }

    /// <summary>
    /// Sélectionne aléatoirement une image de fond parmi une liste prédéfinie.
    /// </summary>
    private void SetRandomBackground()
    {
        string[] images = ["end.jpg", "end1.jpg", "end2.jpg", "wa.png"];
        Random random = new();

        BackgroundImage.Source = images[random.Next(images.Length)];
    }

    /// <summary>
    /// Charge et calcule toutes les statistiques affichées à l’écran :
    /// - Temps total
    /// - Carte la plus difficile
    /// - Nombre de cartes maîtrisées
    /// - Pourcentage global de mémorisation
    /// </summary>
    private void LoadStats()
    {
        // --- 1. Temps passé ---
        TimeSpentLabel.Text = $"{_elapsed:mm\\:ss}";

        // --- 2. Carte la plus difficile ---
        // Si aucune carte n’a été jouée (cas rare), on affiche un message.
        if (_successCount.Count == 0)
        {
            HardestCardLabel.Text = "Aucune carte jouée";
        }
        else
        {
            // On prend la carte avec le score le plus faible.
            var hardest = _successCount.OrderBy(kv => kv.Value).First().Key;
            HardestCardLabel.Text = hardest.Question;
        }

        // --- 3. Nombre de cartes maîtrisées ---
        int mastered = _successCount.Values.Count(v => v >= _connaissanceRequired);
        MasteredCountLabel.Text = mastered.ToString();

        // --- 4. Pourcentage global ---
        double percent = (double)mastered / _deck.Cards.Count * 100.0;
        MemorizationPercentLabel.Text = $"{percent:0}%";
    }

    /// <summary>
    /// Retourne à la page d’accueil lorsque l’utilisateur clique sur le bouton retour.
    /// </summary>
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
