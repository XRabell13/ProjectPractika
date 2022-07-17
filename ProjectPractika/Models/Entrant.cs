using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika.Models
{
    public class Entrant : ObservableObject
    {
        int id;
        string fullName;
        string passport;
        int maxBall;
        int dateYear;

        public int Id { 
            get {
                return id; 
            } 
            set { 
                id = value;
              //  OnPropertyChanged();
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
        public string Passport
        {
            get
            {
                return passport;
            }
            set
            {
                passport = value;
                OnPropertyChanged();
            }
        }
        public int MaxBall
        {
            get
            {
                return maxBall;
            }
            set
            {
                maxBall = value;
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

        public Entrant(int id, string fullName, string passport, int maxBall, int dateYear)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Passport = passport;
            this.MaxBall = maxBall; 
            this.DateYear = dateYear;
          
        }

        public override string ToString()
        {
            return FullName + " " + DateYear;
        }
    }
}
