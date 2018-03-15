using System.Windows.Controls;

namespace NewsReader.View
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
            Util.LocalizationService.SetLanguage(((ComboBoxItem)e.AddedItems[0]).Uid);
        }
    }
}
