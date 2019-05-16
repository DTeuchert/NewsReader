using System.Windows;
using NewsReader.Util;
using NewsReader.Models;

namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für RssLinkAddWindow.xaml
    /// </summary>
    public partial class RssLinkAddWindow
    {
        public RssLinkCollection RSSLinkList { get; set; }

        public RssLinkAddWindow()
        {
            InitializeComponent(); 
            TbRssTitle.Focus();
        }

        private void btnDialogAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbRssLink.Text.Equals("")) return;
            RSSLinkList.Add(new RssLink { 
                Title = TbRssTitle.Text,
                Link = TbRssLink.Text 
            });
            
            DialogResult = true;
            Close();
        }
    }
}
