using NewsReader.Util;
using System.Windows;

namespace NewsReader
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LocalizationService.SetLanguage(ConfigurationService.Language);
        }
    }
}
