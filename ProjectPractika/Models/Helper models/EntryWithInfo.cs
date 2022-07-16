using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class EntryWithInfo:ObservableObject
    {
        int id;
        string fullName;
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
        public EntryWithInfo(int id, string fullName,  string specName, string insName)
        {
            Id = id;
            FullName = fullName;
            SpecName = specName;
            InsName = insName;
        }

        public override string ToString()
        {
            return fullName + "\n" + specName + "\n" + insName;
        }
    }
}
