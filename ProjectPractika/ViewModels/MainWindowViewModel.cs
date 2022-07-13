using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectPractika.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        private ICommand _changePageCommand;
        private ICommand _addKey;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

      /*  public static RoutedCommand MyCommand = new RoutedCommand();

        public RoutedCommand KeyCommand
        {
            get { return MyCommand; }
            set { MyCommand = value; }
        }*/
        #endregion

        public MainWindowViewModel()
        {
            //MyCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));

            // Add available pages
            PageViewModels.Add(new AdminPageViewModel());
            PageViewModels.Add(new AuthorizationViewModel(this));
            PageViewModels.Add(new HomePageViewModel());
           // PageViewModels.Add(new Test2ViewModel());


            // Set starting page
            CurrentPageViewModel = PageViewModels[2];
        }

        #region Properties / Commands

      


        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public ICommand ShowAuthorizCommand
        {
            get
            {
                if (_addKey == null)
                {
                    _addKey = new RelayCommand(p => ShowAdminAuthoriz());
                }

                return _addKey;
            }

        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

       public  void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void ShowAdminAuthoriz()
        {
            ChangeViewModel(PageViewModels[1]);
        }


        #endregion
    }
}
