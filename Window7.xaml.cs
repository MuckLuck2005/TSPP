using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace TSPP
{
    public partial class Window7 : Window
    {
        public Window7()
        {
            InitializeComponent();
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            string surname = TextBoxSurname.Text.Trim();
            string address = TextBoxAdres.Text.Trim();

            if (string.IsNullOrEmpty(surname))
            {
                ShowInputError(TextBoxSurname, "Не коректний ввід даних");
                return;
            }
            else
            {
                ResetInputError(TextBoxSurname);
            }

            if (string.IsNullOrEmpty(address))
            {
                ShowInputError(TextBoxAdres, "Не коректний ввід даних");
                return;
            }
            else
            {
                ResetInputError(TextBoxAdres);
            }

            SearchClient(surname, address);
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

        private void SearchClient(string surname, string address)
        {
            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    db.OpenConnection();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM `companyclient` WHERE `surname`= @uS AND `adres`= @uA", db.getConnection());
                    command.Parameters.Add("@uS", MySqlDbType.VarChar).Value = surname;
                    command.Parameters.Add("@uA", MySqlDbType.VarChar).Value = address;
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        int id = 0;
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = Convert.ToInt32(reader["id"]);
                            }
                        }

                        Window8 window8 = new Window8(surname, address, id);
                        window8.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Такого клієнта немає");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час пошуку клієнта: " + ex.Message);
            }
        }
    }
}
