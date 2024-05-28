using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Media;

namespace TSPP
{
    public partial class Window9 : Window
    {
        public Window9()
        {
            InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            string log = TextBoxLog.Text.Trim();
            string pass = TextBoxPassword.Text.Trim();
            string surname = TextBoxSurname.Text.Trim();
            string adres = TextBoxAdres.Text.Trim();
            string debt = TextBoxDebt.Text.Trim();

            if (ValidateInput(log, pass, surname, adres, debt))
            {
                try
                {
                    using (MySqlDB db = new MySqlDB())
                    {
                        MySqlCommand command = new MySqlCommand("INSERT INTO `companyclient` (`login`, `password`, `surname`, `adres`, `debt`) VALUES (@login, @pass, @surname, @adres, @debt)", db.getConnection());
                        command.Parameters.AddWithValue("@login", log);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@adres", adres);
                        command.Parameters.AddWithValue("@debt", debt);

                        db.OpenConnection();

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Запис був створений");
                        }
                        else
                        {
                            MessageBox.Show("Запис не був створений");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка під час створення запису: " + ex.Message);
                }
            }
        }

        private bool ValidateInput(string log, string pass, string surname, string adres, string debt)
        {
            bool isValid = true;

            isValid &= ValidateTextBox(TextBoxLog, log.Length >= 5, "Не коректний ввід даних");
            isValid &= ValidateTextBox(TextBoxPassword, pass.Length >= 5, "Не коректний ввід даних");
            isValid &= ValidateTextBox(TextBoxSurname, !string.IsNullOrEmpty(surname), "Ви не ввели дані");
            isValid &= ValidateTextBox(TextBoxAdres, !string.IsNullOrEmpty(adres), "Ви не ввели дані");
            isValid &= ValidateTextBox(TextBoxDebt, !string.IsNullOrEmpty(debt), "Ви не ввели дані");

            return isValid;
        }

        private bool ValidateTextBox(TextBox textBox, bool condition, string errorMessage)
        {
            if (!condition)
            {
                ShowInputError(textBox, errorMessage);
                return false;
            }
            else
            {
                ResetInputError(textBox);
                return true;
            }
        }

        private void ShowInputError(TextBox textBox, string errorMessage)
        {
            textBox.ToolTip = errorMessage;
            textBox.Background = Brushes.Red;
        }

        private void ResetInputError(TextBox textBox)
        {
            textBox.ToolTip = "";
            textBox.Background = Brushes.Transparent;
        }
    }
}
