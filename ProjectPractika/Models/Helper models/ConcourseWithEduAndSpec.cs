using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.Models.Helper_models
{
    public class ConcourseWithEduAndSpec : ObservableObject
    {
        int idCouncourse;
        string specName;
        string insName;

        public int Id
        {
            get
            {
                return idCouncourse;
            }
            set
            {
                idCouncourse = value;
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

        public ConcourseWithEduAndSpec(int id, string specName, string insName)
        {
            Id = id;
            SpecName = specName;
            InsName = insName;
        }

        public override string ToString()
        {
            return specName + "\n" + insName;
        }

    }
}
