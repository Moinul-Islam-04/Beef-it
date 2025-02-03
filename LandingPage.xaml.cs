using Microsoft.Maui.Controls;

namespace BeefIt
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage(string userName)
        {
            InitializeComponent();
            this.FindByName<Label>("TitleLabel").Text = $"Hello {userName}";
        }
    }
}
