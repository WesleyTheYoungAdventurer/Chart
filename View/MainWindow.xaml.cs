using Chart.ViewModel;
using System.Windows;

namespace Chart
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //[System.Obsolete]
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }
    }
}
