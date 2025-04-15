using Microsoft.Maui.Controls;

namespace Beef__it
{
    public partial class FullScreenImagePage : ContentPage
    {
        public FullScreenImagePage(string imagePath)
        {
            InitializeComponent();

            // Removes query string e.g. "?t=123456"
            string filePath = imagePath?.Split('?')[0] ?? string.Empty;

            // Sets image source from FullScreenImage field for page
            FullScreenImage.Source = ImageSource.FromFile(filePath);
        }

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Closes the page
        }
    }
}
