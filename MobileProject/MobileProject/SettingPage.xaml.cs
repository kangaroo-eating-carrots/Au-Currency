using MobileProject.Models;
using MobileProject.Services;


namespace MobileProject
{
    public partial class SettingPage : ContentPage
    {
        ServiceController dataController = App.DataController;
        public SettingPage()
        {
            InitializeComponent();

            this.IsEnabled = false;


            MyCurrencyPicker.ItemsSource = null;
            MyCurrencyPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));

            if (Preferences.Get(PreferenceKeys.MyCurrencyPreference.ToString(), null) is string myCurrencyPreference)
            {
                if (Enum.TryParse(myCurrencyPreference, out CurrencySymbol userMyCurrencyPreference)){
                    dataController.MyCurrencySymbol = userMyCurrencyPreference;
                    dataController.UpdateTargetCurrencies();
                }
            }

            MyCurrencyPicker.SelectedItem = dataController.MyCurrencySymbol;

            InterestOnePicker.ItemsSource = null;

            if (Preferences.Get(PreferenceKeys.InterestOnePreference.ToString(), null) is string interestOnePreference)
            {
                if (Enum.TryParse(interestOnePreference, out CurrencySymbol userInteretOnePreference))
                {
                    dataController.InterestingOneSymbol = userInteretOnePreference;
                    dataController.UpdateTargetCurrencies();
                }
            }

            InterestOnePicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));
            InterestOnePicker.SelectedItem = dataController.InterestingOneSymbol;

            InterestTwoPicker.ItemsSource = null;

            if (Preferences.Get(PreferenceKeys.InterestTwoPreference.ToString(), null) is string interestTwoPreference)
            {
                if (Enum.TryParse(interestTwoPreference, out CurrencySymbol userInteretTwoPreference))
                {
                    dataController.InterestingTwoSymbol = userInteretTwoPreference;
                    dataController.UpdateTargetCurrencies();
                }
            }

            InterestTwoPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));
            InterestTwoPicker.SelectedItem = dataController.InterestingTwoSymbol;

            if (dataController.DatabaseExists())
            {
                DatabasePath.Text = "Database can be found with the path: " + Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "currencies.db");
            }
            else
            {
                DatabasePath.Text = "Database can not be found with the path: " + Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "currencies.db"); ;
            }

            dataController.DatabaseService.DisplayPrimiaryKeys();


            // For this part, I referred to contents from Chat GPT
            Task.Run(async () =>
            {
                if (Shell.Current is AppShell appShell)
                {
                    appShell.DisableAllTabs();

                    if (dataController.CheckLatestUpdate())
                    {
                        await dataController.CurrentRateUpdate();
                    }

                    if (dataController.CheckHistoricalUpdate())
                    {
                        await dataController.UpdateHistoricalData();
                    }

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        appShell.EnableAllTabs();
                        this.IsEnabled = true;
                    });

                    int apiNumber = dataController.GetApiRequestNumber();

                    if (apiNumber != 0)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            DisplayAlert($"{apiNumber} API Request(s) Used", "Always try the limited use of API request", "OK");
                        });
                    }
                }

                if (this.IsEnabled == false)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        this.IsEnabled = true;
                    });
                }
            });
        }



        private async void MyCurrencyChanged(object sender, EventArgs e)
        {
            dataController.MyCurrencySymbol=(CurrencySymbol)MyCurrencyPicker.SelectedItem;
            Preferences.Set(PreferenceKeys.MyCurrencyPreference.ToString(), dataController.MyCurrencySymbol.ToString());
            dataController.UpdateTargetCurrencies();
        }

        private async void InterestOneChanged(object sender, EventArgs e)
        {
            dataController.InterestingOneSymbol = (CurrencySymbol)InterestOnePicker.SelectedItem;
            Preferences.Set(PreferenceKeys.InterestOnePreference.ToString(), dataController.InterestingOneSymbol.ToString());
            dataController.UpdateTargetCurrencies();
        }

        private async void InterestTwoChanged(object sender, EventArgs e)
        {
            dataController.InterestingTwoSymbol = (CurrencySymbol)InterestTwoPicker.SelectedItem;
            Preferences.Set(PreferenceKeys.InterestTwoPreference.ToString(), dataController.InterestingTwoSymbol.ToString());
            dataController.UpdateTargetCurrencies();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await dataController.DatabaseService.DisplayPrimiaryKeys();
        }


    }
}

enum PreferenceKeys
{
    MyCurrencyPreference,
    InterestOnePreference,
    InterestTwoPreference
}


