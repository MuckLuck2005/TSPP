using System;
using System.Windows;

namespace TSPP
{
    public partial class Window6 : Window
    {
        public Window6()
        {
            InitializeComponent();
        }

        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            OpenNewWindow();
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            OpenSearchWindow();
        }

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            OpenWordWindow();
        }

        private void OpenNewWindow()
        {
            try
            {
                Window9 window9 = new Window9();
                window9.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the new window: " + ex.Message);
            }
        }

        private void OpenSearchWindow()
        {
            try
            {
                Window7 window7 = new Window7();
                window7.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the search window: " + ex.Message);
            }
        }

        private void OpenWordWindow()
        {
            try
            {
                WindowWord windowWord = new WindowWord();
                windowWord.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the word window: " + ex.Message);
            }
        }
    }
}
