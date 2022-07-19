using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class EntryDG : ObservableObject
    {
        int id;
        string fullName;
        string specName;
        string insName;
        bool isFree, isIntramural;
        int dateYear;

       

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
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
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }
        public EntryDG(int id, string fullName, string specName, string insName, bool isFree, bool isIntramural, int dateYear)
        {
            this.Id = id;
            this.SpecName = specName;
            this.InsName = insName;
            this.FullName = fullName;
            DateYear = dateYear;
            IsFree = isFree;
            IsIntramural = isIntramural;
        }

        public override string ToString()
        {
            return Id.ToString() + " " + specName + " " + insName;
        }
    }
}

