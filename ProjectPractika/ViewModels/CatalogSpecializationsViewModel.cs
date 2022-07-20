using ProjectPractika.DataBase;
using ProjectPractika.Models;
using ProjectPractika.Models.Helper_models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectPractika.ViewModels
{
    internal class CatalogSpecializationsViewModel : ObservableObject, IPageViewModel
   {
        #region IPageViewModel
        string visibility = "Visible";

        public string Name
        {
            get
            {
                return "Каталог специальностей";
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

        ICommand _searchBySpecName, _searchByLetter, _searchByConcourse;

        char selectedLetter;
        string searchTxtSpecialization;
        int selectedYear;
        bool checkRBtnIsFreeYes = false, checkRBtnIsFreeNo = false,
          checkRBtnIsIntramuralYes = false, checkRBtnIsIntramuralNo = false;

        ObservableCollection<Category> categories = new ObservableCollection<Category>();
        ObservableCollection<ConcoursesForView> concourses = new ObservableCollection<ConcoursesForView>();

        Category selectedCategory;
        ConcoursesForView selectedConcourse;

        List<char> alphabet = new List<char>() 
        {
            'А','Б','В','Г','Д','Ж','З','И','К','Л','М','Н','О','П','Р','С','Т','У','Ф','Х','Ц','Ч','Э','Ю','Я'
        };
        List<int> years = new List<int>() { 2019, 2020, 2021, 2022 };

        public string SearchTxtSpecialization
        {
            get { return searchTxtSpecialization; }
            set { searchTxtSpecialization = value; OnPropertyChanged(); }
        }

        #region Collections

        public List<char> Alphabet
        {
            get { return alphabet; }
            set { alphabet = value; OnPropertyChanged(); }
        }
        public List<int> Years
        {
            get { return years; }
            set { years = value; }
        }
        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set { categories = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ConcoursesForView> Concourses
        {
            get { return concourses; }
            set { concourses = value; OnPropertyChanged(); }
        }
        #endregion

        #region selectedItem
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value; OnPropertyChanged(); }
        }
        public ConcoursesForView SelectedConcourse
        {
            get { return selectedConcourse; }
            set { selectedConcourse = value; OnPropertyChanged(); }
        }
        public int SelectedYear
        {
            get { return selectedYear; }
            set { selectedYear = value; OnPropertyChanged(); }
        }
        public char SelectedLetter
        {
            get { return selectedLetter; }
            set { selectedLetter = value; OnPropertyChanged(); }
        }
        #endregion

        #region RadioButton Check
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

        #region ICommands
        public ICommand SearchBySpecNameCommand
        {
            get
            {
                if (_searchBySpecName == null)
                {
                    _searchBySpecName = new RelayCommand(p => SearchBySpecName());
                }

                return _searchBySpecName;
            }
        }
        public ICommand SearchByLetterCommand
        {
            get
            {
                if (_searchByLetter == null)
                {
                    _searchByLetter = new RelayCommand(p => SearchByLetter());
                }

                return _searchByLetter;
            }
        }
        public ICommand SearchByConcourseCommand
        {
            get
            {
                if (_searchByConcourse == null)
                {
                    _searchByConcourse = new RelayCommand(p => SearchByConcourse());
                }

                return _searchByConcourse;
            }
        }
        #endregion

        #region Methods 
        public void SearchBySpecName()
        {

        }
        public void SearchByLetter()
        {

        }
        public void SearchByConcourse()
        {

        }
        #endregion
        public CatalogSpecializationsViewModel()
        {
            Categories = dbl.GetAllCategory();
            SelectedLetter = alphabet[0];
            Concourses.Add(new ConcoursesForView(1,"specname","as","cat",true,true,2019,123));
        }
   }
}
