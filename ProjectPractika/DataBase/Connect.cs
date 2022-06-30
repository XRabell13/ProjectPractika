using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectPractika.DataBase
{
    public class Connect
    {
        string conStr = @"Server=DESKTOP-0P6S3HA\SQLEXPRESS;Database=Test;Trusted_Connection=Yes;";
        public bool status = false;

        public SqlConnection conn;

        public Connect()
        {
            conn = new SqlConnection(conStr);
        }

        public void Open()
        {
            if (conn.State != System.Data.ConnectionState.Open)
                try
                {
                    conn.Open();
                }
                catch
                {
                    MessageBox.Show(@"Ошибка соединения с сервером");
                }
            if (conn.State.ToString() == "Open")
            {
                status = true;
                MessageBox.Show("Open");
            }
            else
            {
                MessageBox.Show(@"Please check connection string");
            }
        }
        public void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                status = false;
                MessageBox.Show("Close");
            }
        }
    }
}
