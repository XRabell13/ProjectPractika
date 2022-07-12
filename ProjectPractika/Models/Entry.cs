using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika.Models
{
    public class Entry : ObservableObject
    {
        int id;
        int idEntrant;
        int idConcourse;

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
        public int IdEntrant
        {
            get
            {
                return idEntrant;
            }
            set
            {
                idEntrant = value;
                OnPropertyChanged();
            }
        }
        public int IdConcourse
        {
            get
            {
                return idConcourse;
            }
            set
            {
                idConcourse = value;
                OnPropertyChanged();
            }
        }

        public Entry(int id, int idEntrant, int idConcourse)
        {
            this.Id = id;
            this.IdEntrant = idEntrant;
            this.idConcourse = idConcourse;
        }

        public override string ToString()
        {
            return Id.ToString() + " " + idConcourse + " " + idEntrant;
        }
    }
}
