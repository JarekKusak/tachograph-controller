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
    /// Interakční logika pro settingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        const int buttonsInRow = 16;
        const int rowCount = 3;
        const int buttonHeight = 18;
        const int buttonWidth = 18;
        
        public SettingsPage()
        {
            InitializeComponent();

            StackPanel stackPanel = new StackPanel();
            stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackPanel.VerticalAlignment = VerticalAlignment.Top;
            stackPanel.Margin = new Thickness(10);

            Label titleLabel = new Label();
            titleLabel.Content = "Aktivní signály v záznamu";
            stackPanel.Children.Add(titleLabel);

            StackPanel aroundPanel = new StackPanel();

            Border mainBorder = new Border(); // Vytvořte nový Border pro každý cyklus
            mainBorder.CornerRadius = new CornerRadius(6);
            mainBorder.BorderBrush = Brushes.Gray;
            mainBorder.Background = Brushes.LightGray;
            mainBorder.BorderThickness = new Thickness(2);
            mainBorder.Padding = new Thickness(5);
            mainBorder.Child = aroundPanel; // Přidejte každý buttonPanel do hlavního Borderu
            for (int j = 1; j <= rowCount; j++)
            {
                StackPanel buttonPanel = new StackPanel();
                buttonPanel.Orientation = Orientation.Horizontal;
                buttonPanel.HorizontalAlignment = HorizontalAlignment.Center;

                for (int i = buttonsInRow * (j - 1); i <= buttonsInRow * j; i++)
                {
                    StackPanel buttonContainer = new StackPanel();
                    Label label = new Label();
                    label.Content = i.ToString();

                    Button button = new Button();
                    button.Height = buttonHeight;
                    button.Width = buttonWidth;

                    buttonContainer.Children.Add(label);
                    buttonContainer.Children.Add(button);

                    buttonPanel.Children.Add(buttonContainer);
                }
                aroundPanel.Children.Add(buttonPanel);
            }

            stackPanel.Children.Add(mainBorder);
            Grid.SetColumn(stackPanel, 2);
            settingGrid.Children.Add(stackPanel); // Přidejte StackPanel s hlavním Borderem do Gridu
        }
    }
}
