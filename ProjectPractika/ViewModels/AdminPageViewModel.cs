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
        const int offset = 0, limit = 20;

        #region Fields
       
        #region Enabled
        bool enabledEntrant = false, enabledEntry = false, enabledEducationalIns = false,
           enabledSpecialization = false, enabledSpecializationOne = false, enabledConcourse = false;

        bool enabledSpecializationEduByAddConcourse = false;
        bool enabledAddEntryEntrants = false, enabledAddEntryConcourses = false;

        bool enabledEduSpecSpecialization = false, enabledEduSpecEducation = false;

        bool enabledEduSpecConcourseDG = false;

        bool enabledEntryEntrantDGCombobox = false, enabledEntryConcourseDGCombobox = false;

        // dropdown
        bool enabledDropDownEntrant = false, enabledDropDownEntry = false, enabledDropDownEducationalIns = false,
         enabledDropDownSpecialization = false, enabledDropDownSpecializationOne = false, enabledDropDownConcourse = false;

        bool enabledDropDownSpecializationEduByAddConcourse = false;

        bool enabledDropDownAddEntryEntrants = false, enabledDropDownAddEntryConcourses = false;

        bool enabledDropDownEduSpecSpecialization = false, enabledDropDownEduSpecEducation = false;

        bool enabledDropDownEduSpecConcourseDG = false;


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

        string searchTxtDGSpecializationEduCombobox = "", searchTxtDGSpecialization = "", searchTxtDGEducation = "", 
            searchTxtDGEntrant = "", searchTxtDGConcourse = "", searchTxtDGEntry = "";
        string searchTxtDGEntrantCombobox = "", searchTxtDGConcourseCombobox = "";
        #endregion

        #region infoAdd
        int infoAddMaxBall, infoAddYear, infoAddCountSeats;
        string infoAddFullName = "", infoAddPassport = "";
        string infoAddInsName = "", infoAddInsAddress = "";
        string infoAddCategory = "";
        string infoAddSpecializationName = "";
        #endregion


        #region count pagination

        int numPageEntrantDG = 1, numPageEduInsDG = 1, numPageSpecializationDG = 1, numPageConcourseDG = 1, numPageEntryDG = 1;

        public int NumPageEntrantDG
        {
            get { return numPageEntrantDG; }
            set { numPageEntrantDG = value; OnPropertyChanged(); }
        }
        public int NumPageEduInsDG
        {
            get { return numPageEduInsDG; }
            set { numPageEduInsDG = value; OnPropertyChanged(); }
        }
        public int NumPageSpecializationDG
        {
            get { return numPageSpecializationDG; }
            set { numPageSpecializationDG = value; OnPropertyChanged(); }
        }
        public int NumPageConcourseDG
        {
            get { return numPageConcourseDG; }
            set{ numPageConcourseDG = value; OnPropertyChanged();}
        }
        public int NumPageEntryDG
        {
            get { return numPageEntryDG; }
            set { numPageEntryDG = value; OnPropertyChanged(); }
        }
        #endregion

        #region ICommand fields
        private ICommand _delCategory, _delEntrant, _delEducationalIns, _delEntry, _delConcourse, _delSpecialization, _delSpecializationOne;
        private ICommand _addCategory, _addEntrant, _addEducationalIns, _addEntry, _addConcourse, _addSpecialization, _addEduSpec;
        private ICommand _updateCategory, _updateEntrant, _updateEducationalIns, _updateEntryEntrant, _updateEntryConcourse, _updateConcourse, 
            _updateSpecializationName, _updateSpecializationCategory, _updateConcourseEduSpec;
        private ICommand _searchEntrantDG, _searchEducationalInsDG, _searchEntryDG, _searchConcourseDG, _searchSpecializationDG;

        private ICommand _backPagEntrantDG, _backPagEducationalInsDG, _backPagEntryDG, _backPagConcourseDG, _backPagSpecializationDG;
        private ICommand _forwardPagEntrantDG, _forwardPagEducationalInsDG, _forwardPagEntryDG, _forwardPagConcourseDG,
            _forwardPagSpecializationDG;
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

        #region Collections/Objects for updating

        ObservableCollection<Entrant> entrantsDG = new ObservableCollection<Entrant>();
        ObservableCollection<EducationIns> educationInsDG = new ObservableCollection<EducationIns>();
        ObservableCollection<Category> categoriesDG = new ObservableCollection<Category>();
        ObservableCollection<SpecializationDG> specializationsDG = new ObservableCollection<SpecializationDG>();
        ObservableCollection<ConcourseWithEduAndSpec> concoursesDG = new ObservableCollection<ConcourseWithEduAndSpec>();
        ObservableCollection<EntryDG> entriesDG = new ObservableCollection<EntryDG>();

        ObservableCollection<Specialization> specializationsDGCombobox = new ObservableCollection<Specialization>();
        ObservableCollection<SpecializationEducation> specializationsEduDGcombobox = new ObservableCollection<SpecializationEducation>();
        ObservableCollection<Entrant> entrantsDGCombobox = new ObservableCollection<Entrant>();
        ObservableCollection<ConcourseWithEduAndSpec> concoursesDGCombobox = new ObservableCollection<ConcourseWithEduAndSpec>();

        Entrant selectedEntrantDG;
        Entrant selectedEntrantDGCombobox;
        EducationIns selectedEducationInsDG;
        Category selectedCategoryDG;
        Category selectedCategoryDGCombobox;
        SpecializationDG selectedSpecializationDG;
        Specialization selectedSpecializationDGCombobox;
        SpecializationEducation selectedSpecializationEduDGCombobox;
        ConcourseWithEduAndSpec selectedConcourseDG;
        ConcourseWithEduAndSpec selectedConcourseDGCombobox;
        EntryDG selectedEntryDG;

        #endregion

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

        #region SelectedItems for updating



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
        // DG
        public bool EnabledEduSpecConcourseDG
        {
            get { return enabledEduSpecConcourseDG; }
            set { enabledEduSpecConcourseDG = value; OnPropertyChanged(); }
        }

        public bool EnabledDropDownEduSpecConcourseDG
        {
            get { return enabledDropDownEduSpecConcourseDG; }
            set { enabledDropDownEduSpecConcourseDG = value; OnPropertyChanged(); }
        }
        public bool EnabledEntryEntrantDGCombobox
        {
            get { return enabledEntryEntrantDGCombobox; }
            set { enabledEntryEntrantDGCombobox = value; OnPropertyChanged(); }
        }
        public bool EnabledEntryConcourseDGCombobox
        {
            get { return enabledEntryConcourseDGCombobox; }
            set { enabledEntryConcourseDGCombobox = value; OnPropertyChanged(); }
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

        #region add/delete searchTxt
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

        #region datagrid search text properties

        public string SearchTxtDGSpecializationEduCombobox
        {
            get { return searchTxtDGSpecializationEduCombobox; }
            set { searchTxtDGSpecializationEduCombobox = value; OnPropertyChanged(); SearchDGSpecializationEduCombobox(); }
        }
        public string SearchTxtDGSpecialization
        {
            get { return searchTxtDGSpecialization; }
            set { searchTxtDGSpecialization = value; OnPropertyChanged(); }
        }
        public string SearchTxtDGEducation
        {
            get { return searchTxtDGEducation; }
            set { searchTxtDGEducation = value; OnPropertyChanged(); }
        }
        public string SearchTxtDGEntrant
        {
            get { return searchTxtDGEntrant; }
            set { searchTxtDGEntrant = value; OnPropertyChanged();  }
        }
        public string SearchTxtDGConcourse
        {
            get { return searchTxtDGConcourse; }
            set { searchTxtDGConcourse = value; OnPropertyChanged();  }
        }
        // update entry
        public string SearchTxtDGEntrantCombobox
        {
            get { return searchTxtDGEntrantCombobox; }
            set { searchTxtDGEntrantCombobox = value; OnPropertyChanged(); SearchDGEntrantCombobox(); }
        }
        public string SearchTxtDGConcourseCombobox
        {
            get { return searchTxtDGConcourseCombobox; }
            set { searchTxtDGConcourseCombobox = value; OnPropertyChanged(); SearchDGConcourseCombobox(); }
        }

        public string SearchTxtDGEntry
        {
            get { return searchTxtDGEntry; }
            set { searchTxtDGEntry = value; OnPropertyChanged();  }
        }
        #endregion

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

        #region SelectedItem / ComboBox-Delete-Add Bindings
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

       // DATAGRID 
        #region DataGrid ItemsSource/SlecetedItem Bindings

        public ObservableCollection<Entrant> EntrantsDG
        {
            get { return entrantsDG; }
            
            set { entrantsDG = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Entrant> EntrantsDGCombobox
        {
            get { return entrantsDGCombobox; }

            set { entrantsDGCombobox = value; OnPropertyChanged(); }
        }

        public ObservableCollection<EducationIns> EducationalInsDG
        {
            get { return educationInsDG; }
            set { educationInsDG = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Category> CategoriesDG
        {
            get { return categoriesDG; }
            set { categoriesDG = value; OnPropertyChanged(); }
        }
        public ObservableCollection<SpecializationDG> SpecializationsDG
        {
            get { return specializationsDG; }
            set { specializationsDG = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ConcourseWithEduAndSpec> ConcoursesDG
        {
            get { return concoursesDG; }
            set { concoursesDG = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ConcourseWithEduAndSpec> ConcoursesDGCombobox
        {
            get { return concoursesDGCombobox; }
            set { concoursesDGCombobox = value; OnPropertyChanged(); }
        }
        public ObservableCollection<EntryDG> EntriesDG
        {
            get { return entriesDG; }
            set { entriesDG = value; OnPropertyChanged(); }
        }

        public ObservableCollection<SpecializationEducation> SpecializationsEduDGcombobox
        {
            get { return specializationsEduDGcombobox; }
            set { specializationsEduDGcombobox = value; OnPropertyChanged(); }
        }

        #region SelectedItem / DG Update Bindings

        public Entrant SelectedEntrantDG
        {
            get { return selectedEntrantDG; }
            set { selectedEntrantDG = value; OnPropertyChanged(); } 
        }
        public Entrant SelectedEntrantDGCombobox
        {
            get { return selectedEntrantDGCombobox; }
            set { selectedEntrantDGCombobox = value; OnPropertyChanged(); }
        }
        public EducationIns SelectedEducationInsDG
        {
            get { return selectedEducationInsDG; }
            set { selectedEducationInsDG = value; OnPropertyChanged(); }
        }
        public Category SelectedCategoryDG
        {
            get { return selectedCategoryDG; }
            set { selectedCategoryDG = value; OnPropertyChanged(); }
        }
        public Category SelectedCategoryDGCombobox
        {
            get { return selectedCategoryDGCombobox; }
            set { selectedCategoryDGCombobox = value; OnPropertyChanged(); }
        }
        public SpecializationDG SelectedSpecializationDG
        {
            get { return selectedSpecializationDG; }
            set { selectedSpecializationDG = value; OnPropertyChanged(); }
        }
        public Specialization SpecializationsDGCombobox
        {
            get { return SpecializationsDGCombobox; }
            set { SpecializationsDGCombobox = value; OnPropertyChanged(); }
        }
        public Specialization SelectedSpecializationDGCombobox
        {
            get { return selectedSpecializationDGCombobox; }
            set { selectedSpecializationDGCombobox = value; OnPropertyChanged(); }
        }
        public SpecializationEducation SelectedSpecializationEduDGCombobox
        {
            get { return selectedSpecializationEduDGCombobox; }
            set { selectedSpecializationEduDGCombobox = value; OnPropertyChanged(); }
        }
        public ConcourseWithEduAndSpec SelectedConcourseDG
        {
            get { return selectedConcourseDG; }
            set { selectedConcourseDG = value; OnPropertyChanged(); }
        }
        public ConcourseWithEduAndSpec SelectedConcourseDGCombobox
        {
            get { return selectedConcourseDGCombobox; }
            set { selectedConcourseDGCombobox = value; OnPropertyChanged(); }
        }
        public EntryDG SelectedEntryDG
        {
            get { return selectedEntryDG; }
            set { selectedEntryDG = value; OnPropertyChanged(); }
        }
        #endregion

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

        #region UpdateCommand

        public ICommand UpdateEntrantCommand
        {
            get
            {
                if (_updateEntrant == null)
                {
                    _updateEntrant = new RelayCommand(p => UpdateEntrant());
                }

                return _updateEntrant;
            }
        }
        public ICommand UpdateCategoryCommand
        {
            get
            {
                if (_updateCategory == null)
                {
                    _updateCategory = new RelayCommand(p => UpdateCategory());
                }

                return _updateCategory;
            }
        }
        public ICommand UpdateEduInsCommand
        {
            get
            {
                if (_updateEducationalIns == null)
                {
                    _updateEducationalIns = new RelayCommand(p => UpdateEduIns());
                }

                return _updateEducationalIns;
            }
        }
        public ICommand UpdateSpecializationCommand
        {
            get
            {
                if (_updateSpecializationName == null)
                {
                    _updateSpecializationName = new RelayCommand(p => UpdateSpecialization());
                }

                return _updateSpecializationName;
            }
        }
        public ICommand UpdateSpecializationCategoryCommand
        {
            get
            {
                if (_updateSpecializationCategory == null)
                {
                    _updateSpecializationCategory = new RelayCommand(p => UpdateSpecializationCategory());
                }

                return _updateSpecializationCategory;
            }
        }
        public ICommand UpdateConcourseCommand
        {
            get
            {
                if (_updateConcourse == null)
                {
                   _updateConcourse  = new RelayCommand(p => UpdateConcourse());
                }

                return _updateConcourse;
            }
        }
        public ICommand UpdateConcourseEduSpecCommand
        {
            get
            {
                if (_updateConcourseEduSpec == null)
                {
                    _updateConcourseEduSpec = new RelayCommand(p => UpdateConcourseEduSpec());
                }

                return _updateConcourseEduSpec;
            }
        }
        public ICommand UpdateEntryEntrantCommand
        {
            get
            {
                if (_updateEntryEntrant == null)
                {
                    _updateEntryEntrant = new RelayCommand(p => UpdateEntryEntrant());
                }

                return _updateEntryEntrant;
            }
        }
        public ICommand UpdateEntryConcourseCommand
        {
            get
            {
                if (_updateEntryConcourse == null)
                {
                    _updateEntryConcourse = new RelayCommand(p => UpdateEntryConcourse());
                }

                return _updateEntryConcourse;
            }
        }
        #endregion

        #region Commands For Searches
        public ICommand SearchEntrantCommand
        {
            get
            {
                if (_searchEntrantDG == null)
                {
                    _searchEntrantDG = new RelayCommand(p => SearchEntrantDG());
                }

                return _searchEntrantDG;
            }
        }
      
        public ICommand SearchEduInsCommand
        {
            get
            {
                if (_searchEducationalInsDG == null)
                {
                    _searchEducationalInsDG = new RelayCommand(p => SearchEduInsDG());
                }

                return _searchEducationalInsDG;
            }
        }
        public ICommand SearchSpecializationCommand
        {
            get
            {
                if (_searchSpecializationDG == null)
                {
                    _searchSpecializationDG = new RelayCommand(p => SearchSpecializationDG());
                }

                return _searchSpecializationDG;
            }
        }
        
        public ICommand SearchConcourseCommand
        {
            get
            {
                if (_searchConcourseDG == null)
                {
                    _searchConcourseDG = new RelayCommand(p => SearchConcourseDG());
                }

                return _searchConcourseDG;
            }
        }
       
        public ICommand SearchEntryCommand
        {
            get
            {
                if (_searchEntryDG == null)
                {
                    _searchEntryDG = new RelayCommand(p => SearchEntryDG());
                }

                return _searchEntryDG;
            }
        }


        #endregion

        #region Commands For Pagination
        // BACK
        public ICommand BackPagEntrantCommand
        {
            get
            {
                if (_backPagEntrantDG == null)
                {
                    _backPagEntrantDG = new RelayCommand(p => BackPagEntrantDG());
                }

                return _backPagEntrantDG;
            }
        }
        public ICommand BackPagEduInsCommand
        {
            get
            {
                if (_backPagEducationalInsDG == null)
                {
                    _backPagEducationalInsDG = new RelayCommand(p => BackPagEduInsDG());
                }

                return _backPagEducationalInsDG;
            }
        }
        public ICommand BackPagSpecializationCommand
        {
            get
            {
                if (_backPagSpecializationDG == null)
                {
                    _backPagSpecializationDG = new RelayCommand(p => BackPagSpecializationDG());
                }

                return _backPagSpecializationDG;
            }
        }
        public ICommand BackPagConcourseCommand
        {
            get
            {
                if (_backPagConcourseDG == null)
                {
                    _backPagConcourseDG = new RelayCommand(p => BackPagConcourseDG());
                }

                return _backPagConcourseDG;
            }
        }
        public ICommand BackPagEntryCommand
        {
            get
            {
                if (_backPagEntryDG == null)
                {
                    _backPagEntryDG = new RelayCommand(p => BackPagEntryDG());
                }

                return _backPagEntryDG;
            }
        }
       
        // FORWARD
        public ICommand ForwardEntrantCommand
        {
            get
            {
                if (_forwardPagEntrantDG == null)
                {
                    _forwardPagEntrantDG = new RelayCommand(p => ForwardPagEntrantDG());
                }

                return _forwardPagEntrantDG;
            }
        }

        public ICommand ForwardPagEduInsCommand
        {
            get
            {
                if (_forwardPagEducationalInsDG == null)
                {
                    _forwardPagEducationalInsDG = new RelayCommand(p => ForwardPagEduInsDG());
                }

                return _forwardPagEducationalInsDG;
            }
        }
        public ICommand ForwardPagSpecializationCommand
        {
            get
            {
                if (_forwardPagSpecializationDG == null)
                {
                    _forwardPagSpecializationDG = new RelayCommand(p => ForwardPagSpecializationDG());
                }

                return _forwardPagSpecializationDG;
            }
        }

        public ICommand ForwardPagConcourseCommand
        {
            get
            {
                if (_forwardPagConcourseDG == null)
                {
                    _forwardPagConcourseDG = new RelayCommand(p => ForwardPagConcourseDG());
                }

                return _forwardPagConcourseDG;
            }
        }

        public ICommand ForwardPagEntryCommand
        {
            get
            {
                if (_forwardPagEntryDG == null)
                {
                    _forwardPagEntryDG = new RelayCommand(p => ForwardPagEntryDG());
                }

                return _forwardPagEntryDG;
            }
        }

        #endregion

        #endregion

        #endregion

        #region Methods

        #region Methods For Search

        #region search for adding or deleting
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


        #region search for updating

        private void SearchDGSpecializationEduCombobox()
        {
            if (SearchTxtDGSpecializationEduCombobox != "")
            {
                SpecializationsEduDGcombobox = dbla.GetAllSpecializationEducationByInfo(SearchTxtDGSpecializationEduCombobox);
                EnabledEduSpecConcourseDG = true;
                EnabledDropDownEduSpecConcourseDG = true;
            }
            else {
                EnabledEduSpecConcourseDG = false;
                EnabledDropDownEduSpecConcourseDG = false;
            }

        }
      
        private void SearchDGEntrantCombobox()
        {
            if (SearchTxtDGEntrantCombobox != "")
            {
                // MessageBox.Show(SearchTxtDGEntrantCombobox);
                EntrantsDGCombobox = dbla.GetAllEntrantsByName(SearchTxtDGEntrantCombobox);
                EnabledEntryEntrantDGCombobox = true;
            }
            else
            {
                EnabledEntryEntrantDGCombobox = false;
            }

        }
      
        private void SearchDGConcourseCombobox()
        {
            if (SearchTxtDGConcourseCombobox != "")
            {
                ConcoursesDGCombobox = dbl.GetAllConcourseWithInfo(SearchTxtDGConcourseCombobox);
                EnabledEntryConcourseDGCombobox = true;
            }
            else
            {
                EnabledEntryConcourseDGCombobox= false;
            }

        }
    

        #endregion
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

        #region Methods For Update

        private void UpdateEntrant() {
            dbla.UpdateEntrant(SelectedEntrantDG.Id, SelectedEntrantDG.FullName,
                SelectedEntrantDG.Passport, SelectedEntrantDG.MaxBall, SelectedEntrantDG.DateYear);
        }
        private void UpdateCategory()
        {
            dbla.UpdateCategory(SelectedCategoryDG.Id, SelectedCategoryDG.CategoryName);
        }
        private void UpdateEduIns()
        {
            dbla.UpdateEduIns(SelectedEducationInsDG.Id, SelectedEducationInsDG.InsName, SelectedEducationInsDG.InsAddress);
        }
        private void UpdateSpecialization()
        {
            dbla.UpdateSpecializationName(SelectedSpecializationDG.Id, SelectedSpecializationDG.SpecName);
        }
        private void UpdateSpecializationCategory()
        {
            if (SelectedSpecializationDG != null & SelectedCategoryDGCombobox != null)
            {
               
               bool updatted = dbla.UpdateSpecializationCategory(SelectedSpecializationDG.Id, SelectedCategoryDGCombobox.Id);
                SpecializationsDG = dbl.GetAllSpecializationPagination((limit * NumPageSpecializationDG) - limit,limit);
            }
            else MessageBox.Show("Выберите изменяемый элемент из таблицы и элемент из списка");
        }
        private void UpdateConcourse()
        {
            //  MessageBox.Show(SelectedSpecializationEduDGCombobox.ToString() + " " + SelectedConcourseDG.ToString());
            dbla.UpdateConcourse(SelectedConcourseDG.Id, SelectedConcourseDG.CountSeats,
                Convert.ToInt32(SelectedConcourseDG.IsFree),
                Convert.ToInt32(SelectedConcourseDG.IsIntramural), SelectedConcourseDG.DateYear);
            ConcoursesDG = dbl.GetAllConcourseWithInfoPagination((limit * NumPageConcourseDG) - limit, limit);
        }
        private void UpdateConcourseEduSpec()
        {
            //MessageBox.Show(SelectedSpecializationEduDGCombobox.ToString() + " " + SelectedConcourseDG.ToString());
            dbla.UpdateConcourse(SelectedConcourseDG.Id, SelectedSpecializationEduDGCombobox.Id);
            ConcoursesDG = dbl.GetAllConcourseWithInfoPagination((limit * NumPageConcourseDG) - limit, limit);

        }
        private void UpdateEntryEntrant()
        {
            if(SelectedEntrantDGCombobox!=null & SelectedEntryDG !=null )
            {
                dbla.UpdateEntryEntrant(SelectedEntryDG.Id, SelectedEntrantDGCombobox.Id);
                EntriesDG = dbla.GetAllEntriesDGPagination((limit * NumPageEntryDG) - limit, limit);
               // MessageBox.Show(SelectedEntrantDGCombobox.ToString() + " " + SelectedEntryDG.ToString());
            }
        }
        private void UpdateEntryConcourse()
        {
            if (SelectedConcourseDGCombobox != null & SelectedEntryDG!=null)
            {
                dbla.UpdateEntryConcourse(SelectedEntryDG.Id, SelectedConcourseDGCombobox.Id);
                EntriesDG = dbla.GetAllEntriesDGPagination((limit * NumPageEntryDG) - limit, limit);


                // MessageBox.Show(SelectedConcourseDGCombobox.ToString() + " " + SelectedEntryDG.ToString());
            }
        }
        #endregion

        #region Methods For Searches
        private void SearchEntrantDG()
        {
           if(!String.IsNullOrWhiteSpace(SearchTxtDGEntrant))
           {
                EntrantsDG = dbla.GetAllEntrantsByName(SearchTxtDGEntrant);
                NumPageEntrantDG = 1;
           }
        }
        private void SearchEduInsDG()
        {
            if(!String.IsNullOrWhiteSpace(SearchTxtDGEducation))
            {
                EducationalInsDG = dbl.GetAllEducationalInsByName(SearchTxtDGEducation);
                NumPageEduInsDG = 1;
            }
        }
        private void SearchSpecializationDG()
        {
            if (!String.IsNullOrWhiteSpace(SearchTxtDGSpecialization))
            {
                SpecializationsDG = dbl.GetAllSpecializationsWithCategory(SearchTxtDGSpecialization);
                NumPageSpecializationDG = 1;
            }
        }
        private void SearchConcourseDG()
        {
            if (!String.IsNullOrWhiteSpace(SearchTxtDGConcourse))
            {
                ConcoursesDG = dbl.GetAllConcourseWithInfoDG(SearchTxtDGConcourse);
                NumPageConcourseDG = 1;
            }
        }
        private void SearchEntryDG()
        {
            if (!String.IsNullOrWhiteSpace(SearchTxtDGEntry))
            {
                EntriesDG = dbla.GetAllEntriesDG(SearchTxtDGEntry);
                NumPageEntryDG = 1;
            }
        }

        #endregion

        #region Methods For Back and Forward
        private void BackPagEntrantDG()
        {
            if (NumPageEntrantDG-1 > 0)
            {
                NumPageEntrantDG--;
                int num = (limit*numPageEntrantDG)-limit;
               
                EntrantsDG = dbla.GetAllEntrantsPagination(num, limit);
                
            }
        }
        private void BackPagEduInsDG()
        {
            if (NumPageEduInsDG-1>0)
            {
                NumPageEduInsDG--;
                int num = (limit * NumPageEduInsDG) - limit;

                EducationalInsDG = dbl.GetAllEduInsPagination(num, limit);
            }
        }
        private void BackPagSpecializationDG()
        {
            if (NumPageSpecializationDG - 1>0)
            {
                NumPageSpecializationDG--;
                int num = (limit * NumPageSpecializationDG) - limit;
                SpecializationsDG = dbl.GetAllSpecializationPagination(num, limit);
            }
        }
        private void BackPagConcourseDG()
        {
            if (NumPageConcourseDG - 1>0)
            {
                NumPageConcourseDG--;
                int num = (limit * NumPageConcourseDG) - limit;
                ConcoursesDG = dbl.GetAllConcourseWithInfoPagination(num, limit);
            }
        }
        private void BackPagEntryDG()
        {
            if (NumPageEntryDG - 1>0)
            {
                NumPageEntryDG--;
                int num = (limit * NumPageEntryDG) - limit;
                EntriesDG = dbla.GetAllEntriesDGPagination(num, limit);
            }
        }
        // FORWARD 

        private void ForwardPagEntrantDG()
        {
            NumPageEntrantDG++;
            int num = (limit * NumPageEntrantDG) - limit;
            EntrantsDG = dbla.GetAllEntrantsPagination(num, limit);
            

        }
        private void ForwardPagEduInsDG()
        {
            NumPageEduInsDG++;
            int num = (limit * NumPageEduInsDG) - limit;

            EducationalInsDG = dbl.GetAllEduInsPagination(num, limit);


        }
        private void ForwardPagSpecializationDG()
        {
            NumPageSpecializationDG++;
            int num = (limit * NumPageSpecializationDG) - limit;
            SpecializationsDG = dbl.GetAllSpecializationPagination(num, limit);

        }
        private void ForwardPagConcourseDG()
        {
            NumPageConcourseDG++;
            int num = (limit * NumPageConcourseDG) - limit;
            ConcoursesDG = dbl.GetAllConcourseWithInfoPagination(num, limit);


        }
        private void ForwardPagEntryDG()
        {
            NumPageEntryDG++;
            int num = (limit * NumPageEntryDG) - limit;
            EntriesDG = dbla.GetAllEntriesDGPagination(num, limit);


        }
        #endregion


        #endregion

        public AdminPageViewModel()
        {
            categories = dbl.GetAllCategory();
            entrantsDG = dbla.GetAllEntrantsPagination(offset, limit);
            educationInsDG = dbl.GetAllEduInsPagination(offset, limit);
            specializationsDG = dbl.GetAllSpecializationPagination(offset, limit);

            concoursesDG = dbl.GetAllConcourseWithInfoPagination(offset, limit);
            entriesDG = dbla.GetAllEntriesDGPagination(offset, limit);
            
           // entrantsList = dbla.GetAllEntrants();
        }
    }
}
