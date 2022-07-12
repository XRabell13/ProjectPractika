using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.ViewModels
{
    public class AdminPageViewModel : ObservableObject, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "АдминПанель";
            }
        }
    }
}
