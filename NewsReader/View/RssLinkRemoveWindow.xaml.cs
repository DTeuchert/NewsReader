using System.Windows;
using NewsReader.Util;
using NewsReader.Model;

namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für AddRssLinkWindow.xaml
    /// </summary>
    public partial class RssLinkRemoveWindow
    {
        public RSSLink RemoveLink { get; set; }
        public RSSLinkCollection RSSLinkList { get; set; }

        public RssLinkRemoveWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnDialogRemove_Click(object sender, RoutedEventArgs e)
        {
            RSSLinkList.Remove(RemoveLink);

            DialogResult = true;
            Close();
        }
    }
}
