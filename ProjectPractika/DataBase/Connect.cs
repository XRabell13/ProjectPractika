using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace ProjectPractika.DataBase
{
    public class Connect
    {
        string conStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;// @"Server=DESKTOP-0P6S3HA\SQLEXPRESS;Database=ManualDb;Trusted_Connection=Yes;"; // тестовая проверка подключения к серверу
        public bool status = false;

        public SqlConnection connection;

        public Connect()
        {
            connection = new SqlConnection(conStr);
        }

        public void Open()
        {
            if (connection.State != System.Data.ConnectionState.Open)
                try
                {
                    connection.Open();
                }
                catch
                {
                    MessageBox.Show(@"Ошибка соединения с сервером");
                }
            if (connection.State.ToString() == "Open")
            {
                status = true;
              //  MessageBox.Show("Open");
            }
            else
            {
                MessageBox.Show(@"Please check connection string");
            }
        }
        public void Close()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                status = false;
              //  MessageBox.Show("Close");
            }
        }
    }
}
