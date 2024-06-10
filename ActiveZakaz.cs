using specialForcesVeterans.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace specialForcesVeterans
{
    public partial class ActiveZakaz : Form
    {
        private int index = -1;
        int selectedRow;
        int idZakaz;
        Zakaz zakaz = null;
       


        public ActiveZakaz()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(zakaz != null)
            {
                EditZakaz fr = new EditZakaz(zakaz);
                fr.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Выберите заказ для изменения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void ActiveZakaz_Load(object sender, EventArgs e)
        {
            getDataZakaz();
        }

        private void getDataZakaz()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=HONOR;Initial Catalog=Veterans;Integrated Security=True");
            sqlConnection.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id [№], names [Наименование], fullnameClient [Клиент], phone [Телефон], " +
                "times [Время для звонка], typeObjects.name_object [Объект], adress [Адрес], typeServices.name_service [Услуга], email [Почта], descriptions [Описание]" +
                " FROM zakazs,  typeObjects, typeServices " +
                "WHERE typeObject = typeObjects.id_object AND typeService = typeServices.id_service", sqlConnection);

            DataTable table = new DataTable();

            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            index = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                idZakaz = Convert.ToInt32(row.Cells[0].Value);
                string names = Convert.ToString(row.Cells[1].Value);
                string fullnameClient = Convert.ToString(row.Cells[2].Value.ToString());
                string phone = Convert.ToString(row.Cells[3].Value.ToString());
                string times = Convert.ToString(row.Cells[4].Value);
                string typeObject = Convert.ToString(row.Cells[5].Value);
                string adress = Convert.ToString(row.Cells[6].Value);
                string typeService = Convert.ToString(row.Cells[7].Value);
                string email = Convert.ToString(row.Cells[8].Value);
                string descriptions = Convert.ToString(row.Cells[9].Value);

                zakaz = new Zakaz(names, fullnameClient, phone, times, typeObject, adress, typeService, email, descriptions);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(zakaz != null)
            {
                deleteZakaz();
                getDataZakaz();

            } else
            {
                MessageBox.Show("Выберите заказ для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void deleteZakaz()
        {
            DataBase data = new DataBase();
            data.openConnection();

            DialogResult dialog = MessageBox.Show("Вы точно хотите удалить данный заказ?", "Вопрос", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                int res = 0;
                string sql1 = $"DELETE FROM zakazs WHERE id = '{idZakaz}'";
              

                SqlCommand command = new SqlCommand(sql1, data.GetConnection());
                if (command.ExecuteNonQuery() > 0)
                {
                    res++;

                }
                if (res == 1)
                {
                    MessageBox.Show("Данные заказа успешно удалены!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Непредвиденная ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (dialog == DialogResult.No)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataBase data = new DataBase();
            data.openConnection();

            var sql = $"SELECT id [№], names [Наименование], fullnameClient [Клиент], phone [Телефон], times [Время для звонка], " +
                $"typeObjects.name_object [Объект], adress [Адрес], typeServices.name_service [Услуга], email [Почта], descriptions [Описание]" +
                $" FROM zakazs,  typeObjects, typeServices WHERE typeObject = typeObjects.id_object AND typeService = typeServices.id_service " +
                $"AND CONCAT(id, names, fullnameClient, phone, times, typeObjects.name_object, adress, typeServices.name_service, email, descriptions) LIKE '%" + textBox1.Text + "%'";

            SqlCommand command = new SqlCommand(sql, data.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }
    }
}
