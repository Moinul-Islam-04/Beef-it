namespace Beef__it
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Register your routes
            Routing.RegisterRoute("landing", typeof(Pages.LandingPage));
            Routing.RegisterRoute("main", typeof(Pages.MainPage));
            Routing.RegisterRoute("placeholder", typeof(Pages.PlaceholderPage));
            Routing.RegisterRoute("progress", typeof(Pages.ProgressPicPage));
            Routing.RegisterRoute("workout", typeof(Pages.WorkoutEntryPage));

            // Set your main page
            MainPage = new AppShell();
        }
    }
}
