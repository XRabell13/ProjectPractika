using ProjectPractika.ViewModels;
using ProjectPractika.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectPractika
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow app = new MainWindow();
            MainWindowViewModel context = new MainWindowViewModel();
            app.DataContext = context;
            app.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // заменяем строку подключения на строку подключения с ролью пользователя, т.е. возвращаем строке прежний вид
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["MyConnectionString"].ConnectionString = ConfigurationManager.ConnectionStrings["UserConnectionString"].ConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

        }
    }
}
