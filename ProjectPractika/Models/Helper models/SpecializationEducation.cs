using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class SpecializationEducation : ObservableObject
    {
        int id;
        string specName;
        string insName;

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

        public SpecializationEducation(int id, string specName, string insName)
        {
            this.Id = id;
            this.SpecName = specName;
            this.InsName = insName;
        }

        public override string ToString()
        {
            return specName + "\n" + insName;
        }
    }
}
