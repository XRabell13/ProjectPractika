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
       
        #region Enabled
        bool enabledEntrant = false, enabledEntry = false, enabledEducationalIns = false,
           enabledSpecialization = false, enabledSpecializationOne = false, enabledConcourse = false;

        bool enabledSpecializationEduByAddConcourse = false;
        bool enabledAddEntryEntrants = false, enabledAddEntryConcourses = false;

        bool enabledEduSpecSpecialization = false, enabledEduSpecEducation = false;

        bool enabledDropDownEntrant = false, enabledDropDownEntry = false, enabledDropDownEducationalIns = false,
         enabledDropDownSpecialization = false, enabledDropDownSpecializationOne = false, enabledDropDownConcourse = false;

        bool enabledDropDownSpecializationEduByAddConcourse = false;

        bool enabledDropDownAddEntryEntrants = false, enabledDropDownAddEntryConcourses = false;

        bool enabledDropDownEduSpecSpecialization = false, enabledDropDownEduSpecEducation = false;

        #endregion

        #region Checks
        bool checkRBtnIsFreeYes = false, checkRBtnIsFreeNo = false, 
            checkRBtnIsIntramuralYes = false, checkRBtnIsIntramuralNo = false;

        bool checkRBtnEntryIsFreeYes = false, checkRBtnEntryIsFreeNo = false,
            checkRBtnEntryIsIntramuralYes = false, checkRBtnEntryIsIntramuralNo = false;

        bool checkRBtnAddIsFreeYes = false, checkRBtnAddIsFreeNo = false,
           checkRBtnAddIsIntramuralYes = false, checkRBtnAddIsIntramuralNo = false;
        #endregion

        #region Searches
        string searchTxtEntrant = "", searchTxtEntry = "", searchTxtEducationalIns = "",
             searchTxtSpecialization = "", searchTxtSpecializationOne = "", searchTxtConcourse = "";

        string searchTxtSpecializationEduByAddConcourse = "";

        string searchTxtAddEntryConcourse = "";
        string searchTxtAddEntryEntrant = "";

        string searchTxtAddEduSpecSpecialization = "", searchTxtAddEduSpecEducation = "";
        #endregion

        int infoAddMaxBall, infoAddYear, infoAddCountSeats;
        string infoAddFullName = "", infoAddPassport = "";
        string infoAddInsName = "", infoAddInsAddress = "";
        string infoAddCategory = "";
        string infoAddSpecializationName = "";

        #region ICommand fields
        private ICommand _delCategory, _delEntrant, _delEducationalIns, _delEntry, _delConcourse, _delSpecialization, _delSpecializationOne;
        private ICommand _addCategory, _addEntrant, _addEducationalIns, _addEntry, _addConcourse, _addSpecialization, _addEduSpec;
        #endregion

        #region Collections for deleteing
      
        List<int> years = new List<int>() { 2019, 2020, 2021, 2022 };
        
        ObservableCollection<Category> categories = new ObservableCollection<Category>();
        ObservableCollection<Entrant> entrants = new ObservableCollection<Entrant>();
        ObservableCollection<EducationIns> educationIns = new ObservableCollection<EducationIns>();
        ObservableCollection<ConcourseWithEduAndSpec> concourses = new ObservableCollection<ConcourseWithEduAndSpec>();
        ObservableCollection<EntryWithInfo> entries = new ObservableCollection<EntryWithInfo>();
        ObservableCollection<SpecializationWithInfo> specializations = new ObservableCollection<SpecializationWithInfo>();
        ObservableCollection<Specialization> specializationsDelOne = new ObservableCollection<Specialization>();

        #endregion

        #region Collections for adding

        ObservableCollection<SpecializationEducation> specializationEducations = new ObservableCollection<SpecializationEducation>();

        ObservableCollection<Entrant> entrantsByEntry = new ObservableCollection<Entrant>();
        ObservableCollection<ConcourseWithEduAndSpec> concoursesByEntry = new ObservableCollection<ConcourseWithEduAndSpec>();

        ObservableCollection<Specialization> eduSpecAddSpecializations = new ObservableCollection<Specialization>();
        ObservableCollection<EducationIns> eduSpecAddEducations = new ObservableCollection<EducationIns>();
        #endregion

        ObservableCollection<Entrant> entrantsDG = new ObservableCollection<Entrant>();
        ObservableCollection<EducationIns> educationInsDG = new ObservableCollection<EducationIns>();

        #region SelectedItems for deleting

        int selectedYear, selectedYearEntry;
        Category selectedCategory;
       
        Entrant selectedEntrant;
        EducationIns selectedEducationIns;
        ConcourseWithEduAndSpec selectedConcourseWithEduAndSpec;
        EntryWithInfo selectedEntryWithInfo;
        SpecializationWithInfo selectedSpecializationWithInfo;
        Specialization selectedSpecializationOne;

        #endregion

        #region SelectedItems for adding

        int selectedAddConcourseYear;
        Category selectedCategoryBySpec;
        SpecializationEducation selectedSpecializationEducation;

        Entrant selectedEntrantByEntry;
        ConcourseWithEduAndSpec selectedConcourseByEntry;

        Specialization selectedEduSpecSpecialization;
        EducationIns selectedEduSpecEducation;

        #endregion


        #endregion

        #region Properties / Commands

        #region EnabledProperties Binding

        #region COMBOBOX ENABLED
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
        public bool EnabledSpecializationOne
        {
            get { return enabledSpecializationOne; }
            set { enabledSpecializationOne = value; OnPropertyChanged(); }
        }
        public bool EnabledConcourse
        {
            get { return enabledConcourse; }
            set { enabledConcourse = value; OnPropertyChanged(); }
        }
        public bool EnabledSpecializationEduByAddConcourse
        {
            get { return enabledSpecializationEduByAddConcourse; }
            set { enabledSpecializationEduByAddConcourse = value; OnPropertyChanged(); }
        }
        // add combobox entry
        public bool EnabledAddEntryEntrants
        { 
            get { return enabledAddEntryEntrants; }
            set { enabledAddEntryEntrants = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownAddEntryEntrants
        {
            get { return enabledDropDownAddEntryEntrants; }
            set { enabledDropDownAddEntryEntrants = value; OnPropertyChanged(); }
        }
        public bool EnabledAddEntryConcourses { 
            get { return enabledAddEntryConcourses; }
            set { enabledAddEntryConcourses = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDowmAddEntryConcourses
        {
            get { return enabledDropDownAddEntryConcourses; }
            set { enabledDropDownAddEntryConcourses = value; OnPropertyChanged(); }
        }
        // add eduspec
        public bool EnabledEduSpecSpecialization
        {
            get { return enabledEduSpecSpecialization; }
            set { enabledEduSpecSpecialization = value; OnPropertyChanged(); }
        }
        public bool EnabledEduSpecEducation
        {
            get { return enabledEduSpecEducation; }
            set { enabledEduSpecEducation = value; OnPropertyChanged(); }
        }
        #endregion
        #region DROPDOWN ENABLED
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
        public bool EnabledDropDownSpecializationOne
        {
            get { return enabledDropDownSpecializationOne; }
            set { enabledDropDownSpecializationOne = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownConcourse
        {
            get { return enabledDropDownConcourse; }
            set { enabledDropDownConcourse = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownSpecializationEduByAddConcourse 
        {
            get { return enabledDropDownSpecializationEduByAddConcourse; }
            set { enabledDropDownSpecializationEduByAddConcourse = value; OnPropertyChanged(); }
        }
        //add eduSpec
        public bool EnabledDropDownEduSpecSpecialization
        {
            get { return enabledDropDownEduSpecSpecialization; }
            set { enabledDropDownEduSpecSpecialization = value; OnPropertyChanged(); }
        }
        public bool EnabledDropDownEduSpecEducation
        {
            get { return enabledDropDownEduSpecEducation; }
            set { enabledDropDownEduSpecEducation = value; OnPropertyChanged(); }
        }

        #endregion
        #region RADIOBUTTON ENABLED CHECKS

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
        // ------------------------------------------------------------------- 
        public bool CheckRBtnEntryIsFreeYes
        {
            get { return checkRBtnEntryIsFreeYes; }
            set { checkRBtnEntryIsFreeYes = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnEntryIsFreeNo
        {
            get { return checkRBtnEntryIsFreeNo; }
            set { checkRBtnEntryIsFreeNo = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnEntryIsIntramuralYes
        {
            get { return checkRBtnEntryIsIntramuralYes; }
            set { checkRBtnEntryIsIntramuralYes = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnEntryIsIntramuralNo
        {
            get { return checkRBtnEntryIsIntramuralNo; }
            set { checkRBtnEntryIsIntramuralNo = value; OnPropertyChanged(); }
        }
        // ------------------------------------------------------------------- 
        public bool CheckRBtnAddIsFreeYes
        {
            get { return checkRBtnAddIsFreeYes; }
            set { checkRBtnAddIsFreeYes = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnAddIsFreeNo
        {
            get { return checkRBtnAddIsFreeNo; }
            set { checkRBtnAddIsFreeNo = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnAddIsIntramuralYes
        {
            get { return checkRBtnAddIsIntramuralYes; }
            set { checkRBtnAddIsIntramuralYes = value; OnPropertyChanged(); }
        }
        public bool CheckRBtnAddIsIntramuralNo
        {
            get { return checkRBtnAddIsIntramuralNo; }
            set { checkRBtnAddIsIntramuralNo = value; OnPropertyChanged(); }
        }

        #endregion

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
            set { searchTxtSpecialization = value; OnPropertyChanged();  SearchSpecialization();  }
        }
        public string SearchTxtSpecializationOne
        {
            get { return searchTxtSpecializationOne; }
            set { searchTxtSpecializationOne = value; OnPropertyChanged(); SearchSpecializationOne(); }
        }
        public string SearchTxtConcourse
        {
            get { return searchTxtConcourse; }
            set { searchTxtConcourse = value; OnPropertyChanged(); SearchConcourse(); }
        }
        public string SearchTxtSpecializationEduByAddConcourse
        {
            get { return searchTxtSpecializationEduByAddConcourse; }
            set { searchTxtSpecializationEduByAddConcourse = value; OnPropertyChanged(); SearchSpecializationEducationByAddedConcourse(); }
        }
        // entry
        public string SearchTxtAddEntryConcourse
        {
            get { return searchTxtAddEntryConcourse; }
            set { searchTxtAddEntryConcourse = value; OnPropertyChanged(); SearchEntryConcourse(); }
        }
        public string SearchTxtAddEntryEntrant
        {
            get { return searchTxtAddEntryEntrant; }
            set { searchTxtAddEntryEntrant = value; OnPropertyChanged(); SearchEntryEntrant(); }
        }
        // eduSpec
        public string SearchTxtAddEduSpecSpecialization 
        {
            get { return searchTxtAddEduSpecSpecialization; }
            set { searchTxtAddEduSpecSpecialization = value; OnPropertyChanged(); SearchEduSpecSpecialization(); }
        }
        public string SearchTxtAddEduSpecEducation
        {
            get { return searchTxtAddEduSpecEducation; }
            set { searchTxtAddEduSpecEducation = value; OnPropertyChanged(); SearchEduSpecEducation(); }
        }
        #endregion

        #region InfoAdds

        public string InfoAddFullName
        {
            get { return infoAddFullName; }
            set { infoAddFullName = value; OnPropertyChanged(); }
        }

        public string InfoAddPassport
        {
            get { return infoAddPassport; }
            set { infoAddPassport = value; OnPropertyChanged(); }
        }

        public int InfoAddMaxBall
        {
            get { return infoAddMaxBall; }
            set { infoAddMaxBall = value; OnPropertyChanged(); }
        }
        public int InfoAddYear
        {
            get { return infoAddYear; }
            set { infoAddYear = value; OnPropertyChanged(); }
        }
        public int InfoAddCountSeats
        {
            get { return infoAddCountSeats; }
            set { infoAddCountSeats = value; OnPropertyChanged(); }
        }
        public string InfoAddInsName
        {
            get { return infoAddInsName; }
            set { infoAddInsName = value; OnPropertyChanged(); }
        }
        public string InfoAddInsAddress
        {
            get { return infoAddInsAddress; }
            set { infoAddInsAddress = value; OnPropertyChanged(); }
        }
        public string InfoAddCategory
        {
            get { return infoAddCategory; }
            set { infoAddCategory = value; OnPropertyChanged(); }
        }
        public string InfoAddSpecializationName
        {
            get { return infoAddSpecializationName; }
            set { infoAddSpecializationName = value; OnPropertyChanged(); }
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
        public int SelectedYearEntry
        {
            get { return selectedYearEntry; }
            set { selectedYearEntry = value; OnPropertyChanged(); }
        }
        public int SelectedAddConcourseYear
        {
            get { return selectedAddConcourseYear; }
            set { selectedAddConcourseYear = value; OnPropertyChanged(); }
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

        public ObservableCollection<EntryWithInfo> Entries
        {
            get { return entries; }
            set { entries = value; OnPropertyChanged(); }
        }

        public EntryWithInfo SelectedEntryWithInfo
        {
            get { return selectedEntryWithInfo; }
            set { selectedEntryWithInfo = value; OnPropertyChanged(); }
        }

        public ObservableCollection<SpecializationWithInfo> Specializations
        {
            get { return specializations; }
            set { specializations = value; OnPropertyChanged(); }
        }

        public SpecializationWithInfo SelectedSpecialization
        {
            get { return selectedSpecializationWithInfo; }
            set { selectedSpecializationWithInfo = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Specialization> SpecializationsDelOne
        {
            get { return specializationsDelOne; }
            set { specializationsDelOne = value; OnPropertyChanged(); }
        }

        public Specialization SelectedSpecializationOne
        {
            get { return selectedSpecializationOne; }
            set { selectedSpecializationOne = value; OnPropertyChanged(); }
        }

        public Category SelectedCategoryBySpec
        {
            get { return selectedCategoryBySpec; }
            set { selectedCategoryBySpec = value; OnPropertyChanged(); }
        }

        public ObservableCollection<SpecializationEducation> SpecializationEducations
        {
            get { return specializationEducations; }
            set { specializationEducations = value; OnPropertyChanged(); }
        }

        public SpecializationEducation SelectedSpecializationEducation
        {
            get { return selectedSpecializationEducation; }
            set { selectedSpecializationEducation = value; OnPropertyChanged(); }
        }
        // добавление записи
        public ObservableCollection<Entrant> EntrantsByEntry
        {
            get { return entrantsByEntry; }
            set { entrantsByEntry = value; OnPropertyChanged(); }
        }

        public Entrant SelectedEntrantByEntry
        { 
            get { return selectedEntrantByEntry; }
            set { selectedEntrantByEntry = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ConcourseWithEduAndSpec> ConcoursesByEntry
        {
            get { return concoursesByEntry; }
            set { concoursesByEntry = value; OnPropertyChanged(); }
        }
        public ConcourseWithEduAndSpec SelectedConcourseByEntry
        {
            get { return selectedConcourseByEntry; }
            set { selectedConcourseByEntry = value; OnPropertyChanged(); }
        }
        // add EduSpec

        public ObservableCollection<Specialization> EduSpecAddSpecializations
        { 
            get { return eduSpecAddSpecializations; }
            set { eduSpecAddSpecializations = value; OnPropertyChanged(); }
        }
        public ObservableCollection<EducationIns> EduSpecAddEducations
        {
            get { return eduSpecAddEducations; }
            set { eduSpecAddEducations = value; OnPropertyChanged(); }
        }

        public Specialization SelectedEduSpecSpecialization
        {
            get { return selectedEduSpecSpecialization; }
            set { selectedEduSpecSpecialization = value; OnPropertyChanged(); }
        }

        public EducationIns SelectedEduSpecEducationIns
        {
            get { return selectedEduSpecEducation; }
            set { selectedEduSpecEducation = value; OnPropertyChanged(); }
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

        #region DeleteCommands
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
        public ICommand DeleteSpecializationOneCommand
        {
            get
            {
                if (_delSpecializationOne == null)
                {
                    _delSpecializationOne = new RelayCommand(p => DeleteSpecializationOne());
                }

                return _delSpecializationOne;
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

        #region AddCommands
        public ICommand AddCategoryCommand
        {
            get
            {
                if (_addCategory == null)
                {
                    _addCategory = new RelayCommand(p => AddCategory());
                }

                return _addCategory;
            }
        }
        public ICommand AddEntrantCommand
        {
            get
            {
                if (_addEntrant == null)
                {
                    _addEntrant = new RelayCommand(p => AddEntrant());
                }

                return _addEntrant;
            }
        }
        public ICommand AddEduCommand
        {
            get
            {
                if (_addEducationalIns == null)
                {
                    _addEducationalIns = new RelayCommand(p => AddEducationalIns());
                }

                return _addEducationalIns;
            }
        }
        public ICommand AddSpecializationCommand
        {
            get
            {
                if (_addSpecialization == null)
                {
                    _addSpecialization = new RelayCommand(p => AddSpecialization());
                }

                return _addSpecialization;
            }
        }
        public ICommand AddConcourseCommand
        {
            get
            {
                if (_addConcourse == null)
                {
                    _addConcourse = new RelayCommand(p => AddConcourse());
                }

                return _addConcourse;
            }
        }
        public ICommand AddEntryCommand
        {
            get
            {
                if (_addEntry == null)
                {
                    _addEntry = new RelayCommand(p => AddEntry());
                }

                return _addEntry;
            }
        }
        public ICommand AddEduSpecCommand
        {
            get
            {
                if (_addEduSpec == null)
                {
                    _addEduSpec = new RelayCommand(p => AddEduSpec());
                }

                return _addEduSpec;
            }
        }
        #endregion

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
                Specializations = dbl.GetAllSpecializations(searchTxtSpecialization);
                EnabledSpecialization = true;
                EnabledDropDownSpecialization = true;
            }
            else
            {
                EnabledSpecialization = false;
                EnabledDropDownSpecialization = false;
            }

        }

        private void SearchSpecializationOne()
        {
            if (searchTxtSpecializationOne != "")
            {
                SpecializationsDelOne = dbl.GetAllSpecializationsByName(searchTxtSpecializationOne);
                EnabledSpecializationOne = true;
                EnabledDropDownSpecializationOne = true;
            }
            else
            {
                EnabledSpecializationOne = false;
                EnabledDropDownSpecializationOne = false;
            }

        }

        private void SearchEntry()
        {
            if (searchTxtEntry != "")
            {
                if (selectedYearEntry != 0)
                {
                    if (checkRBtnEntryIsFreeNo == false & checkRBtnEntryIsFreeYes == false)
                    {
                        MessageBox.Show("Выберите платное или бесплатное обучение.");
                        return;
                    }
                    if (checkRBtnEntryIsIntramuralNo == false & checkRBtnEntryIsIntramuralYes == false)
                    {
                        MessageBox.Show("Выберите очное или заочное обучение.");
                        return;
                    }

                    Entries = dbla.GetAllEntryWithInfo(SelectedYearEntry, Convert.ToInt32(checkRBtnEntryIsFreeYes),
                        Convert.ToInt32(checkRBtnEntryIsIntramuralYes), SearchTxtEntry);
                   
                    EnabledEntry = true;
                    EnabledDropDownEntry = true;

                }
                else MessageBox.Show("Выберите год");
            }
            else
            {
                EnabledEntry = false;
                EnabledDropDownEntry = false;
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

        private void SearchSpecializationEducationByAddedConcourse() {
            if (SearchTxtSpecializationEduByAddConcourse != "")
            {
                SpecializationEducations = dbla.GetAllSpecializationEducationByInfo(SearchTxtSpecializationEduByAddConcourse);
                EnabledDropDownSpecializationEduByAddConcourse = true;
                EnabledSpecializationEduByAddConcourse = true;
            }
            else {
                EnabledDropDownSpecializationEduByAddConcourse = false;
                EnabledSpecializationEduByAddConcourse = false;
            }
        }

        private void SearchEntryEntrant(){
            if (SearchTxtAddEntryEntrant != "")
            {
                EntrantsByEntry = dbla.GetAllEntrantsByName(SearchTxtAddEntryEntrant);
                EnabledAddEntryEntrants = true;
                EnabledDropDownAddEntryEntrants = true;
            }
            else {
                EnabledAddEntryEntrants = false;
                EnabledDropDownAddEntryEntrants = false;
            }
        }

        private void SearchEntryConcourse()
        {
            if (SearchTxtAddEntryConcourse != "")
            {
                ConcoursesByEntry = dbl.GetAllConcourseWithInfo(SearchTxtAddEntryConcourse);
                EnabledAddEntryConcourses = true;
                EnabledDropDowmAddEntryConcourses = true;
            }
            else {
                EnabledAddEntryConcourses = false;
                EnabledDropDowmAddEntryConcourses = false;
            }
        }
        //eduspecsearch

        private void SearchEduSpecSpecialization()
        {
            if (SearchTxtAddEduSpecSpecialization != "")
            {
                EduSpecAddSpecializations = dbl.GetAllSpecializationsBasic(SearchTxtAddEduSpecSpecialization);

               // MessageBox.Show(EduSpecAddSpecializations1[0].ToString());
                EnabledEduSpecSpecialization = true;
                EnabledDropDownEduSpecSpecialization = true;
            }
            else {
                EnabledEduSpecSpecialization = false;
                EnabledDropDownEduSpecSpecialization = false;
            }
        }
        private void SearchEduSpecEducation()
        {
            if (SearchTxtAddEduSpecEducation != "")
            {
                EduSpecAddEducations = dbl.GetAllEducationalInsByName(SearchTxtAddEduSpecEducation);
                EnabledEduSpecEducation = true;
                EnabledDropDownEduSpecEducation = true;
            }
            else {
                EnabledEduSpecEducation = false;
                EnabledDropDownEduSpecEducation = false;
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
            else MessageBox.Show("Элемент не выбран");

        }
     
        private void DeleteEntry()
        {
            if(SelectedEntryWithInfo != null)
            { 
                dbla.DeleteEntry(SelectedEntryWithInfo.Id);
                Entries.Clear();
            }
            else MessageBox.Show("Элемент не выбран");
        }
      
        private void DeleteSpecialization()
        {
            if (SelectedSpecialization != null)
            {
                dbla.DeleteSpecializationByEdu(SelectedSpecialization.SpecEduId);
                Specializations.Clear();
            }
            else MessageBox.Show("Элемент не выбран");
        }
      
        private void DeleteSpecializationOne()
        {
            if (SelectedSpecializationOne != null)
            {
                dbla.DeleteSpecialization(SelectedSpecializationOne.Id);
                SpecializationsDelOne.Clear();
            }
            else MessageBox.Show("Элемент не выбран");
        }

        #endregion

        #region Methods For Add

        private void AddCategory()
        {
            if (InfoAddCategory != "")
            {
               bool added = dbla.AddCategory(InfoAddCategory);
                if (added) MessageBox.Show("Категория " + InfoAddCategory + " добавлена");
                Categories.Clear();
                Categories = dbl.GetAllCategory();

            }
            else MessageBox.Show("Впишите название категории");
        }

        private void AddEntrant()
        {
            if (InfoAddFullName != "" & InfoAddPassport != "" & InfoAddMaxBall != 0 & InfoAddYear != 0)
            {
                bool added = dbla.AddEntrant(InfoAddFullName, InfoAddPassport, InfoAddMaxBall, InfoAddYear);
                if (added) MessageBox.Show("Абитуриент " + InfoAddFullName + " добавлен");

            }
            else MessageBox.Show("Заполните все поля");
        }

        private void AddEducationalIns()
        {
            if (InfoAddInsName != "" & InfoAddInsAddress != "")
            {
                bool added = dbla.AddEduIns(InfoAddInsName, InfoAddInsAddress);
                if (added) MessageBox.Show("Учебное учреждение " + InfoAddInsName + " добавлено");

            }
            else MessageBox.Show("Заполните все поля");
        }

        private void AddSpecialization()
        {
            if (InfoAddSpecializationName != "" & SelectedCategoryBySpec != null)
            {
                bool added = dbla.AddSpecialization(InfoAddSpecializationName, SelectedCategoryBySpec.Id);
                if (added) MessageBox.Show("Специальность " + InfoAddSpecializationName + " добавлена");

            }
        }
       
        private void AddConcourse()
        {
            if (InfoAddCountSeats != 0)
            {
                if (SelectedAddConcourseYear != 0)
                {
                    if (CheckRBtnAddIsFreeNo == false & CheckRBtnAddIsFreeYes == false)
                    {
                        MessageBox.Show("Выберите платное или бесплатное обучение.");
                        return;
                    }
                    if (CheckRBtnAddIsIntramuralNo == false & CheckRBtnAddIsIntramuralYes == false)
                    {
                        MessageBox.Show("Выберите очное или заочное обучение.");
                        return;
                    }
                    if (SelectedSpecializationEducation != null)
                    {
                      
                        bool added = dbla.AddConcourse(InfoAddCountSeats,
                           Convert.ToInt32(CheckRBtnAddIsFreeYes), Convert.ToInt32(CheckRBtnAddIsIntramuralYes),
                           SelectedAddConcourseYear, SelectedSpecializationEducation.Id);
                        if (added) MessageBox.Show("Конкурс добавлен");
                    }
                    else MessageBox.Show("Выберите специальность");
                }
                else MessageBox.Show("Выберите год");
            }
            else MessageBox.Show("Впишите количество мест");

        }
        
        private void AddEntry()
        {
            if (SelectedEntrantByEntry != null & SelectedConcourseByEntry != null)
            {
               // MessageBox.Show("Запись: " + SelectedConcourseByEntry.ToString() + "\n" + SelectedEntrantByEntry.ToString());
                bool added = dbla.AddEntry(SelectedEntrantByEntry.Id, SelectedConcourseByEntry.Id);
                if (added) MessageBox.Show("Запись добавлена: " + SelectedConcourseByEntry.ToString() + "\n" + SelectedEntrantByEntry.ToString());
            }
            else {
                MessageBox.Show("Выберите значения");
            }
        }
        private void AddEduSpec() 
        {
            if (SelectedEduSpecSpecialization != null & SelectedEduSpecEducationIns != null)
            {
                bool added = dbla.AddEduSpec(SelectedEduSpecSpecialization.Id, SelectedEduSpecEducationIns.Id);
                if (added) MessageBox.Show("Специальность " +SelectedEduSpecSpecialization.SpecName + " добавлена в " + SelectedEduSpecEducationIns.InsName );  
            }
            else
            {
                MessageBox.Show("Выберите значения");
            }
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
    }
}
