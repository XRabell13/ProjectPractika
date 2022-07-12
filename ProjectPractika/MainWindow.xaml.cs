using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectPractika.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // public SqlConnection conn;
       // string conStr = ConfigurationManager.ConnectionStrings["UserConnectionString"].ConnectionString;// @"Server=DESKTOP-0P6S3HA\SQLEXPRESS;Database=ManualDb;Trusted_Connection=Yes;"; // тестовая проверка подключения к серверу
       // public bool status = false;

        public MainWindow()
        {
            InitializeComponent();

          //  conn = new SqlConnection(conStr);
          //  MessageBox.Show("A");
          //  GetUsers();
            // название процедуры
           /* string sqlExpression = "AllCatTest";

            Open();
            SqlCommand command = new SqlCommand(sqlExpression, conn);
            // указываем, что команда представляет хранимую процедуру
            command.CommandType = System.Data.CommandType.StoredProcedure;
            var reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                MessageBox.Show("No void");

                while (reader.Read())
                {
                    int num = reader.GetInt32(0);
                    MessageBox.Show(num + "");
                }
            }
            reader.Close();
            Close();*/

        }

        /*
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

        // вывод всех пользователей
        private void GetUsers()
        {
            // название процедуры
            string sqlExpression = "GeneralApp.AllCategorySpec";

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                MessageBox.Show("1");

                var reader = command.ExecuteReader();
                MessageBox.Show("2");
                if (reader.HasRows)
                {
                    MessageBox.Show("no void");

                    while (reader.Read())
                    {
                        
                        MessageBox.Show(reader.GetInt32(0)+ "  "+ reader.GetString(1));

                    }
                }
                reader.Close();
            }
        }*/

    }


}
