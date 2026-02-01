using System.Diagnostics;

namespace flashquizz.Pages.Play;

public partial class CardPlay : ContentPage
{
    private readonly Models.Deck _deck;
    private bool _showingQuestion = true;

    private Models.Card? _currentCard;
    private readonly List<Models.Card> _remainingCards;
    private int _currentIndex = 0;

    public int TotalCards => _deck.Cards.Count;
    public int SuccessCount { get; set; } = 0;
    public int FailCount { get; set; } = 0;

    public CardPlay(Models.Deck deck)
    {
        InitializeComponent();
        _deck = deck;

        _remainingCards = _deck.Cards.OrderBy(c => Guid.NewGuid()).ToList();

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
        await CardContainer.RotateYTo(90, 150, Easing.CubicIn);

        CardText.Text = _showingQuestion ? _currentCard?.Answer : _currentCard?.Question;
        _showingQuestion = !_showingQuestion;

        await CardContainer.RotateYTo(0, 150, Easing.CubicOut);
    }

    private void LoadNextCard()
    {
        if (_currentIndex >= _remainingCards.Count)
        {
            DisplayAlert("Terminé",
                $"Réussi : {SuccessCount}\nRaté : {FailCount}",
                "OK");

            Navigation.PopAsync();
            return;
        }

        _currentCard = _remainingCards[_currentIndex];
        CardText.Text = _currentCard.Question;
        _showingQuestion = true;

        UpdateProgress();
    }

    private void OnSuccessClicked(object sender, EventArgs e)
    {
        SuccessCount++;
        _currentIndex++;
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
            FailCount++;
            _currentIndex++;
            LoadNextCard();
        });
    }

    private void UpdateProgress()
    {
        double ratio = Math.Clamp((double)_currentIndex / TotalCards, 0, 1);
        ProgressViewport.ScaleX = ratio;
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
