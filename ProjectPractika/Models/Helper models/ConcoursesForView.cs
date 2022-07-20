using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class ConcoursesForView : ObservableObject
    {
        int idCouncourse;
        string specName;
        string insName;
        string categoryName;
        bool isFree, isIntramural;
        int dateYear;
        int countSeats;
        

        public int Id
        {
            get
            {
                return idCouncourse;
            }
            set
            {
                idCouncourse = value;
                //  OnPropertyChanged();
            }
        }
        public string SpecName
        {
            get
            {
                return specName;
            }
            set
            {
                specName = value;
                OnPropertyChanged();
            }
        }
        public string InsName
        {
            get
            {
                return insName;
            }
            set
            {
                insName = value;
                OnPropertyChanged();
            }
        }
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                OnPropertyChanged();
            }
        }
        public int DateYear
        {
            get
            {
                return dateYear;
            }
            set
            {
                dateYear = value;
                OnPropertyChanged();
            }
        }
        public bool IsFree
        {
            get
            {
                return isFree;
            }
            set
            {
                isFree = value;
                OnPropertyChanged();
            }
        }
        public bool IsIntramural
        {
            get
            {
                return isIntramural;
            }
            set
            {
                isIntramural = value;
                OnPropertyChanged();
            }
        }
        public int CountSeats
        {
            get
            {
                return countSeats;
            }
            set
            {
                countSeats = value;
                OnPropertyChanged();
            }
        }

        public ConcoursesForView(int id, string specName, string insName, string categoryName, bool isFree, bool isIntramural, int dateYear, int countSeats)
        {
            Id = id;
            SpecName = specName;
            InsName = insName;
            DateYear = dateYear;
            IsFree = isFree;
            IsIntramural = isIntramural;
            CountSeats = countSeats;
            CategoryName = categoryName;
          
        }

        public override string ToString()
        {
           
                string isFree = "Платно";
                string isIntramural = "Очно";
                if (IsFree) isFree = "Бюджет";
                if (IsIntramural) isIntramural = "Заочно";
                return specName + " " + categoryName +" " + insName + "\n" + isFree + ", " + isIntramural + " " + dateYear;
            
           // return specName + "\n" + insName;
        }
    }
}
