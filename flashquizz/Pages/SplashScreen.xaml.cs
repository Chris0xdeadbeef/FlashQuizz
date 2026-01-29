namespace flashquizz.Pages;

public partial class SplashScreen : ContentPage
{
    private readonly Menu _menu;

    public SplashScreen(Menu menu)
    {
        InitializeComponent();
        _menu = menu;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(5000);

        Application.Current.MainPage = new NavigationPage(_menu);
    }
}
