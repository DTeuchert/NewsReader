using System.Windows;
using NewsReader.Models;

namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für RssLinkRemoveWindow.xaml
    /// </summary>
    public partial class RssLinkEditWindow
    {
        public RssLink EditLink { get; set; }

        public RssLinkEditWindow()
        {
            InitializeComponent();
            DataContext = this;
            TbRssTitle.Focus();
        }

        private void btnDialogEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TbRssLink.Text.Equals("") ||
                (TbRssTitle.Text.Equals(EditLink.Title) && TbRssLink.Text.Equals(EditLink.Link))) return;
            EditLink.Title = TbRssTitle.Text;
            EditLink.Link = TbRssLink.Text;

            DialogResult = true;
            Close();
        }
    }
}
