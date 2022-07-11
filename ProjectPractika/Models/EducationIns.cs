using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika.Models
{
    public class EducationIns : INotifyPropertyChanged
    {
        int id;
        string insName;
        string insAddress;

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
        public string InsAddress
        {
            get
            {
                return insAddress;
            }
            set
            {
                insAddress = value;
                OnPropertyChanged();
            }
        }
        

        public EducationIns(int id, string insName, string insAddress)
        {
            this.Id = id;
            this.InsName = insName;
            this.InsAddress = insAddress;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(name));
        }


        public override string ToString()
        {
            return Id.ToString() + " " + InsName + " " + InsAddress; 
        }
    }
}
