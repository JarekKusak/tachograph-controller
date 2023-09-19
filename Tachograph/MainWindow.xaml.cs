using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tachograph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReadingInterface readingInterface;
        WritingInterface writingInterface;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += settingsBtn_Click;
        }

        protected async void readAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            readingInterface = new ReadingInterface("192.168.30.15", 5049, 5049);

            readAndSaveButton.IsEnabled = false; // znemožní opakované klikání na tlačítko
            progressBar.Visibility = Visibility.Visible; // Zobrazí ProgressBar
            await readingInterface.ReadAndSaveData(progressBar); // Spustíme čtení dat s ProgressBar
            progressBar.Visibility = Visibility.Hidden; // Skryje ProgressBar po dokončení
            readAndSaveButton.IsEnabled = true;
        }

        protected void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new SettingsPage();
        }

        private void signalsBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new SignalsPage();
        }
    }
}
