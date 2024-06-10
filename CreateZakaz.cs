using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace specialForcesVeterans
{
    public partial class CreateZakaz : Form
    {
        public CreateZakaz()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string n = checkData();
            if (n.Length != 0)
            {
                MessageBox.Show(n, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (createZakaz())
            {
                MessageBox.Show("Данные заказа успешно сохранены", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearBox();
                Form1 fr = new Form1();
                fr.Show();
                this.Hide();
            }
        }

        private int getIdTypeService(string name, SqlConnection connection)
        {
            string sql = $"SELECT id_service FROM typeServices WHERE name_service = '{name}'";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            int id = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader.GetValue(0));
                }
            }
            reader.Close();
            return id;
        }

        private int getIdTypeObject(string name, SqlConnection connection)
        {
            string sql = $"SELECT id_object FROM typeObjects WHERE name_object = '{name}'";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            int id = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader.GetValue(0));
                }
            }
            reader.Close();
            return id;
        }

        private void clearBox()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";

        }

        private string checkData()
        {
            Regex ex = new Regex("^[0-9]{11}");
            Regex ex2 = new Regex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_" +
                "`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9]" +
                "(?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|" +
                "[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");

            bool isValid = ex.IsMatch(textBox3.Text);
            bool IsValidEmail = ex2.IsMatch(textBox6.Text);
            //если все ячейки пустные, выводим сообщение
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0 || textBox7.Text.Length == 0 || comboBox2.Text.Length == 0 || comboBox1.Text.Length == 0)
            {
                return "Все поля должны быть заполнены";
            }
            if (!isValid)
            {
                return "Номер телефона должен содержать 11 числовых символов.\n" +
                    "Пример: 89009990012";
            }
           
            {

            }
            if (!IsValidEmail)
            {
                return "Адрес почты не корректен";
            }
            else
            {
                string n = "";
                return n;
            }
        }

        private bool createZakaz()
        {
            try
            {
                DataBase dataBase = new DataBase();
                dataBase.openConnection();

                var name = textBox1.Text;
                var fioClient = textBox2.Text;
                var phone = textBox3.Text;
                var time = DateTime.Parse(textBox4.Text);
                var adress = textBox5.Text;
                var email = textBox6.Text;
                var description = textBox7.Text;

                string sql = $"INSERT INTO zakazs (names, fullnameClient, phone, times, typeObject, adress, typeService, email, descriptions) " +
                    $"VALUES ('{name}', '{fioClient}', '{phone}', '{time}', '{getIdTypeObject(comboBox1.Text, dataBase.GetConnection())}', " +
                    $"'{adress}', '{getIdTypeService(comboBox2.Text, dataBase.GetConnection())}', '{email}', '{description}')";

                byte res = 0;
                SqlCommand command1 = new SqlCommand(sql, dataBase.GetConnection());
                if (command1.ExecuteNonQuery() > 0)
                    res++;

                if (res == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите правильный формат времени. Пример: 12:30", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void CreateZakaz_Load(object sender, EventArgs e)
        {
            getTypeObjectComboBox();
            getTypeServiceComboBox();
        }

        private void getTypeObjectComboBox()
        {
            string sql = "SELECT name_object FROM typeObjects";
            DataBase date = new DataBase();
            date.openConnection();
            var command = new SqlCommand(sql, date.GetConnection());

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
            }
            reader.Close();
            date.closeConnection();
        }

        private void getTypeServiceComboBox()
        {
            string sql = "SELECT name_service FROM typeServices";
            DataBase date = new DataBase();
            date.openConnection();
            var command = new SqlCommand(sql, date.GetConnection());

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0));
                }
            }
            reader.Close();
            date.closeConnection();
        }
    }
}
