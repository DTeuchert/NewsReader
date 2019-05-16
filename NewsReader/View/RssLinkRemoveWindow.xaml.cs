using System.Windows;
using NewsReader.Models;

namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für AddRssLinkWindow.xaml
    /// </summary>
    public partial class RssLinkRemoveWindow
    {
        public RssLink RemoveLink { get; set; }
        public RssLinkCollection RssLinks { get; set; }

        public RssLinkRemoveWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnDialogRemove_Click(object sender, RoutedEventArgs e)
        {
            RssLinks.Remove(RemoveLink);

            DialogResult = true;
            Close();
        }
    }
}
