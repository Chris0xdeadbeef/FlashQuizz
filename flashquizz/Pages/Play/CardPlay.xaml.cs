using System.Diagnostics;

namespace flashquizz.Pages.Play;

/// <summary>
/// Page principale du mode "Play".
/// Gère l'affichage des cartes, les animations, la progression,
/// le comptage des réussites, et la détection du shake pour les erreurs.
/// </summary>
public partial class CardPlay : ContentPage
{
    // --- Données principales ---
    private readonly Stopwatch _timer = new();                     // Chronomètre de la session
    private readonly Models.Deck _deck;                            // Deck en cours
    private Models.Card? _currentCard;                             // Carte actuellement affichée
    private readonly Dictionary<Models.Card, int> _successCount = []; // Réussites par carte
    private readonly int _connaissanceRequired;                    // Nb de réussites nécessaires

    // --- État d'affichage ---
    private bool _showingQuestion = true;                          // Face visible : question ou réponse
    private bool _isAnimating = false;                             // Empêche les doubles clics pendant l'animation

    // --- Images de cartes ---
    private readonly List<(string front, string back)> _cardImages =
    [
        ("card1.jpg", "card2.jpg"),
        ("card3.jpg", "card4.jpg"),
        ("card5.jpg", "card6.jpg"),
        ("card7.jpg", "card8.jpg"),
        ("card9.jpg", "card10.jpg"),
        ("card11.jpg", "card12.jpg"),
        ("card13.jpg", "card14.jpg"),
        ("card15.jpg", "card16.jpg"),
        ("card17.jpg", "card18.jpg"),
        ("card19.jpg", "card20.jpg")
    ];

    private (string front, string back) _currentImages;

    // --- Propriétés utiles ---
    public int TotalCards => _deck.Cards.Count;
    public int SuccessCount { get; set; } = 0;
    public int FailCount { get; set; } = 0;

    /// <summary>
    /// Constructeur principal.
    /// Initialise la session, démarre le timer, charge la première carte
    /// et active l'accéléromètre.
    /// </summary>
    public CardPlay(Models.Deck deck, int connaissanceRequired)
    {
        InitializeComponent();

        _deck = deck;
        _connaissanceRequired = connaissanceRequired;

        _timer.Start();
        LoadNextCard();
        UpdateProgress();

        BindingContext = this;
        Title = _deck.Title;

        // Activation du shake
        Accelerometer.ShakeDetected += OnShakeDetected;
        Accelerometer.Start(SensorSpeed.Game);
    }

    /// <summary>
    /// Retourne à l'écran de fin de partie avec les statistiques.
    /// </summary>
    private async void OnBackClicked(object sender, EventArgs e)
    {
        _timer.Stop();
        await Navigation.PushAsync(new EndGameStats(_deck, _successCount, _timer.Elapsed, _connaissanceRequired));
    }

    /// <summary>
    /// Gère le flip de la carte (question - réponse) avec animation.
    /// </summary>
    private async void OnCardTapped(object sender, TappedEventArgs e)
    {
        if (_isAnimating) return;
        _isAnimating = true;

        // Début du flip
        CardContainer.AnchorX = 0;
        await CardContainer.RotateYTo(90, 200, Easing.SinIn);

        // Animation intermédiaire
        await Task.WhenAll(
            CardContainer.RotateYTo(90, 200, Easing.SinIn),
            CardContainer.ScaleTo(0.97, 200, Easing.SinIn),
            CardContainer.FadeTo(0.85, 200, Easing.SinIn)
        );

        // Changement du contenu
        if (_showingQuestion)
        {
            CardText.Text = _currentCard?.Answer;
            CardImage.Source = _currentImages.back;
        }
        else
        {
            CardText.Text = _currentCard?.Question;
            CardImage.Source = _currentImages.front;
        }

        _showingQuestion = !_showingQuestion;

        // Fin du flip
        await Task.WhenAll(
            CardContainer.RotateYTo(0, 220, Easing.SinOut),
            CardContainer.ScaleTo(1, 220, Easing.SinOut),
            CardContainer.FadeTo(1, 220, Easing.SinOut)
        );

        _isAnimating = false;
    }

    /// <summary>
    /// Charge une nouvelle carte aléatoire et un nouveau set d'images.
    /// </summary>
    private void LoadNextCard()
    {
        var random = new Random();

        // Sélection d'une carte
        _currentCard = _deck.Cards[random.Next(_deck.Cards.Count)];

        // Sélection d'un set d'images différent du précédent
        (string front, string back) newImages;
        do
        {
            newImages = _cardImages[random.Next(_cardImages.Count)];
        }
        while (newImages.front == _currentImages.front &&
               newImages.back == _currentImages.back);

        _currentImages = newImages;

        // Affichage de la face avant
        CardText.Text = _currentCard.Question;
        CardImage.Source = _currentImages.front;

        _showingQuestion = true;

        UpdateProgress();
    }

    /// <summary>
    /// Appelé lorsque l'utilisateur clique sur "Réussi".
    /// Incrémente la progression de la carte.
    /// </summary>
    private void OnSuccessClicked(object sender, EventArgs e)
    {
        if (_currentCard is null)
            return;

        if (!_successCount.ContainsKey(_currentCard))
            _successCount[_currentCard] = 0;

        // Incrément limité au seuil requis
        _successCount[_currentCard] = Math.Min(
            _successCount[_currentCard] + 1,
            _connaissanceRequired
        );

        CheckIfGameFinished();
        LoadNextCard();
    }

    /// <summary>
    /// Nettoyage : désactive l'accéléromètre pour éviter les fuites d'événements.
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Accelerometer.ShakeDetected -= OnShakeDetected;
        Accelerometer.Stop();
    }

    /// <summary>
    /// Déclenché lorsqu'un shake est détecté.
    /// Retire 1 point à la carte actuelle (sans descendre sous 0).
    /// Doit être exécuté sur le MainThread car il modifie l'UI.
    /// </summary>
    private void OnShakeDetected(object? sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (_currentCard is null)
                return;

            if (!_successCount.ContainsKey(_currentCard))
            {
                _successCount[_currentCard] = 0;
            }
            else
            {
                _successCount[_currentCard] = Math.Max(0, _successCount[_currentCard] - 1);
            }

            CheckIfGameFinished();
            LoadNextCard();
        });
    }

    /// <summary>
    /// Met à jour la barre de progression en fonction du nombre de cartes maîtrisées.
    /// </summary>
    private void UpdateProgress()
    {
        int mastered = _successCount.Values.Count(v => v >= _connaissanceRequired);
        double ratio = (double)mastered / TotalCards;

        // Correction visuelle
        double correctedRatio = ratio * ((250.0 - 52.0) / 250.0);

        ProgressViewport.ScaleX = correctedRatio;
    }

    /// <summary>
    /// Animation continue du fond de progression (effet de bande défilante).
    /// </summary>
    private async void AnimateFill()
    {
        double speed = 80;
        double tileWidth = 250;
        double position = 0;

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        while (true)
        {
            double dt = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();

            position += speed * dt;
            position %= tileWidth;

            FillBand.TranslationX = -position;

            await Task.Delay(16);
        }
    }

    /// <summary>
    /// Démarre l'animation de fond lorsque la page apparaît.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        AnimateFill();
    }

    /// <summary>
    /// Vérifie si toutes les cartes sont maîtrisées.
    /// Si oui, affiche l'écran de fin de partie.
    /// </summary>
    private async void CheckIfGameFinished()
    {
        int mastered = _successCount.Values.Count(v => v >= _connaissanceRequired);

        if (mastered == TotalCards)
        {
            _timer.Stop();
            await Navigation.PushAsync(new EndGameStats(_deck, _successCount, _timer.Elapsed, _connaissanceRequired));
        }
    }
}
