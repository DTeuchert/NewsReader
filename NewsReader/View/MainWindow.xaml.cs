using NewsReader.Util;

namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            LanguageHandler.SetLanguageDictionary(Resources);
            InitializeComponent();
        }
    }
}
