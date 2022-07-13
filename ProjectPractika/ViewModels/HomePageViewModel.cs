using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.ViewModels
{
    internal class HomePageViewModel : ObservableObject, IPageViewModel
    {
        #region IPageViewModel
        string visibility = "Visible";

        public string Name
        {
            get
            {
                return "Домашняя страница";
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
    }
}
