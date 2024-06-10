using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace specialForcesVeterans
{
    class DataBase
    {

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=HONOR;Initial Catalog=Veterans;Integrated Security=True");

        public void openConnection()//открываем подключения к БД
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed) //если подключение к БД закрыто, мы его открываем
            {
                sqlConnection.Open();
            }

        }

        public void closeConnection()//закрываем подключение
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()//возвращаем подключение
        {
            return sqlConnection;
        }
    }
}
