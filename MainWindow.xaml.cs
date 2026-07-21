using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMCA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsTickerRunning { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
            SetbtnTickerPlayStopContent();
        }
        private void MediaDropArea_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void MediaDropArea_Drop(object sender, DragEventArgs e)
        {
        }

        private void BrowseMedia_Click(object sender, RoutedEventArgs e)
        {
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnTickerPlayStop_Click(object sender, RoutedEventArgs e)
        {
            IsTickerRunning = !IsTickerRunning;
            SetbtnTickerPlayStopContent();
        }

        //minimethods

        /// <summary>
        /// sets what is on button for running ticker after click
        /// </summary>
        private void SetbtnTickerPlayStopContent()
        {
            if (IsTickerRunning)
            {
                btnTickerPlayStop.Content = "⭕";
            }
            else
            {
                btnTickerPlayStop.Content = "❌";
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl tabControl &&
        tabControl.SelectedItem is TabItem selectedTab &&
        selectedTab.Header?.ToString() == "About/Help") 
            {
                webReadme.Navigate("https://github.com/Estlib/SMCA/blob/main/README.md");
            }
        }
    }
}