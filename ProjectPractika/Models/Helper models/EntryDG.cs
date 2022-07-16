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
        string specName;
        string insName;
        int countSeats;
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

        public EntryDG(int id, string specName, string insName)
        {
            this.Id = id;
            this.SpecName = specName;
            this.InsName = insName;
        }

        public override string ToString()
        {
            return Id.ToString() + " " + specName + " " + insName;
        }
    }
}

