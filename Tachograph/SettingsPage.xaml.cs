using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        string[] signals = { "aktivní signály", "brzdné signály", "inverzní signály" };
        
        string speedRecordTypeRadioBtnContent;
        string tachographTypeRadioBtnContent;
        int modeRadioBtnContent;
        bool[] markedSignals = { false, false, false };

        List<ToggleButton> signalButtons;
        List<Button> markingSignalsButtons = new List<Button>();

        public SettingsPage()
        {
            InitializeComponent();

            StackPanel mainPanel = new StackPanel();
            mainPanel.VerticalAlignment = VerticalAlignment.Center;

            signalButtons = new List<ToggleButton>(); // list na ukládání signálů (mělo by jich být 144)

            mainPanel.Children.Add(CreateSignalStackPanel("Aktivní signály v záznamu"));
            mainPanel.Children.Add(CreateSignalStackPanel("Signály pro test brzdy"));
            mainPanel.Children.Add(CreateSignalStackPanel("Inverzní signály"));

            markingSignalsButtons.Add(pressActiveSignalsBtn);
            markingSignalsButtons.Add(pressBreakSignalsBtn);
            markingSignalsButtons.Add(pressInverseSignalsBtn);

            Grid.SetColumn(mainPanel, column);
            settingGrid.Children.Add(mainPanel); // Přidejte StackPanel s hlavním Borderem do Gridu

            speedRecordTypeRadioBtnContent = "PR.";
            tachographTypeRadioBtnContent = "TT62";
            modeRadioBtnContent = 0;
        }

        /// <summary>
        /// Vrací číselné parametry tachografu
        /// </summary>
        /// <returns> Pole číselných parametrů </returns>
        public int?[] ReturnNumericalParameters()
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
                int mode = modeRadioBtnContent; // radio

                // může být i float!!!
                int recordStep = int.Parse(recordStepComboBox.SelectedItem.ToString().Split()[1]); // položky v comboboxu mají podobu "X m" a my chceme uložit pouze číselné X (nultá položka je typ elementu...)

                int?[] parameters = { wheelDiameter, carNumber, gearRatio, maxWheelDiameter, maxSpeed, kFactor,
                    totalKilometersDriven, counter1, counter2, counter3, counter4, counter5,
                    mode, recordStep };

                return parameters;
            }
            catch { return null; }        
        }

        /// <summary>
        /// Vrací textové parametry tachografu
        /// </summary>
        /// <returns> Pole textových parametrů tachografu </returns>
        public string[] ReturnTextParameters()
        {
            string carType = carTypeTxtBox.Text;
            string speedRecordType = speedRecordTypeRadioBtnContent;
            string tachographType = tachographTypeRadioBtnContent; 

            string[] parameters = { carType, speedRecordType, tachographType };
            return parameters;
        }

        /// <summary>
        /// Vrací všechny druhy (zakliklých) signálů
        /// </summary>
        /// <returns> Bool pole (ne)zakliklých signálů </returns>
        public bool[] ReturnSignalParameters()
        {
            bool[] turnedSignals = new bool[signalButtons.Count];
            for (int i = 0; i < signalButtons.Count; i++)
                if (signalButtons[i].IsChecked == true)
                    turnedSignals[i] = true;      
            return turnedSignals;
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

                    ToggleButton toggleButton = new ToggleButton(); // Použijeme ToggleButton místo Button
                    toggleButton.Height = buttonHeight;
                    toggleButton.Width = buttonWidth;

                    buttonContainer.Children.Add(label);
                    buttonContainer.Children.Add(toggleButton);

                    buttonPanel.Children.Add(buttonContainer);

                    signalButtons.Add(toggleButton); // Přidejte tlačítko do pole
                }
                aroundPanel.Children.Add(buttonPanel);
            }

            signalPanel.Children.Add(mainBorder);
            return signalPanel;
        }

        /// <summary>
        /// Univerzální metoda pro obsluhu RadioButton kliknutí s generickým typem T.
        /// </summary>
        private void RadioButton_Click<T>(object sender, RoutedEventArgs e, Action<T> action)
        {
            // Přetypování odesílatele (sender) na RadioButton.
            RadioButton clickedRadioButton = (RadioButton)sender;

            // Zkontrolujeme, zda byl RadioButton označen.
            if (clickedRadioButton.IsChecked == true)
            {
                // Získáme obsah RadioButtonu a přetypujeme ho na generický typ T.
                T content = (T)Convert.ChangeType(clickedRadioButton.Content, typeof(T));

                // Volání akce (delegátu) s obsahem RadioButtonu jako argumentem.
                action(content);
            }
        }

        /// <summary>
        /// Obsluha RadioButton kliknutí pro tlačítko modeRadioButton s typem int.
        /// </summary>
        private void modeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            // Volání generické metody s typem int a delegátem, který nastavuje modeRadioBtnContent.
            RadioButton_Click<int>(sender, e, (content) => { modeRadioBtnContent = content; });
        }

        /// <summary>
        /// Obsluha RadioButton kliknutí pro tlačítko tachographTypeRadioButton s typem string.
        /// </summary>
        private void tachographTypeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            // Volání generické metody s typem string a delegátem, který nastavuje tachographTypeRadioBtnContent.
            RadioButton_Click<string>(sender, e, (content) => { tachographTypeRadioBtnContent = content; });
        }

        /// <summary>
        /// Obsluha RadioButton kliknutí pro tlačítko speedRecordTypeRadioButton s typem string.
        /// </summary>
        private void speedRecordTypeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            // Volání generické metody s typem string a delegátem, který nastavuje speedRecordTypeRadioBtnContent.
            RadioButton_Click<string>(sender, e, (content) => { speedRecordTypeRadioBtnContent = content; });
        }

        /// <summary>
        /// Metoda označuje/odznačuje vybrané typy signálů
        /// </summary>
        private void pressSignalsBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            for (int i = 0; i < markingSignalsButtons.Count; i++) // buttons = pole tří tlačítek
            {
                if (button == markingSignalsButtons[i])
                {
                    // vždy se signály odznačují po třetinách (signály jsou rozdělené do tří typů)
                    if (markedSignals[i]) // bool pole tří typů signálů
                    {
                        for (int j = (signalButtons.Count / 3) * i; j < (signalButtons.Count / 3) * (i + 1); j++)
                            signalButtons[j].IsChecked = false;
                        markingSignalsButtons[i].Content = $"Označit {signals[i]}";
                    }
                    else
                    {
                        for (int j = (signalButtons.Count/3) * i; j < (signalButtons.Count / 3) * (i + 1); j++)
                            signalButtons[j].IsChecked = true;
                        markingSignalsButtons[i].Content = $"Odznačit {signals[i]}";
                    }
                    markedSignals[i] = !markedSignals[i];
                    break;
                }
            }
        } 
    }
}
