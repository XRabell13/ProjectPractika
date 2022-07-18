using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class SpecializationDG : ObservableObject
    {
        int id;
        string specName;
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

        public SpecializationDG(int id, string specName, string category)
        {
            this.Id = id;
            this.SpecName = specName;
            this.CategoryName = category;
        }

        public override string ToString()
        {
            return Id.ToString() + " " + specName + " " + categoryName;
        }
    }
}
