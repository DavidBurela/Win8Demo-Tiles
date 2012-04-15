using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Win8Demo_Tiles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage : Page
    {
        public BlankPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Set the application's tile to display text content.
        /// After clicking, go back to the start screen to watch your application's tile update.
        /// </summary>
        private void SetTileTextButtonClick(object sender, RoutedEventArgs e)
        {
            // Tiles use a predefined set of standard templates to display their content.
            // The updates happen by sending a XML fragment to the Tile update manager.
            // To make things easier, we will get the template for a square tile with text as a base, and modify it from there
            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText01);

            // Find the 'text' element in the template's XML, and insert the text "Hi :-)" into it.
            var tileAttributes = tileXml.GetElementsByTagName("text");
            tileAttributes[0].AppendChild(tileXml.CreateTextNode("Hi :-)"));

            // Create a TileNotification from our XML, and send it to the Tile update manager
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
