using Beef__it.Database;
using SQLite;

namespace Beef__it
{
    //public partial class App : Application
    //{

    //    public App()
    //    {
    //        InitializeComponent();

    //        MainPage = new NavigationPage(new LoginPage());
    //    }

    //    protected override void OnStart()
    //    { 
    //        base.OnStart();
    //    }
    //}
    public partial class App : Application
    {
        // This property holds the service provider.
        public IServiceProvider Services { get; }

        // Overrides Current to return an instance of the App.
        public static new App Current => (App)Application.Current; // Adding so we can access instance of App for access to services
                                                                   // Will appear in LandingPage as App.Current.services.
        public App(IServiceProvider services)
        {
            InitializeComponent();
            Services = services;
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}