using System.Diagnostics;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordCounterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Select_File_Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "Text documents (.txt)|*.txt";
            if(fileDialog.ShowDialog() == true)
            {
                try
                {
                    SelectFileButton.IsEnabled = false;
                    int wordCount = await CountWordsInFile(fileDialog.FileName);
                    int charCount = await CountCharactersInFile(fileDialog.FileName);
                    SelectFileButton.IsEnabled = true;
                    if(wordCount != 0)
                    {
                        WordsCountUI.Text = wordCount.ToString("#,##");
                    }
                    else if(wordCount == 0)
                    {
                        WordsCountUI.Text = ":(";
                    }
                    if(charCount !=0)
                    {
                        CharactersCountUI.Text = charCount.ToString("#,##");
                    }
                    else if(charCount == 0)
                    {
                        CharactersCountUI.Text = ":(";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Something went wrong.",MessageBoxButton.OK);
                    Application.Current.Shutdown();
                }
            }
        }

        private async Task<int> CountWordsInFile(string fileName)
        {
            return await Task.Run(() =>
            {
                string fileWordCount = File.ReadAllText(fileName);
                return fileWordCount.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;}
            );
            
        }

        private async Task<int> CountCharactersInFile(string fileName)
        {
            return await Task.Run(() =>
            {
                return File.ReadAllText(fileName).Length;
            });
        }
    }
}