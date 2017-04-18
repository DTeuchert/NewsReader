using System.Windows;
using NewsReader.Util;
using NewsReader.Model;

namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für RssLinkRemoveWindow.xaml
    /// </summary>
    public partial class RssLinkEditWindow
    {
        public RSSLink EditLink { get; set; }

        public RssLinkEditWindow()
        {
            InitializeComponent();
            DataContext = this;
            editLinkTextBox_Title.Focus();
        }

        private void btnDialogEdit_Click(object sender, RoutedEventArgs e)
        {
            if (editLinkTextBox_Link.Text.Equals("") || 
                (editLinkTextBox_Title.Text.Equals(EditLink.Title) && editLinkTextBox_Link.Text.Equals(EditLink.Link))) return;
            EditLink.Title = editLinkTextBox_Title.Text;
            EditLink.Link = editLinkTextBox_Link.Text;

            DialogResult = true;
            Close();
        }
    }
}
