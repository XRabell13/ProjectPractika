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
using Excel = Microsoft.Office.Interop.Excel;

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

        ICommand _searchBySpecName, _searchByLetter, _searchByConcourse, _sortByAverageBall, _sortByCountSeats, _loadExcelInfo;

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
            set { selectedYear = value; OnPropertyChanged(); SearchByLetterAndYear(); }
        }
        public char SelectedLetter
        {
            get { return selectedLetter; }
            set { selectedLetter = value; OnPropertyChanged(); SearchByLetterAndYear(); }
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
        public ICommand SortByAverageBallCommand
        {
            get
            {
                if (_sortByAverageBall == null)
                {
                    _sortByAverageBall = new RelayCommand(p => SortByAverageBall());
                }

                return _sortByAverageBall;
            }
        }
        public ICommand SortByCountSeatsCommand
        {
            get
            {
                if (_sortByCountSeats == null)
                {
                    _sortByCountSeats = new RelayCommand(p => SortByCountSeats());
                }

                return _sortByCountSeats;
            }
        }
        public ICommand LoadExcelInfoCommand
        {
            get
            {
                if (_loadExcelInfo == null)
                {
                    _loadExcelInfo = new RelayCommand(p => LoadExcelInfo());
                }

                return _loadExcelInfo;
            }
        }
        #endregion

        #region Methods 

        #region methods Search
        public void SearchBySpecName()
        {
            if (!String.IsNullOrWhiteSpace(SearchTxtSpecialization))
            {
                Concourses = dbl.GetAllConcoursesForViewBySpecName(SearchTxtSpecialization);
            }
            else MessageBox.Show("Введите в поиск название специальности");
        }
        public void SearchByLetterAndYear()
        {
            Concourses = dbl.GetAllConcoursesForViewByLetterAndYear(SelectedLetter, SelectedYear);
        }
        public void SearchByConcourse()
        {
            if (SelectedCategory != null)
            {
                if (CheckRBtnIsFreeNo == false & CheckRBtnIsFreeYes == false)
                {
                    MessageBox.Show("Выберите платное или бесплатное обучение.");
                    return;
                }
                if (CheckRBtnIsIntramuralNo == false & CheckRBtnIsIntramuralYes == false)
                {
                    MessageBox.Show("Выберите очное или заочное обучение.");
                    return;
                }

                Concourses = dbl.GetAllConcoursesForViewByInfo(SelectedCategory.Id,  Convert.ToInt32(CheckRBtnIsFreeYes),
                     Convert.ToInt32(CheckRBtnIsIntramuralYes));

            }
            else MessageBox.Show("Выберите категорию");
        }
        #endregion

        #region methods Sorts

        public void SortByAverageBall()
        {
            try
            {
             var sortList = Concourses.OrderByDescending(x => x.AverageBall);
                ObservableCollection<ConcoursesForView> list = new ObservableCollection<ConcoursesForView>();
            foreach(var item in sortList)
            {
                    list.Add(new ConcoursesForView(item.Id, item.SpecName, item.InsName,
                        item.CategoryName, item.IsFree, item.IsIntramural, item.DateYear, item.CountSeats, item.CountEntrants,
                        item.AverageBall));
            }
          
            Concourses = list;
            }
            catch(Exception e){ MessageBox.Show(e.Message); }
          
        }
        public void SortByCountSeats()
        {
            try
            {
                var sortList = Concourses.OrderByDescending(x => x.CountSeats);
                ObservableCollection<ConcoursesForView> list = new ObservableCollection<ConcoursesForView>();
                foreach (var item in sortList)
                {
                    list.Add(new ConcoursesForView(item.Id, item.SpecName, item.InsName,
                        item.CategoryName, item.IsFree, item.IsIntramural, item.DateYear, item.CountSeats, item.CountEntrants,
                        item.AverageBall));
                }

                Concourses = list;
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        #endregion


        public void LoadExcelInfo()
        {
            if (SelectedConcourse != null)
            {
                MessageBox.Show(SelectedConcourse.ToString());

                List<int> balls = dbl.GetBallsByConcourse(SelectedConcourse.Id);
                //Объявляем приложение
                Excel.Application app = new Excel.Application
                {
                    //Отобразить Excel
                    Visible = true,
                    //Количество листов в рабочей книге
                    SheetsInNewWorkbook = 1
                };
                //Добавить рабочую книгу
                Excel.Workbook workBook = app.Workbooks.Add(Type.Missing);
                //Отключить отображение окон с сообщениями
                app.DisplayAlerts = false;
                //Получаем первый лист документа (счет начинается с 1)
                Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
                //Название листа (вкладки снизу)
                sheet.Name = "Monitoring";

                //Объединение ячеек 
                Excel.Range range1 = sheet.get_Range("B2", "K5");
                range1.Merge(Type.Missing);
                range1.Value = SelectedConcourse.ToString();

                Excel.Range range2 = sheet.get_Range("B7", "D7");
                range2.Merge(Type.Missing);
                range2.Value = "План приема: " + SelectedConcourse.CountSeats;

                range2 = sheet.get_Range("B8", "D8");
                range2.Merge(Type.Missing);
                range2.Value = "Подано заявлений: " + SelectedConcourse.CountEntrants;

                int bal1=400, bal2=396;
               
                   for (int i = 5; i <= 78; i++)
                   {
                    int countEntrant = 0;
                    sheet.Cells[7, i] = "" + bal1 + "-" + bal2;

                    for(int j = 0; j < balls.Count; j++)
                        if (balls[j] >= bal2 & balls[j] <= bal1)
                            countEntrant++;

                     sheet.Cells[8, i] = countEntrant;
                        
                   
                        bal1 = bal1 - 5;
                        bal2 = bal2 - 5;
                    
                   }
                
                app.Application.ActiveWorkbook.SaveAs("Monitoring.xlsx", Type.Missing,
  Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
  Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
           
        }
        #endregion
       
        
        public CatalogSpecializationsViewModel()
        {
            Categories = dbl.GetAllCategory();
            SelectedLetter = alphabet[0];
            SelectedYear = years[0];
            Concourses = dbl.GetAllConcoursesForViewByLetterAndYear(SelectedLetter, SelectedYear);
           // Concourses.Add(new ConcoursesForView(1,"specname","as","cat",true,true,2019,123,34,250));
           // Concourses.Add(new ConcoursesForView(1,"specname","as","cat",true,true,2019,123,34,250));
        }
   }
}
