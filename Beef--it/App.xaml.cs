namespace Beef__it
{
    public partial class App : Application
    {
        // This property holds the service provider.
        public IServiceProvider Services { get; }

        // Override Current to return an instance of your App.
        public static new App Current => (App)Application.Current;

        public App(IServiceProvider services)
        {
            InitializeComponent();
            Services = services;
            MainPage = new NavigationPage(new LandingPage());
        }
    }
}
