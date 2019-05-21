using System.Windows;
using NewsReader.Models;

namespace NewsReader.Views
{
    /// <summary>
    /// Interaktionslogik für RssLinkRemoveWindow.xaml
    /// </summary>
    public partial class RssLinkEditWindow
    {
        public RssLink RssLink { get; set; }

        public RssLinkEditWindow()
        {
            InitializeComponent();
            DataContext = this;
            TbRssTitle.Focus();
        }

        private void btnDialogEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TbRssLink.Text.Equals("") ||
                (TbRssTitle.Text.Equals(RssLink.Title) && TbRssLink.Text.Equals(RssLink.Link))) return;
            RssLink.Title = TbRssTitle.Text;
            RssLink.Link = TbRssLink.Text;

            DialogResult = true;
            Close();
        }
    }
}
