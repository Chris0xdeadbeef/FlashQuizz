using System.Diagnostics;

namespace flashquizz.Pages.Play;

public partial class CardPlay : ContentPage
{
    private readonly Models.Deck _deck;
    private bool _showingQuestion = true;
    private bool _isAnimating = false;
    private Models.Card? _currentCard;
    private readonly Dictionary<Models.Card, int> _successCount = new();
    private readonly int _connaissanceRequired;


    public int TotalCards => _deck.Cards.Count;
    public int SuccessCount { get; set; } = 0;
    public int FailCount { get; set; } = 0;

    public CardPlay(Models.Deck deck, int connaissanceRequired)
    {
        InitializeComponent();
        _deck = deck;
        _connaissanceRequired = connaissanceRequired;

        LoadNextCard();
        UpdateProgress();

        BindingContext = this;
        Title = _deck.Title;

        Accelerometer.ShakeDetected += OnShakeDetected;
        Accelerometer.Start(SensorSpeed.Game);

    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
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
            CardImage.Source = "card2.jpg";
        }
        else
        {
            CardText.Text = _currentCard?.Question;
            CardImage.Source = "card1.jpg";
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
        // Choisir une carte aléatoire dans le deck
        var random = new Random();
        _currentCard = _deck.Cards[random.Next(_deck.Cards.Count)];

        CardText.Text = _currentCard.Question;
        _showingQuestion = true;

        UpdateProgress();
    }


    private void OnSuccessClicked(object sender, EventArgs e)
    {
        if (_currentCard is null) 
            return;

        if (!_successCount.ContainsKey(_currentCard))
            _successCount[_currentCard] = 0;

        _successCount[_currentCard]++;

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


}
