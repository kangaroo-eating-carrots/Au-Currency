using MobileProject.Services;

namespace MobileProject
{
    public partial class App : Application
    {

        private static ServiceController dataController = default!;

        public static ServiceController DataController 
        {
            get
            {
                if (dataController == null)
                {
                    dataController = new ServiceController(Models.CurrencySymbol.AUD, Models.CurrencySymbol.USD, Models.CurrencySymbol.KRW);
                }
                return dataController;
            }
            set { dataController = value; }
        }



        public App()
        {
            InitializeComponent();

            Application.Current.UserAppTheme = AppTheme.Dark;

            MainPage = new AppShell();

        }
    }
}
