using System.Windows;
using NewsReader.Models;

namespace NewsReader.Views
{
    /// <summary>
    /// Interaktionslogik für AddRssLinkWindow.xaml
    /// </summary>
    public partial class RssLinkRemoveWindow
    {
        public RssLink RssLink { get; set; }
        public RssLinkCollection RssLinks { get; set; }

        public RssLinkRemoveWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnDialogRemove_Click(object sender, RoutedEventArgs e)
        {
            RssLinks.Remove(RssLink);

            DialogResult = true;
            Close();
        }
    }
}
