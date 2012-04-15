using Windows.Data.Xml.Dom;
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
            var elements = tileXml.GetElementsByTagName("text");
            elements[0].AppendChild(tileXml.CreateTextNode("Hi :-)"));

            // Create a TileNotification from our XML, and send it to the Tile update manager
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        /// <summary>
        /// Set the application's tile to display a local image file.
        /// After clicking, go back to the start screen to watch your application's tile update.
        /// </summary>
        private void SetTileImageButtonClick(object sender, RoutedEventArgs e)
        {
            // It is possible to start from an existing template and modify what is needed.
            // Alternatively you can construct the XML from scratch.
            var tileXml = new XmlDocument();
            var title = tileXml.CreateElement("title");
            var visual = tileXml.CreateElement("visual");
            visual.SetAttribute("version", "1");
            visual.SetAttribute("lang", "en-US");

            // The template is set to be a TileSquareImage. This tells the tile update manager what to expect next.
            var binding = tileXml.CreateElement("binding");
            binding.SetAttribute("template", "TileSquareImage");
            // An image element is then created under the TileSquareImage XML node. The path to the image is specified
            var image = tileXml.CreateElement("image");
            image.SetAttribute("id", "1");
            image.SetAttribute("src", @"ms-appx:///Assets/DemoImage.png");

            // All the XML elements are chained up together.
            title.AppendChild(visual);
            visual.AppendChild(binding);
            binding.AppendChild(image);
            tileXml.AppendChild(title);

            // The XML is used to create a new TileNotification which is then sent to the TileUpdateManager
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        /// <summary>
        /// Set the application's tile to display a remote image on the internet.
        /// After clicking, go back to the start screen to watch your application's tile update.
        /// </summary>
        private void SetTileRemoteImageButtonClick(object sender, RoutedEventArgs e)
        {
            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareImage);

            // Find the 'image' element in the template's XML. Change the src attribute to a remote URL
            var elements = tileXml.GetElementsByTagName("image");
            var imageElement = elements[0] as XmlElement;
            imageElement.SetAttribute("src", @"http://i.microsoft.com/global/en-us/homepage/PublishingImages/sprites/microsoft.png");

            // Create a TileNotification from our XML, and send it to the Tile update manager
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
