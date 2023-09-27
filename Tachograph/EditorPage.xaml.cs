using System;
using System.Windows;
using System.Windows.Controls;

namespace Tachograph
{
    /// <summary>
    /// Interakční logika pro EditorPage.xaml
    /// </summary>
    public partial class EditorPage : Page
    {

        FileManager fileManager;
        public EditorPage(FileManager fileManager)
        {
            InitializeComponent();
            this.fileManager = fileManager;
            LoadContent();
        }   

        void LoadContent()
        {
            string fileContents = fileManager.OpenFileAndReadContents();
            socketEditorTxtBox.Text = fileContents;
        }

        private void saveEditorBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void openEditorBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
