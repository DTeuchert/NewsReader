using System.Windows.Controls;

namespace NewsReader.Views
{
    /// <summary>
    /// Interaktionslogik für ConfigurationControl.xaml
    /// </summary>
    public partial class ConfigurationControl
    {
        public ConfigurationControl()
        {
            InitializeComponent();
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Services.LocalizationService.SetLanguage(((ComboBoxItem)e.AddedItems[0]).Uid);
        }
    }
}
