using MobileProject.Services;
using MobileProject.Models;
using System.Collections.ObjectModel;


namespace MobileProject;


public partial class InfoCentrePage : ContentPage
{
    ServiceController dataController = App.DataController;
	public ObservableCollection<Currency> SelectedCurrencies { get; set; }


    public InfoCentrePage()
	{
		InitializeComponent();

		SelectedCurrencies = new ObservableCollection<Currency>();

        
        BindingContext = this;
	}

    private async void onUSDClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyUSD))
        {
            btnUSD.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyUSD);
        }
        else
        {
            btnUSD.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyUSD);
        }
    }

    private async void onAUDClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyAUD))
        {
            btnAUD.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyAUD);
        }
        else
        {
            btnAUD.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyAUD);
        }
    }
    private async void onEURClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyEUR))
        {
            btnEUR.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyEUR);
        }
        else
        {
            btnEUR.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyEUR);
        }
    }

    private async void onJPYClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyJPY))
        {
            btnJPY.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyJPY);
        }
        else
        {
            btnJPY.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyJPY);
        }
    }

    private async void onGBPClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyGBP))
        {
            btnGBP.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyGBP);
        }
        else
        {
            btnGBP.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyGBP);
        }
    }

    private async void onCADClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyCAD))
        {
            btnCAD.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyCAD);
        }
        else
        {
            btnCAD.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyCAD);
        }
    }

    private async void onCNYClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyCNY))
        {
            btnCNY.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyCNY);
        }
        else
        {
            btnCNY.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyCNY);
        }
    }

    private async void onCHFClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyCHF))
        {
            btnCHF.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyCHF);
        }
        else
        {
            btnCHF.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyCHF);
        }
    }

    private async void onHKDClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyHKD))
        {
            btnHKD.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyHKD);
        }
        else
        {
            btnHKD.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyHKD);
        }
    }

    private async void onSGDClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencySGD))
        {
            btnSGD.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencySGD);
        }
        else
        {
            btnSGD.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencySGD);
        }
    }

    private async void onSEKClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencySEK))
        {
            btnSEK.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencySEK);
        }
        else
        {
            btnSEK.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencySEK);
        }
    }

    private async void onKRWClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyKRW))
        {
            btnKRW.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyKRW);
        }
        else
        {
            btnKRW.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyKRW);
        }
    }

    private async void onNOKClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyNOK))
        {
            btnNOK.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyNOK);
        }
        else
        {
            btnNOK.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyNOK);
        }
    }

    private async void onNZDClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyNZD))
        {
            btnNZD.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyNZD);
        }
        else
        {
            btnNZD.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyNZD);
        }
    }

    private async void onINRClicked(object sender, EventArgs e)
    {
        if (SelectedCurrencies == null)
        {
            return;
        }

        if (SelectedCurrencies.Contains(dataController.CurrencyINR))
        {
            btnINR.BackgroundColor = Colors.Black;
            SelectedCurrencies.Remove(dataController.CurrencyINR);
        }
        else
        {
            btnINR.BackgroundColor = Colors.Blue;
            SelectedCurrencies.Add(dataController.CurrencyINR);
        }

        
    }

    private async Task TabOpen()
    {


        if (dataController.CheckLatestUpdate())
        {
            await dataController.CurrentRateUpdate();
        }


        int apiNumber = dataController.GetApiRequestNumber();

        if (apiNumber != 0)
        {
            DisplayAlert($"{apiNumber} API Request(s) Used", "Always try the limited use of API request", "OK");
        }

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await TabOpen();
    }

}