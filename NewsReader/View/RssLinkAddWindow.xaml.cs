using System.Windows;
using NewsReader.Util;
using NewsReader.Model;

namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für RssLinkAddWindow.xaml
    /// </summary>
    public partial class RssLinkAddWindow
    {
        public RSSLinkCollection RSSLinkList { get; set; }

        public RssLinkAddWindow()
        {
            InitializeComponent(); 
            newLinkTextBox_Titel.Focus();
        }

        private void btnDialogAdd_Click(object sender, RoutedEventArgs e)
        {
            if (newLinkTextBox_Link.Text.Equals("")) return;
            RSSLinkList.Add(new RSSLink { 
                Title = newLinkTextBox_Titel.Text,
                Link = newLinkTextBox_Link.Text 
            });
            
            DialogResult = true;
            Close();
        }
    }
}
