using ProjectPractika.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectPractika.ViewModels
{
    public class AuthorizationViewModel : ObservableObject, IPageViewModel 
    {
        #region IPageViewModel
        string visibility = "Collapsed";

        public string Name
        {
            get
            {
                return "Авторизация";
            }
        }    
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Fields

        private DBLoad dbl = new DBLoad();
        private MainWindowViewModel mVM;
        private ICommand _logIn;
        private string password;
        private string log;

        #endregion

        public AuthorizationViewModel(MainWindowViewModel mVM) {
            this.mVM = mVM;
        }

        #region Properties / Commands
        public ICommand LogIn
        {
            get
            {
                if (_logIn == null)
                {
                    _logIn = new RelayCommand(p => LoginAdmin());
                }

                return _logIn;
            }
        }

        public string Log
        {
            get { return log; }
            set { log = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        private void LoginAdmin()
        {
           bool isAdmin = dbl.IsAdminCheck(Log,Password);
            if (isAdmin) {
                // заменяем строку подключения на строку подключения с ролью админа
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["MyConnectionString"].ConnectionString = ConfigurationManager.ConnectionStrings["AdminConnectionString"].ConnectionString;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");

                mVM.PageViewModels[0].Visibility = "Visible";
                mVM.ChangeViewModel(mVM.PageViewModels[0]);
            }
        }
        #endregion
    }
}
