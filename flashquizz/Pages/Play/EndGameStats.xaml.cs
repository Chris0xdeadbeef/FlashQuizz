namespace flashquizz.Pages.Play;

public partial class EndGameStats : ContentPage
{
    private readonly Models.Deck _deck;
    private readonly Dictionary<Models.Card, int> _successCount;
    private readonly TimeSpan _elapsed;
    private readonly int _connaissanceRequired;

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
    private void SetRandomBackground()
    {
        var images = new[] { "end.jpg", "end1.jpg", "end2.jpg", "wa.png" };
        Random random = new();

        BackgroundImage.Source = images[random.Next(images.Length)];
    }


    private void LoadStats()
    {
        // 1. Temps passé
        TimeSpentLabel.Text = $"{_elapsed:mm\\:ss}";

        // 2. Carte la plus difficile = celle avec le moins de réussite
        // (si jamais une carte n’a jamais été vue, on lui met 0)
        if (_successCount.Count == 0)
        {
            HardestCardLabel.Text = "Aucune carte jouée";
        }
        else
        {
            var hardest = _successCount.OrderBy(kv => kv.Value).First().Key;
            HardestCardLabel.Text = hardest.Question;
        }

        // 3. Nombre de cartes maitrisées à 100%
        int mastered = _successCount.Values.Count(v => v >= _connaissanceRequired);
        MasteredCountLabel.Text = mastered.ToString();

        // 4. Pourcentage de mémorisation
        double percent = (double)mastered / _deck.Cards.Count * 100.0;
        MemorizationPercentLabel.Text = $"{percent:0}%";
    }


    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
