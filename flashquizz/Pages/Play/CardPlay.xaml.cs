using System.Diagnostics;

namespace flashquizz.Pages.Play;

public partial class CardPlay : ContentPage
{
    private readonly Stopwatch _timer = new();
    private readonly Models.Deck _deck;
    private bool _showingQuestion = true;
    private bool _isAnimating = false;
    private Models.Card? _currentCard;
    private readonly Dictionary<Models.Card, int> _successCount = [];
    private readonly int _connaissanceRequired;
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
    public int TotalCards => _deck.Cards.Count;
    public int SuccessCount { get; set; } = 0;
    public int FailCount { get; set; } = 0;

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

        Accelerometer.ShakeDetected += OnShakeDetected;
        Accelerometer.Start(SensorSpeed.Game);

    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        _timer.Stop();
        await Navigation.PushAsync(new EndGameStats(_deck, _successCount, _timer.Elapsed, _connaissanceRequired));
    }


    private async void OnCardTapped(object sender, TappedEventArgs e)
    {
        if (_isAnimating) return;
        _isAnimating = true;

        CardContainer.AnchorX = 0; // flip autour du côté gauche
        await CardContainer.RotateYTo(90, 200, Easing.SinIn);

        // Première moitié du flip
        await Task.WhenAll(
            CardContainer.RotateYTo(90, 200, Easing.SinIn),
            CardContainer.ScaleTo(0.97, 200, Easing.SinIn),
            CardContainer.FadeTo(0.85, 200, Easing.SinIn)
        );

        // Changement du contenu au "dos"
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

        // Deuxième moitié du flip
        await Task.WhenAll(
            CardContainer.RotateYTo(0, 220, Easing.SinOut),
            CardContainer.ScaleTo(1, 220, Easing.SinOut),
            CardContainer.FadeTo(1, 220, Easing.SinOut)
        );

        _isAnimating = false;
    }

    private void LoadNextCard()
    {
        var random = new Random();

        // Choisir une carte aléatoire
        _currentCard = _deck.Cards[random.Next(_deck.Cards.Count)];

        // Choisir un set d’images différent du précédent
        (string front, string back) newImages;
        do
        {
            newImages = _cardImages[random.Next(_cardImages.Count)];
        }
        while (newImages.front == _currentImages.front &&
               newImages.back == _currentImages.back);

        _currentImages = newImages;

        // Afficher la face avant
        CardText.Text = _currentCard.Question;
        CardImage.Source = _currentImages.front;

        _showingQuestion = true;

        UpdateProgress();
    }

    private void OnSuccessClicked(object sender, EventArgs e)
    {
        if (_currentCard is null)
            return;

        if (!_successCount.ContainsKey(_currentCard))
            _successCount[_currentCard] = 0;

        // ne dépasse jamais _connaissanceRequired
        _successCount[_currentCard] = Math.Min(
            _successCount[_currentCard] + 1,
            _connaissanceRequired
        );

        CheckIfGameFinished();

        LoadNextCard();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Accelerometer.ShakeDetected -= OnShakeDetected;
        Accelerometer.Stop();
    }

    private void OnShakeDetected(object? sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (_currentCard is null)
                return;

            // Si la carte n’a jamais été réussie → reste à 0
            if (!_successCount.ContainsKey(_currentCard))
            {
                _successCount[_currentCard] = 0;
            }
            else
            {
                // Si elle a déjà été réussie → on retire 1 (sans descendre sous 0)
                _successCount[_currentCard] = Math.Max(0, _successCount[_currentCard] - 1);
            }

            CheckIfGameFinished();

            LoadNextCard();
        });
    }

    private void UpdateProgress()
    {
        int mastered = _successCount.Values.Count(v => v >= _connaissanceRequired);
        double ratio = (double)mastered / TotalCards;

        // Compense la marge visuelle de 52px
        double correctedRatio = ratio * ((250.0 - 52.0) / 250.0);

        ProgressViewport.ScaleX = correctedRatio;
    }

    private async void AnimateFill()
    {
        double speed = 80; // px/sec
        double tileWidth = 250; // largeur d’un motif
        double position = 0;

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        while (true)
        {
            double dt = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();

            position += speed * dt;

            // boucle parfaite sans trou
            position %= tileWidth;

            FillBand.TranslationX = -position;

            await Task.Delay(16);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AnimateFill();
    }

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
