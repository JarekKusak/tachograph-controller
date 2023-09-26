using System;
using System.Windows;

namespace Tachograph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReadingInterface readingInterface;
        WritingInterface writingInterface;
        SettingsPage settingsPage;
        SignalsPage signalsPage;
        CommentPage commentPage;

        const string tachoIP = "192.168.30.15";
        const int sourcePort = 5049;
        const int destinationPort = sourcePort;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += settingsBtn_Click;

            settingsPage = new SettingsPage();
            signalsPage = new SignalsPage();
            commentPage = new CommentPage();
        }

        /// <summary>
        /// Událost po stisknutí tlačíka na čtení a uložení
        /// </summary>
        private async void readAndSaveButton_Click(object sender, RoutedEventArgs e)
        {
            readingInterface = new ReadingInterface(tachoIP, sourcePort, destinationPort);

            readAndSaveButton.IsEnabled = false; // znemožní opakované klikání na tlačítko
            progressBar.Visibility = Visibility.Visible; // Zobrazí ProgressBar
            await readingInterface.ReadAndSaveData(progressBar); // Spustíme čtení dat s ProgressBar
            progressBar.Visibility = Visibility.Hidden; // Skryje ProgressBar po dokončení
            readAndSaveButton.IsEnabled = true;
        }

        /// <summary>
        /// Generuje stránku s nastavením parametrů tachografu
        /// </summary>
        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = settingsPage;
        }

        /// <summary>
        /// Stránka na signály (ještě úplně nevím k čemu)
        /// </summary>
        private void signalsBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = signalsPage;
        }

        /// <summary>
        /// Stránka na poznámku (ještě úplně nevím k čemu)
        /// </summary>
        private void commentBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = commentPage;
        }

        /// <summary>
        /// Metoda na zápis dat do tachografu (při správném vyplnění parametrů)
        /// </summary>
        private async void setTachoParametersBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                writingInterface = new WritingInterface(tachoIP, sourcePort, destinationPort);

                TachographParameters tachographParameters = settingsPage.ReturnTachoParameters();
                CarParameters carParameters = settingsPage.ReturnCarParameters();
                CounterParameters counterParameters = settingsPage.ReturnCounterParameters();
                OtherParameters otherParameters = settingsPage.ReturnOtherParameters();
                SignalParameters signalParameters = settingsPage.ReturnSignalParameters();

                TachographRecord record = new TachographRecord(tachographParameters);
                //TachographRecord record = new TachographRecord(tachographParameters, carParameters, counterParameters, otherParameters, signalParameters);

                int result = await writingInterface.WriteData(record); // návratový kód asynchronní metody

                if (result == 0)
                {
                    MessageBox.Show("Parametry tachografu byly úspěšně zapsány.");
                    settingsPage = new (); // potřeba aktualizovat okno
                    pagesFrame.Content = settingsPage;
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return; // Ukončení metody v případě výjimky
            }
        }

        /// <summary>
        /// Úplně nevím, jak tuhle metodu využít?
        /// </summary>
        private void setCarParametersBtn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
