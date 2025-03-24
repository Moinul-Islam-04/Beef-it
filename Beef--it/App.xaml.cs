using Beef__it.Database;
using SQLite;

namespace Beef__it
{
    public partial class App : Application
    {
   
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        { 
            base.OnStart();
        }
    }
}