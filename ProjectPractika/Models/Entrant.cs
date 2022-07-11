using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika.Models
{
    public class Entrant : INotifyPropertyChanged
    {
        int id;
        string fullName;
        string passport;
        int maxBall;
        int dataYear;

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
        public int DataYear
        {
            get
            {
                return dataYear;
            }
            set
            {
                dataYear = value;
                OnPropertyChanged();
            }
        }

        public Entrant(int id, string fullName, string passport, int maxBall, int dataYear)
        {
            this.Id = id;
            this.FullName = fullName;
            this.Passport = passport;
            this.MaxBall = maxBall; 
            this.DataYear = dataYear;
          
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(name));
        }


        public override string ToString()
        {
            return Id.ToString() + " " + FullName + " " + Passport + " " + MaxBall + " " + DataYear;
        }
    }
}
