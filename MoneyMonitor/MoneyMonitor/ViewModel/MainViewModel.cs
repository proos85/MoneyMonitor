using MoneyMonitor.ViewModel.Base;

namespace MoneyMonitor.ViewModel
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainViewModel: BaseViewModel
    {
        private OverviewViewModel _overviewViewModel;
        private SplashViewModel _splashViewModel;

        public OverviewViewModel OverviewViewModel
        {
            get => _overviewViewModel;
            set
            {
                if (_overviewViewModel != value)
                {
                    _overviewViewModel = value;
                    OnPropertyChanged(nameof(OverviewViewModel));
                }
            }
        }

        public SplashViewModel SplashViewModel
        {
            get => _splashViewModel;
            set
            {
                if (_splashViewModel != value)
                {
                    _splashViewModel = value;
                    OnPropertyChanged(nameof(SplashViewModel));
                }
            }
        }


        public MainViewModel(
            OverviewViewModel overviewViewModel,
            SplashViewModel splashViewModel)
        {
            _overviewViewModel = overviewViewModel;
            _splashViewModel = splashViewModel;
        }
    }
}