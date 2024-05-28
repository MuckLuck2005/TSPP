using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace TSPP
{
    public partial class Window8 : Window
    {
        private int ID { get; set; }

        public Window8(string surname, string address, int id)
        {
            ID = id;
            InitializeComponent();
            PopulateClientDetails(surname, address);
        }

        private void PopulateClientDetails(string surname, string address)
        {
            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    MySqlCommand command = new MySqlCommand("SELECT `surname`, `adres`, `debt`, `login`, `password` FROM `companyclient` WHERE `id` = @uID", db.getConnection());
                    command.Parameters.Add("@uID", MySqlDbType.VarChar).Value = ID;

                    db.OpenConnection();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TextBoxSurname.Text = reader["surname"].ToString();
                                TextBoxAdres.Text = reader["adres"].ToString();
                                TextBoxDebt.Text = reader["debt"].ToString();
                                TextBoxLog.Text = reader["login"].ToString();
                                TextBoxPass.Text = reader["password"].ToString();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого клієнта немає");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час отримання даних про клієнта: " + ex.Message);
            }
        }

        private void Button_Click_Change(object sender, RoutedEventArgs e)
        {
            string debt = TextBoxDebt.Text.Trim();
            string log = TextBoxLog.Text.Trim();
            string pass = TextBoxPass.Text.Trim();
            string surname = TextBoxSurname.Text.Trim();
            string address = TextBoxAdres.Text.Trim();

            if (ValidateInput(log, pass, surname, address, debt))
            {
                try
                {
                    using (MySqlDB db = new MySqlDB())
                    {
                        db.OpenConnection();
                        MySqlCommand command = new MySqlCommand("UPDATE `companyclient` SET `surname` = @uS, `adres` = @uA, `debt` = @uD, `login` = @uL, `password` = @uP WHERE `id` = @uID", db.getConnection());
                        command.Parameters.Add("@uS", MySqlDbType.VarChar).Value = surname;
                        command.Parameters.Add("@uA", MySqlDbType.VarChar).Value = address;
                        command.Parameters.Add("@uD", MySqlDbType.VarChar).Value = debt;
                        command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = log;
                        command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass;
                        command.Parameters.Add("@uID", MySqlDbType.VarChar).Value = ID;

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Ви успішно відредагували запис");
                        }
                        else
                        {
                            MessageBox.Show("Ми не змогли відредагувати запис");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка під час відредагування запису: " + ex.Message);
                }
            }
        }

        private bool ValidateInput(string log, string pass, string surname, string address, string debt)
        {
            if (string.IsNullOrEmpty(log) || log.Length < 5)
            {
                ShowInputError(TextBoxLog, "Не коректний ввід даних");
                return false;
            }
            else
            {
                ResetInputError(TextBoxLog);
            }

            if (string.IsNullOrEmpty(pass) || pass.Length < 5)
            {
                ShowInputError(TextBoxPass, "Не коректний ввід даних");
                return false;
            }
            else
            {
                ResetInputError(TextBoxPass);
            }

            if (string.IsNullOrEmpty(surname))
            {
                ShowInputError(TextBoxSurname, "Ви не ввели дані");
                return false;
            }
            else
            {
                ResetInputError(TextBoxSurname);
            }

            if (string.IsNullOrEmpty(address))
            {
                ShowInputError(TextBoxAdres, "Ви не ввели дані");
                return false;
            }
            else
            {
                ResetInputError(TextBoxAdres);
            }

            if (string.IsNullOrEmpty(debt))
            {
                ShowInputError(TextBoxDebt, "Ви не ввели дані");
                return false;
            }
            else
            {
                ResetInputError(TextBoxDebt);
            }

            return true;
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

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    MySqlCommand command = new MySqlCommand("DELETE FROM `companyclient` WHERE `id` = @uID", db.getConnection());
                    command.Parameters.Add("@uID", MySqlDbType.VarChar).Value = ID;

                    db.OpenConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Ви успішно видалили запис");
                    }
                    else
                    {
                        MessageBox.Show("Ми не змогли видалити запис");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час видалення запису: " + ex.Message);
            }
        }
    }
}
