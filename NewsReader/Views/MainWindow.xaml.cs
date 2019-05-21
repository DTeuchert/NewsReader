namespace NewsReader.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ViewModels.MainWindowViewModel _mainViewModel = new ViewModels.MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _mainViewModel;
        }
    }
}
