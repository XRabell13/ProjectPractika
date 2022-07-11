using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika.Models
{
    public class Concourse : INotifyPropertyChanged
    {
        int id;
        int countSeats;
        bool isFree, isIntramural;
        int dateYear;
        int idSpecializationEducational;

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
        public int IdSpecializationEducational
        {
            get
            {
                return idSpecializationEducational;
            }
            set
            {
                idSpecializationEducational = value;
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

        public Concourse(int id, int countSeats, bool isFree, bool isIntramural, int dateYear, int idSpecializationEducational)
        {
            this.Id = id;
            this.CountSeats = countSeats;
            this.IsFree = isFree;
            this.IsIntramural = isIntramural;
            this.DateYear = dateYear;
            this.IdSpecializationEducational = idSpecializationEducational; 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(name));
        }


        public override string ToString()
        {
            return Id.ToString() + " " +  countSeats + " " + isFree + " " + isIntramural;
        }
    }
}
