using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika.Models
{
    public class Specialization : INotifyPropertyChanged
    {
        int id;
        string specName;
        int idCategory;

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

        public Specialization(int id, string specName, int idCategory)
        {
            this.Id = id;
            this.SpecName = specName;
            this.idCategory = idCategory;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(name));
        }


        public override string ToString()
        {
            return Id.ToString() + " " + specName + " " + idCategory;
        }
    }
}
