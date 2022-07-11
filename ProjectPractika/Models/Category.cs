using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika.Models
{
    public class Category : INotifyPropertyChanged
    {
        int id;
        string categoryName;

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
  
        public Category(int id, string categoryName)
        {
            this.Id = id;
            this.CategoryName = categoryName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(name));
        }


        public override string ToString()
        {
            return Id.ToString() + " " + categoryName;
        }
    }
}
