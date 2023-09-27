using System;
using System.Windows;
using System.Windows.Controls;

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
        /// Metoda na založení záznamu a jeho následný zápis do tachografu
        /// </summary>
        /// <param name="sender"> Tlačítko na zápis parametrů požadovaného typu </param>
        async void CreateRecordAndWriteData(object sender)
        {
            try
            {
                Button clickedBtn = (Button)sender;
                writingInterface = new WritingInterface(tachoIP, sourcePort, destinationPort);
                TachographRecord record = null;

                if (clickedBtn == setTaphoParametersBtn)
                {
                    TachographParameters tachographParameters = settingsPage.ReturnTachoParameters();
                    record = new TachographRecord(tachographParameters);
                }
                else if (clickedBtn == setCarParametersBtn)
                {
                    CarParameters carParameters = settingsPage.ReturnCarParameters();
                    SignalParameters signalParameters = settingsPage.ReturnSignalParameters();
                    OtherParameters otherParameters = settingsPage.ReturnOtherParameters(); 
                    record = new TachographRecord(carParameters, signalParameters, otherParameters);
                }
                else if (clickedBtn == setCountersBtn)
                {
                    CounterParameters counterParameters = settingsPage.ReturnCounterParameters();
                    record = new TachographRecord(counterParameters);

                }
                else // nastavit datum a čas
                {
                    string dateAndTime = (DateTime.Now).ToString("yyyy-dd-MM HH:mm:ss");

                }

                int result = await writingInterface.WriteData(record!); // návratový kód asynchronní metody

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
        /// Jednotná událost na řízení zápisu dat (parametrů) požadovaného typu
        /// </summary>
        /// <param name="sender"> Tlačítko na zápis parametrů požadovaného typu </param>
        private void setParametersBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateRecordAndWriteData(sender);
        }
    }
}
