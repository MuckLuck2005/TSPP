using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace TSPP
{
    public partial class WindowWord : Window
    {
        public WindowWord()
        {
            InitializeComponent();
        }

        private void Button_Click_Word(object sender, RoutedEventArgs e)
        {
            string debt = TextBoxSearchDebt.Text.Trim();

            if (string.IsNullOrEmpty(debt))
            {
                ShowInputError(TextBoxSearchDebt, "Ви не заповнили поле");
                return;
            }

            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    MySqlCommand command = new MySqlCommand("SELECT * FROM `companyclient` WHERE `debt` > @debt", db.getConnection());
                    command.Parameters.AddWithValue("@debt", debt);
                    db.OpenConnection();

                    using (MySqlDataReader read = command.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            string text = $"{read[2]} {read[3]} {read[5]}";
                            SaveFile(text);
                        }
                    }
                }

                MessageBox.Show("Дані були успішно записані у файл");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка: " + ex.Message);
            }
        }

        private void Button_Click_Word1(object sender, RoutedEventArgs e)
        {
            string surname = TextBoxSearchSurname.Text.Trim();
            string adres = TextBoxSearchAdres.Text.Trim();

            if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(adres))
            {
                ShowInputError(string.IsNullOrEmpty(surname) ? TextBoxSearchSurname : TextBoxSearchAdres, "Ви не заповнили поле");
                return;
            }

            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    MySqlCommand command = new MySqlCommand("SELECT `debt` FROM `companyclient` WHERE `surname`= @surname AND `adres`= @adres", db.getConnection());
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@adres", adres);
                    db.OpenConnection();

                    using (MySqlDataReader read = command.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            string debt = read[0].ToString();
                            string text = $"Прізвище: {surname}; Адреса: {adres}; Борг: {debt}";
                            SaveFile(text);
                            MessageBox.Show("Запис був здійснений у файл текстового файлу");
                        }
                        else
                        {
                            MessageBox.Show("Такого клієнта немає в базі");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка: " + ex.Message);
            }
        }

        private void SaveFile(string text)
        {
            string path = TextBoxSaveFile.Text.Trim();

            if (string.IsNullOrEmpty(path))
            {
                ShowInputError(TextBoxSaveFile, "Ви не заповнили поле");
                return;
            }

            try
            {
                path = @"D:\net code\TSPP\" + path + ".txt";

                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка під час запису в файл: " + ex.Message);
            }
        }

        private void ShowInputError(TextBox textBox, string errorMessage)
        {
            textBox.ToolTip = errorMessage;
            textBox.Background = Brushes.Red;
        }
    }
}
