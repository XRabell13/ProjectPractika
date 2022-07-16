using ProjectPractika.DataBase;
using ProjectPractika.DataBase.Administration;
using ProjectPractika.Models;
using ProjectPractika.Models.Helper_models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectPractika.ViewModels
{
    public partial class AdminPageViewModel : ObservableObject, IPageViewModel
    {
        #region IPageViewModel
        string visibility = "Collapsed";

        public string Name
        {
            get
            {
                return "АдминПанель";
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

        DBLoad dbl = new DBLoad();
        DBLoadAdmin dbla = new DBLoadAdmin();

        #region Fields
        bool enabledEntrant = false, enabledEntry = false, enabledEducationalIns = false,
           enabledSpecialization = false, enabledConcourse = false;

        bool enabledDropDownEntrant = false, enabledDropDownEntry = false, enabledDropDownEducationalIns = false,
         enabledDropDownSpecialization = false, enabledDropDownConcourse = false;

        bool checkRBtnIsFreeYes = false, checkRBtnIsFreeNo = false, 
            checkRBtnIsIntramuralYes = false, checkRBtnIsIntramuralNo = false;

        string searchTxtEntrant = "", searchTxtEntry = "", searchTxtEducationalIns = "",
             searchTxtSpecialization = "", searchTxtConcourse = "";

        private ICommand _delCategory, _delEntrant, _delEducationalIns, _delEntry, _delConcourse, _delSpecialization;
        List<int> years = new List<int>() { 2019, 2020, 2021, 2022 };
        
        ObservableCollection<Category> categories = new ObservableCollection<Category>();
        ObservableCollection<Entrant> entrants = new ObservableCollection<Entrant>();
        ObservableCollection<EducationIns> educationIns = new ObservableCollection<EducationIns>();
        ObservableCollection<ConcourseWithEduAndSpec> concourses = new ObservableCollection<ConcourseWithEduAndSpec>();

        ObservableCollection<Entrant> entrantsDG = new ObservableCollection<Entrant>();
        ObservableCollection<EducationIns> educationInsDG = new ObservableCollection<EducationIns>();

        int selectedYear;
        Category selectedCategory; 
        Entrant selectedEntrant;
        EducationIns selectedEducationIns;
        ConcourseWithEduAndSpec selectedConcourseWithEduAndSpec;
         //придумать и изменить для радио баттона
        private bool[] _modeArray = new bool[] { true, false};
        public bool[] ModeArray
        {
            get { return _modeArray; }
        }
        public int SelectedMode
        {
            get { return Array.IndexOf(_modeArray, true); }
        }

        #endregion

        #region Properties / Commands

        #region EnabledProperties Binding
        //  COMBOBOX ENABLED
        public bool EnabledEntrant {
            get { return enabledEntrant; }
            set { enabledEntrant = value; OnPropertyChanged(); }
        }
        public bool EnabledEntry {
            get { return enabledEntry; } 
            set { enabledEntry = value; OnPropertyChanged(); }
        }
        public bool EnabledEducationalIns
        {
           get { return enabledEducationalIns; }
           set { enabledEducationalIns = value; OnPropertyChanged(); }
        }
        public bool EnabledSpecialization
        {
            get { return enabledSpecialization; }
            set { enabledSpecialization = value; OnPropertyChanged(); }
        }
        public bool EnabledConcourse
        {
            get { return enabledConcourse; }
            set { enabledConcourse = value; OnPropertyChanged(); }
        }
        // DROPDOWN ENABLED
        public bool EnabledDropDownEntrant
        {
            get { return enabledDropDownEntrant; }
            set { enabledDropDownEntrant = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownEntry
        {
            get { return enabledDropDownEntry; }
            set { enabledDropDownEntry = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownEducationalIns
        {
            get { return enabledDropDownEducationalIns; }
            set { enabledDropDownEducationalIns = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownSpecialization
        {
            get { return enabledDropDownSpecialization; }
            set { enabledDropDownSpecialization = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownConcourse
        {
            get { return enabledDropDownConcourse; }
            set { enabledDropDownConcourse = value; OnPropertyChanged(); }
        }

        // RADIOBUTTON ENABLED

        public bool CheckRBtnIsFreeYes
        {
            get { return checkRBtnIsFreeYes; }
            set { checkRBtnIsFreeYes = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnIsFreeNo
        {
            get { return checkRBtnIsFreeNo; }
            set { checkRBtnIsFreeNo = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnIsIntramuralYes
        {
            get { return checkRBtnIsIntramuralYes; }
            set { checkRBtnIsIntramuralYes = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnIsIntramuralNo
        {
            get { return checkRBtnIsIntramuralNo; }
            set { checkRBtnIsIntramuralNo = value; OnPropertyChanged(); }
        }

        #endregion

        #region SearchTextProperties Binding

        public string SearchTxtEntrant
        {
            get { return searchTxtEntrant; }
            set { searchTxtEntrant = value; OnPropertyChanged(); SearchEntrants(); }
        }
        public string SearchTxtEntry
        {
            get { return searchTxtEntry; }
            set { searchTxtEntry = value; OnPropertyChanged(); SearchEntry(); }
        }
        public string SearchTxtEducationalIns
        {
            get { return searchTxtEducationalIns; }
            set { searchTxtEducationalIns = value; OnPropertyChanged(); SearchEducationalIns(); }
        }
        public string SearchTxtSpecialization
        {
            get { return searchTxtSpecialization; }
            set { searchTxtSpecialization = value; OnPropertyChanged(); SearchSpecialization(); }
        }
        public string SearchTxtConcourse
        {
            get { return searchTxtConcourse; }
            set { searchTxtConcourse = value; OnPropertyChanged(); SearchConcourse(); }
        }
        #endregion

        #region SelectedItem / ComboBox-Delete Bindings
        public List<int> Years
        {
            get { return years; }
            set { years = value; }
        }

        public int SelectedYear
        {
            get { return selectedYear; }
            set { selectedYear = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Category> Categories { 
            get { return categories; }
            set { categories = value; OnPropertyChanged(); }
        }
       
        public Category SelectedCategory { 
            get   { return selectedCategory; }
            set  {  selectedCategory = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Entrant> Entrants
        {
            get { return entrants; }
            set { entrants = value; OnPropertyChanged(); }
        }

        public Entrant SelectedEntrant
        {
            get { return selectedEntrant; }
            set { selectedEntrant = value; OnPropertyChanged(); }
        }
        public ObservableCollection<EducationIns> EducationalIns
        {
            get { return educationIns; }
            set { educationIns = value; OnPropertyChanged(); }
        }

        public EducationIns SelectedEducationalIns
        {
            get { return selectedEducationIns; }
            set { selectedEducationIns = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ConcourseWithEduAndSpec> Concourses
        {
            get { return concourses; }
            set { concourses = value; OnPropertyChanged(); }
        }

        public ConcourseWithEduAndSpec SelectedConcourseWithEduAndSpec
        {
            get { return selectedConcourseWithEduAndSpec; }
            set { selectedConcourseWithEduAndSpec = value; OnPropertyChanged(); }
        }
        #endregion

        #region DataGrid ItemsSource Bindings

        public ObservableCollection<Entrant> EntrantsDG
        {
            get { return entrantsDG; }
            set { entrantsDG = value; OnPropertyChanged(); }
        }

        public ObservableCollection<EducationIns> EducationalInsDG
        {
            get { return educationInsDG; }
            set { educationInsDG = value; OnPropertyChanged(); }
        }



        #endregion

        #region ICommands

        public ICommand DeleteCategoryCommand
        {
            get
            {
                if (_delCategory == null)
                {
                    _delCategory = new RelayCommand(p => DeleteCategory());
                }

                return _delCategory;
            }
        }
        public ICommand DeleteEntrantCommand
        {
            get
            {
                if (_delEntrant == null)
                {
                    _delEntrant = new RelayCommand(p => DeleteEntrant());
                }

                return _delEntrant;
            }
        }
        public ICommand DeleteEntryCommand
        {
            get
            {
                if (_delEntry == null)
                {
                    _delEntry = new RelayCommand(p => DeleteEntry());
                }

                return _delEntry;
            }
        }
        public ICommand DeleteEduCommand
        {
            get
            {
                if (_delEducationalIns == null)
                {
                    _delEducationalIns = new RelayCommand(p => DeleteEducationalIns());
                }

                return _delEducationalIns;
            }
        }
        public ICommand DeleteSpecializationCommand
        {
            get
            {
                if (_delSpecialization == null)
                {
                    _delSpecialization = new RelayCommand(p => DeleteSpecialization());
                }

                return _delSpecialization;
            }
        }
        public ICommand DeleteConcourseCommand
        {
            get
            {
                if (_delConcourse == null)
                {
                    _delConcourse = new RelayCommand(p => DeleteConcourse());
                }

                return _delConcourse;
            }
        }
        #endregion

        #endregion

        #region Methods

        #region Methods For Search

        private void SearchEntrants()
        {
            if (searchTxtEntrant != "")
            {
                Entrants = dbla.GetAllEntrantsByName(SearchTxtEntrant);
                EnabledEntrant = true;
                EnabledDropDownEntrant = true;
            }
            else
            {
                EnabledEntrant = false;
                EnabledDropDownEntrant = false;
            }
        }

        private void SearchEducationalIns()
        {
            if (searchTxtEducationalIns != "")
            {
                EducationalIns = dbl.GetAllEducationalInsByName(searchTxtEducationalIns);
               // MessageBox.Show(EducationIns[0].ToString());
                EnabledEducationalIns = true;
                EnabledDropDownEducationalIns = true;
            }
            else
            {
                EnabledEducationalIns = false;
                EnabledDropDownEducationalIns = false;
            }

        }

        private void SearchSpecialization()
        {
            if (searchTxtSpecialization != "")
            {
                // foreach(Entrant entrant in entrantsList)
            }

        }

        private void SearchEntry()
        {
            if (searchTxtEntry != "")
            {
                // foreach(Entrant entrant in entrantsList)
            }

        }

        private void SearchConcourse()
        {
            if (searchTxtConcourse != "")
            {
                if (selectedYear != 0)
                {
                    if (checkRBtnIsFreeNo == false & checkRBtnIsFreeYes == false)
                    {
                        MessageBox.Show("Выберите платное или бесплатное обучение.");
                        return;
                    }
                    if (checkRBtnIsIntramuralNo == false & checkRBtnIsIntramuralYes == false)
                    {
                        MessageBox.Show("Выберите очное или заочное обучение.");
                        return;
                    }
                    Concourses = dbl.GetAllConcourseWithEduAndSpec(SelectedYear, Convert.ToInt32(checkRBtnIsFreeYes),
                        Convert.ToInt32(checkRBtnIsIntramuralYes), SearchTxtConcourse);
                    EnabledConcourse = true;
                    EnabledDropDownConcourse = true;

                }
                else MessageBox.Show("Выберите год");
            }
            else {
                EnabledConcourse = false;
                EnabledDropDownConcourse = false;
            }

        }

        #endregion

        #region Methods For Delete
      
        private void DeleteCategory()
        {
            if (SelectedCategory != null)
            {
                dbla.DeleteCategory(SelectedCategory.Id);
                Categories.Remove(SelectedCategory);
            }
            else MessageBox.Show("Элемент не выбран");
        }
      
        private void DeleteEntrant()
        {
            if (SelectedEntrant != null)
            {
                dbla.DeleteEntrant(SelectedEntrant.Id);
                Entrants.Clear();

            }
            else MessageBox.Show("Элемент не выбран");
        }
     
        private void DeleteEducationalIns()
        {
            if (SelectedEducationalIns != null)
            {
                dbla.DeleteEducationalIns(SelectedEducationalIns.Id);
                EducationalIns.Clear();
            }
            else MessageBox.Show("Элемент не выбран");
        }
       
        private void DeleteConcourse()
        {
            if (SelectedConcourseWithEduAndSpec != null)
            { 
                dbla.DeleteConcourse(SelectedConcourseWithEduAndSpec.Id);
                Concourses.Clear();
            }
           
        }
        private void DeleteEntry()
        {

        }
        private void DeleteSpecialization()
        {

        }
        #endregion

        #endregion

        public AdminPageViewModel()
        {
            categories = dbl.GetAllCategory();
            entrantsDG = dbla.GetAllEntrantsPagination(1,20);
            educationInsDG = dbl.GetAllEducationalIns();
           // entrantsList = dbla.GetAllEntrants();
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
