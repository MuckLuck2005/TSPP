using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Media;

namespace YourNamespace
{
    public partial class WindowWorker : Window
    {
        public WindowWorker()
        {
            InitializeComponent();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            ValidateLogin();
        }

        private void ValidateLogin()
        {
            int flag = 0;
            string log = TextBoxLog.Text.Trim();
            string pass = PasswordBoxPass.Password.Trim();

            if (string.IsNullOrEmpty(log))
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

            if (string.IsNullOrEmpty(pass))
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

            if (flag == 2)
            {
                try
                {
                    using (MySqlDB db = new MySqlDB())
                    {
                        DataTable table = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        db.OpenConnection();
                        MySqlCommand command = new MySqlCommand("SELECT * FROM `companyclient` WHERE `login`= @uL AND `password`= @uP", db.getConnection());
                        command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = log;
                        command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass;
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            Window6 window6 = new Window6();
                            window6.Show();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Такого користувача немає");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: " + ex.Message);
                }
            }
        }
    }
}
