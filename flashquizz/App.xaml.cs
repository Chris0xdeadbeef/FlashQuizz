using flashquizz.Pages;

namespace flashquizz
{
    public partial class App : Application
    {
        public App(Menu menu)
        {
            InitializeComponent();

            MainPage = new SplashScreen(menu);
        }
    }

}
