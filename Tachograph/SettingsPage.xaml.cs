using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interakční logika pro settingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        const int buttonsInRow = 16;
        const int rowCount = 3;
        const int buttonHeight = 18;
        const int buttonWidth = 18;
        const int labelAndButtonContainerWidth = 25;
        const int stackPanelMargin = 10;
        const int cornerRadius = 6;
        const int borderThickness = 2;
        const int borderPadding = 5;
        const int column = 2;

        string speedRecordTypeContent;
        string tachographTypeContent;


        public SettingsPage()
        {
            InitializeComponent();

            StackPanel mainPanel = new StackPanel();

            mainPanel.Children.Add(CreateSignalStackPanel("Aktivní signály v záznamu"));
            mainPanel.Children.Add(CreateSignalStackPanel("Signály pro test brzdy"));
            mainPanel.Children.Add(CreateSignalStackPanel("Inverzní signály"));

            Grid.SetColumn(mainPanel, column);
            settingGrid.Children.Add(mainPanel); // Přidejte StackPanel s hlavním Borderem do Gridu

            speedRecordTypeContent = "PR.";
            tachographTypeContent = "TT62";
        } 

        /// <summary>
        /// Vrací číselné parametry tachografu
        /// </summary>
        /// <returns> Pole číselných parametrů </returns>
        public int?[] NumericalParameters()
        {
            try
            {
                int wheelDiameter = int.Parse(wheelDiameterTxtBox.Text);
                int carNumber = int.Parse(carNumberTxtBox.Text);
                int gearRatio = int.Parse(gearRatioTxtBox.Text);
                int maxWheelDiameter = int.Parse(maxWheelDiameterTxtBox.Text);
                int maxSpeed = int.Parse(maxSpeedTxtBox.Text); // km/h
                int kFactor = int.Parse(kFactorTxtBox.Text);
                // Blok počítadla:
                int totalKilometersDriven = int.Parse(totalKilometersDrivenTxtBox.Text);
                int counter1 = int.Parse(counter1TxtBox.Text);
                int counter2 = int.Parse(counter2TxtBox.Text);
                int counter3 = int.Parse(counter3TxtBox.Text);
                int counter4 = int.Parse(counter4TxtBox.Text);
                int counter5 = int.Parse(counter5TxtBox.Text);
                int mode = 0; // radio
                // může být i float!!!
                int recordStep = int.Parse(recordStepComboBox.SelectedItem.ToString().Split()[1]); // položky v comboboxu mají podobu "X m" a my chceme uložit pouze číselné X (nultá položka je typ elementu...)

                int?[] parameters = { wheelDiameter, carNumber, gearRatio, maxWheelDiameter, maxSpeed, kFactor,
                    totalKilometersDriven, counter1, counter2, counter3, counter4, counter5,
                    mode, recordStep };
                return parameters;
            }
            catch
            {
                // ---------zpráva o selhání---------
                return null;
            }        
        }

        /// <summary>
        /// Vrací textové parametry tachografu
        /// </summary>
        /// <returns> Pole textových parametrů tachografu </returns>
        public string[] TextParameters()
        {
            string carType = carTypeTxtBox.Text;
            string speedRecordType = speedRecordTypeContent;
            string tachographType = tachographTypeContent; 

            string[] parameters = { carType, speedRecordType, tachographType };
            return parameters;
        }

        /// <summary>
        /// Zaznamenává zakliklý radio button pro tachographType
        /// </summary>
        void tachographTypeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton clickedRadioButton = (RadioButton)sender;
            if (clickedRadioButton.IsChecked == true)
                tachographTypeContent = clickedRadioButton.Content.ToString();
        }

        /// <summary>
        /// Zaznamenává zakliklý radio button pro speedRecordType
        /// </summary>
        void speedRecordTypeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton clickedRadioButton = (RadioButton)sender;
            if (clickedRadioButton.IsChecked == true)
                speedRecordTypeContent = clickedRadioButton.Content.ToString();
        }

        /// <summary>
        /// Vytvoří požadovaný stack panel na signály, vždy s konkrétním popisem (labelem)
        /// </summary>
        /// <param name="description"> Typ signálů </param>
        /// <returns> Vrátí vytvořený stack panel </returns>
        StackPanel CreateSignalStackPanel(string description)
        {
            StackPanel signalPanel = new StackPanel(); // panel s labelem a tlačítky
            signalPanel.HorizontalAlignment = HorizontalAlignment.Center;
            signalPanel.VerticalAlignment = VerticalAlignment.Center;
            signalPanel.Margin = new Thickness(stackPanelMargin);

            Label titleLabel = new Label();
            titleLabel.Content = description;
            signalPanel.Children.Add(titleLabel);

            StackPanel aroundPanel = new StackPanel(); // obalovací panel na tlačítka (následně ohraničen Borderem)

            Border mainBorder = new Border(); // Vytvořte nový Border pro každý cyklus
            mainBorder.CornerRadius = new CornerRadius(cornerRadius);
            mainBorder.BorderBrush = Brushes.Gray;
            mainBorder.Background = Brushes.LightGray;
            mainBorder.BorderThickness = new Thickness(borderThickness);
            mainBorder.Padding = new Thickness(borderPadding);
            mainBorder.Child = aroundPanel; // Přidejte každý buttonPanel do hlavního Borderu
            for (int j = 1; j <= rowCount; j++)
            {
                StackPanel buttonPanel = new StackPanel(); // panel na řadu tlačítek
                buttonPanel.Orientation = Orientation.Horizontal;
                buttonPanel.HorizontalAlignment = HorizontalAlignment.Center;

                for (int i = buttonsInRow * (j - 1) + 1; i <= buttonsInRow * j; i++)
                {
                    StackPanel buttonContainer = new StackPanel(); // panel na tlačítko s jeho ID
                    buttonContainer.Width = labelAndButtonContainerWidth;
                    Label label = new Label();
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.Content = i.ToString(); // ID tlačítka

                    Button button = new Button();
                    button.Height = buttonHeight;
                    button.Width = buttonWidth;

                    buttonContainer.Children.Add(label);
                    buttonContainer.Children.Add(button);

                    buttonPanel.Children.Add(buttonContainer);
                }
                aroundPanel.Children.Add(buttonPanel);
            }

            signalPanel.Children.Add(mainBorder);
            return signalPanel;
        }
    }
}
