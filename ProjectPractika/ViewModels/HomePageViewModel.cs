﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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


        string dateTime;

        public string DateTimeStr
        {
            get { return dateTime; }
            set { dateTime = value; OnPropertyChanged(); }
        }

        public HomePageViewModel()
        {
            DateTimeStr = DateTime.Now.Date.ToString().Substring(0,10);
           // MessageBox.Show(DateTimeStr);
        }
    
    }
}
