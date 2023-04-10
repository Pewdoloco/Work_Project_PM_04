using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;Uid=tarkov;Pwd=[(Kqx-nritx8jbHy;");
        public MainForm()
        {
            InitializeComponent();
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM users", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.google.com");
        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.Text = e.Url.ToString() + "Is loading ..........";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                string sql = "INSERT INTO users (name, password, login, email) VALUES (@name, @password, @login, @email)";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", textBoxName.Text);
                command.Parameters.AddWithValue("@password", textBoxPassword.Text);
                command.Parameters.AddWithValue("@login", textBoxLogin.Text);
                command.Parameters.AddWithValue("@email", textBoxEmail.Text);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Пользователь добавлен успешно.");
                }
                else
                {
                    MessageBox.Show("Пользователь не добавлен.");
                }

                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM users", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении пользователя: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                string sql = "UPDATE users SET name=@name, password=@password, login=@login, email=@email WHERE id=@id";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", textBoxName.Text);
                command.Parameters.AddWithValue("@password", textBoxPassword.Text);
                command.Parameters.AddWithValue("@login", textBoxLogin.Text);
                command.Parameters.AddWithValue("@email", textBoxEmail.Text);
                command.Parameters.AddWithValue("@id", dataGrid.CurrentRow.Cells["id"].Value);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Пользователь обновлен успешно.");
                }
                else
                {
                    MessageBox.Show("Пользователь не обновлен.");
                }

                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM users", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении пользователя: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                string sql = "DELETE FROM users WHERE id=@id";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", dataGrid.CurrentRow.Cells["id"].Value);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Пользователь удален успешно.");
                }
                else
                {
                    MessageBox.Show("Пользователь не удален.");
                }

                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM users", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGrid.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении пользователя: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}