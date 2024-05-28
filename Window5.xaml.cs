using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace TSPP
{
    public partial class Window5 : Window
    {
        private string log1;
        private string pass1;

        public Window5(string log, string pass)
        {
            log1 = log;
            pass1 = pass;
            InitializeComponent();
            LoadClientData();
        }

        private void LoadClientData()
        {
            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    DataTable table = new DataTable();
                    MySqlCommand command = new MySqlCommand("SELECT `surname`,`adres`,`debt` FROM `companyclient` WHERE `login`= @uL AND `password`=@uP", db.getConnection());
                    command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = log1;
                    command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass1;

                    db.OpenConnection();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        TextBoxSurname.Text = reader["surname"].ToString();
                        TextBoxAdres.Text = reader["adres"].ToString();
                        TextBoxDebt.Text = reader["debt"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при завантаженні даних: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double debt = 0.0;
            if (!double.TryParse(TextBoxCloseDebt.Text, out debt))
            {
                MessageBox.Show("Ви ввели невірне значення");
                return;
            }

            try
            {
                using (MySqlDB db = new MySqlDB())
                {
                    MySqlCommand command = new MySqlCommand("UPDATE `companyclient` SET `debt`= `debt` - @uD WHERE `login`= @uL AND `password`= @uP", db.getConnection());
                    command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = log1;
                    command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = pass1;
                    command.Parameters.Add("@uD", MySqlDbType.Double).Value = debt;

                    db.OpenConnection();

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Ви успішно погасили борг");
                        LoadClientData(); // Reload client data after updating debt
                    }
                    else
                    {
                        MessageBox.Show("Сталася помилка при оновленні даних");
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
