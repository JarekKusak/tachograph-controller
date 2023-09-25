﻿using System;
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

        const string tachoIP = "192.168.30.15";
        const int sourcePort = 5049;
        const int destinationPort = sourcePort;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += settingsBtn_Click;

            settingsPage = new SettingsPage();
            signalsPage = new SignalsPage();
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
        /// Metoda na zápis dat do tafografu (při správném vyplnění parametrů)
        /// </summary>
        private void setTaphoParametersBtn_Click(object sender, RoutedEventArgs e)
        {
            writingInterface = new WritingInterface(tachoIP, sourcePort, destinationPort);

            try
            {
                int[] intParameters = settingsPage.ReturnNumericalParameters();
                string[] textParameters = settingsPage.ReturnTextParameters();
                bool[] signalParameters = settingsPage.ReturnSignalParameters();

                if (intParameters != null)
                {
                    writingInterface.AddRecord(intParameters, textParameters, signalParameters); 
                    settingsPage = new SettingsPage(); // potřeba aktualizovat okno
                    pagesFrame.Content = settingsPage;
                }  
                else MessageBox.Show("Jeden z parametrů není správně vyplněn."); // chce to vymyslet lepší způsob, jak upozornit na konkrétní problémový parametr
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
