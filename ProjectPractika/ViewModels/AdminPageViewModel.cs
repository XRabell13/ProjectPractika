using ProjectPractika.DataBase;
using ProjectPractika.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectPractika.ViewModels
{
    public class AdminPageViewModel : ObservableObject, IPageViewModel
    {

        DBLoad dbl = new DBLoad();

        ObservableCollection<Category> categories = new ObservableCollection<Category>();

        public ObservableCollection<Category> Categories { 
            get { return categories; }
            set { categories = value; }
        }

        public AdminPageViewModel()
        {
            categories = dbl.GetAllCategory();
            
        }

        Category selectedCategory;
        string visibility = "Collapsed";
     
        public string Name
        {
            get
            {
                return "АдминПанель";
            }
        }

        public string Visibility {
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

        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
            }
        }
 

      /*  private void cb_entrants_TextChanged()
        {
            //  MessageBox.Show(Text);

            if (Text.Length != 0)
            {
                SelectedEntrant = null; // Если набирается текст сбросить выбраный элемент
            }
            if (Text.Length == 0 && SelectedEntrant == null)
            {
                IsDropDownOpen = false; // Если сбросили текст и элемент не выбран, сбросить фокус выпадающего списка
            }

            IsDropDownOpen = true;
            if (SelectedEntrant == null)
            {
                //Entrants.Clear();
                foreach (Category category in entrants) {
                    if (category.CategoryName.IndexOf(Text) > -1)
                        MessageBox.Show("sfspofk");
                      //Entrants.Add(category);

                }
                // CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(Entrants);
                //  cv.Filter = s => ((string)s).IndexOf(Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }*/

    }
}
