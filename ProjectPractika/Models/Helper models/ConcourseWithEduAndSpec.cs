using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class ConcourseWithEduAndSpec : ObservableObject
    {
        int idCouncourse;
        string specName;
        string insName;
        bool isFree, isIntramural;
        int dateYear;
        int constr;

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

        public ConcourseWithEduAndSpec(int id, string specName, string insName)
        {
            Id = id;
            SpecName = specName;
            InsName = insName;
            constr = 1;
        }
        public ConcourseWithEduAndSpec(int id, string specName, string insName, bool isFree, bool isIntramural, int dateYear)
        {
            Id = id;
            SpecName = specName;
            InsName = insName;
            DateYear = dateYear;
            IsFree = isFree;
            IsIntramural = isIntramural;
            constr = 2;
        }

        public override string ToString()
        {
            if (constr == 2)
            {
                string isFree = "Платно";
                string isIntramural = "Очно";
                if (IsFree) isFree = "Бюджет";
                if (IsIntramural) isIntramural = "Заочно";
                return specName + "\n" + insName + "\n" + dateYear + "\n" + isFree + ", " + isIntramural;
            }
            return specName + "\n" + insName;
        }

    }
}
