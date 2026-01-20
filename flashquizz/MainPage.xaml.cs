
using flashquizz.Pages;

namespace flashquizz
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly Menu _menu;

        public MainPage(Menu menu)
        {
            InitializeComponent();
            _menu = menu;
        }


        private async void OnMenuClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(_menu);
        }


        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
