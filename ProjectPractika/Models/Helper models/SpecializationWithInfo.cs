using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class SpecializationWithInfo : ObservableObject
    {
        int id;
        int specEduId;
        string insName;
        string specName;

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
        public int SpecEduId
        {
            get
            {
                return specEduId;
            }
            set
            {
                specEduId = value;
                OnPropertyChanged();
            }
        }

        public SpecializationWithInfo(int id, string specName, string insName, int specEduId)
        {
            this.Id = id;
            this.SpecName = specName;
            this.InsName = insName;
            this.SpecEduId = specEduId;
        }

        public override string ToString()
        {
            return specName + "\n" + insName;
        }
    }
}
