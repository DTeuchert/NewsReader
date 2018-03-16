namespace NewsReader.View
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly ViewModel.MainWindowViewModel mainViewModel = new ViewModel.MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
