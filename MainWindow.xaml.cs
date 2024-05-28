using System;
using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Client(object sender, RoutedEventArgs e)
        {
            OpenClientWindow();
        }

        private void Button_Click_Worker(object sender, RoutedEventArgs e)
        {
            OpenWorkerWindow();
        }

        private void OpenClientWindow()
        {
            try
            {
                WindowClient window = new WindowClient();
                window.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the client window: " + ex.Message);
            }
        }

        private void OpenWorkerWindow()
        {
            try
            {
                WindowWorker window1 = new WindowWorker();
                window1.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the worker window: " + ex.Message);
            }
        }
    }
}
