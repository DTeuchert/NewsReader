using System.Windows;
using NewsReader.Models;

namespace NewsReader.Views
{
    /// <summary>
    /// Interaktionslogik für RssLinkAddWindow.xaml
    /// </summary>
    public partial class RssLinkAddWindow
    {
        public RssLinkCollection RssLinks { get; set; }

        public RssLinkAddWindow()
        {
            InitializeComponent();
            TbRssTitle.Focus();
        }

        private void btnDialogAdd_Click(object sender, RoutedEventArgs e)
        {
            if (TbRssLink.Text.Equals("")) return;
            RssLinks.Add(new RssLink
            {
                Title = TbRssTitle.Text,
                Link = TbRssLink.Text
            });

            DialogResult = true;
            Close();
        }
    }
}
