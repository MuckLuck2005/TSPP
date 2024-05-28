using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace TSPP
{
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        public bool Auth_Check(string log, string pass)
        {
            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    db.OpenConnection();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM `companyclient` WHERE `login`= @uL", db.getConnection());
                    command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = log;
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    return table.Rows.Count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при перевірці автентифікації: " + ex.Message);
                return false;
            }
        }

        private int Check(string log, string pass, string surname, string address, string pass2)
        {
            int flag = 0;
            if (log.Length < 5)
            {
                TextBoxLog.ToolTip = "Некоректний ввід даних";
                TextBoxLog.Background = Brushes.Red;
            }
            else
            {
                TextBoxLog.ToolTip = "";
                TextBoxLog.Background = Brushes.Transparent;
                flag++;
            }

            if (pass.Length < 5)
            {
                PasswordBoxPass.ToolTip = "Некоректний ввід даних";
                PasswordBoxPass.Background = Brushes.Red;
            }
            else
            {
                PasswordBoxPass.ToolTip = "";
                PasswordBoxPass.Background = Brushes.Transparent;
                flag++;
            }

            if (string.IsNullOrEmpty(surname))
            {
                TextBoxSurname.ToolTip = "Ви не ввели дані";
                TextBoxSurname.Background = Brushes.Red;
            }
            else
            {
                TextBoxSurname.ToolTip = "";
                TextBoxSurname.Background = Brushes.Transparent;
                flag++;
            }

            if (string.IsNullOrEmpty(address))
            {
                TextBoxAdres.ToolTip = "Ви не ввели дані";
                TextBoxAdres.Background = Brushes.Red;
            }
            else
            {
                TextBoxAdres.ToolTip = "";
                TextBoxAdres.Background = Brushes.Transparent;
                flag++;
            }

            if (pass != pass2)
            {
                PasswordBoxPass2.ToolTip = "Пароль не співпадає";
                PasswordBoxPass2.Background = Brushes.Red;
            }
            else
            {
                PasswordBoxPass2.ToolTip = "";
                PasswordBoxPass2.Background = Brushes.Transparent;
                flag++;
            }

            return flag;
        }

        private void Button_Click_Register(object sender, RoutedEventArgs e)
        {
            string log = TextBoxLog.Text.Trim();
            string pass = PasswordBoxPass.Password.Trim();
            string pass2 = PasswordBoxPass2.Password.Trim();
            string surname = TextBoxSurname.Text.Trim();
            string address = TextBoxAdres.Text.Trim();

            if (Check(log, pass, surname, address, pass2) == 5)
            {
                if (!Auth_Check(log, pass))
                {
                    try
                    {
                        using (MySqlDB db = new MySqlDB())
                        {
                            MySqlCommand command = new MySqlCommand("INSERT INTO `companyclient`(`login`, `password`, `surname`, `adres`) VALUES (@login, @password, @surname, @address)", db.getConnection());
                            db.OpenConnection();
                            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = log;
                            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = pass;
                            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname;
                            command.Parameters.Add("@address", MySqlDbType.VarChar).Value = address;

                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Ви успішно зареєструвалися");
                                Window5 window5 = new Window5(log, pass);
                                window5.Show();
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Не вдалося зареєструватися :(");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталася помилка: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Користувач з таким логіном вже зареєстрований");
                    WindowClient windowClient = new WindowClient();
                    windowClient.Show();
                    Close();
                }
            }
        }
    }
}
