namespace MobileProject;
using MobileProject.Models;
using MobileProject.Services;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

public partial class HistoricalTrendPage : ContentPage
{
    ServiceController dataController = App.DataController;

    Currency numCurrency;
	Currency denumCurrency;

	List<float> tenDayData;
	List<float> oneYearData;
	List<float> threeYearData;

    public HistoricalTrendPage()
	{
		InitializeComponent();

		numCurrencyPicker.ItemsSource = null;
		numCurrencyPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));
		numCurrencyPicker.SelectedItem = dataController.InterestingOne.Symbol;

        denumCurrencyPicker.ItemsSource = null;
		denumCurrencyPicker.ItemsSource = Enum.GetValues(typeof(CurrencySymbol));
        denumCurrencyPicker.SelectedItem = dataController.MyCurrency.Symbol;

		CurrencyChooser(dataController.InterestingOne.Symbol, dataController.MyCurrency.Symbol);
    }

	private async void NumCurrencyChanged(object sender, EventArgs e)
	{
        if (numCurrencyPicker.SelectedItem == null || denumCurrencyPicker.SelectedItem == null)
        {
            return;
        }

        CurrencyChooser((CurrencySymbol)numCurrencyPicker.SelectedItem, (CurrencySymbol)denumCurrencyPicker.SelectedItem);
        await UpdateData();
    }

    private async void DenumCurrencyChanged(object sender, EventArgs e)
    {
        if (numCurrencyPicker.SelectedItem == null || denumCurrencyPicker.SelectedItem == null)
        {
            return;
        }

        CurrencyChooser((CurrencySymbol)numCurrencyPicker.SelectedItem, (CurrencySymbol)denumCurrencyPicker.SelectedItem);
        await UpdateData();
    }

	private async void CurrencyChooser(CurrencySymbol numSymbol, CurrencySymbol denumSymbol)
	{
		foreach (Currency currency in dataController.AllCurrencies)
		{
			if (currency.Symbol == numSymbol)
			{
				numCurrency = currency;
			}

			if (currency.Symbol == denumSymbol)
			{
				denumCurrency = currency;
			}
		}

		tenDayData = await dataController.MakeTenDayGraphData(numCurrency, denumCurrency);
		oneYearData = await dataController.MakeOneYearGraphData(numCurrency, denumCurrency);
		threeYearData = await dataController.MakeThreeYearGraphData(numCurrency, denumCurrency);
	}

	private async Task<ChartEntry[]> GetChartEntries(List<float> graphData)
	{
		int entryLength = graphData.Count;
        ChartEntry[] graphEntries = new ChartEntry[entryLength];

        for (int i = 0; i < graphEntries.Length; i++)
        {
            graphEntries[i] = new ChartEntry(graphData[i])
            {
                Color = SKColor.Parse("#FFFFFF")
            };
        }

		return graphEntries;
    }

	private async Task UpdateGraph()
	{
		ChartEntry[] tenDayGraphEntries = await GetChartEntries(tenDayData);
		float minValueForTenDayGraph = tenDayData.Min() * 0.95f;
		float maxValueForTenDayGraph = tenDayData.Max() * 1.05f;

		graphTenView.Chart = new LineChart
		{
			Entries = tenDayGraphEntries,
			BackgroundColor = SKColor.Parse("#000000"),
			LineMode = LineMode.Straight,
			Margin = 20,
			MinValue = minValueForTenDayGraph,
			MaxValue = maxValueForTenDayGraph
		};


        ChartEntry[] OneYearGraphEntries = await GetChartEntries(oneYearData);
        float minValueForOneYearGraph = oneYearData.Min() * 0.95f;
        float maxValueForOneYearGraph = oneYearData.Max() * 1.05f;

        graphOneYearView.Chart = new LineChart
        {
            Entries = OneYearGraphEntries,
            BackgroundColor = SKColor.Parse("#000000"),
            LineMode = LineMode.Straight,
            Margin = 20,
            MinValue = minValueForOneYearGraph,
            MaxValue = maxValueForOneYearGraph
        };


        ChartEntry[] ThreeYearGraphEntries = await GetChartEntries(threeYearData);
        float minValueForThreeYearGraph = threeYearData.Min() * 0.95f;
        float maxValueForThreeYearGraph = threeYearData.Max() * 1.05f;

        graphThreeYearView.Chart = new LineChart
        {
            Entries = ThreeYearGraphEntries,
            BackgroundColor = SKColor.Parse("#000000"),
            LineMode = LineMode.Straight,
            Margin = 20,
            MinValue = minValueForThreeYearGraph,
            MaxValue = maxValueForThreeYearGraph
        };
    }

    private async Task UpdateData()
    {
        if (dataController.CheckLatestUpdate())
        {
            await dataController.CurrentRateUpdate();
        }

		float currentRate = numCurrency.LatestRatePerUSD / denumCurrency.LatestRatePerUSD;
		string currentRateStr;
        string roundStr;

        if (currentRate >= 9999.5)
        {
            roundStr = "F0";
            currentRateStr = currentRate.ToString(roundStr);
            
        }
        else if (currentRate >= 999.95)
        {
            roundStr = "F1";
            currentRateStr = currentRate.ToString(roundStr);
        }
        else if (currentRate >= 99.995)
        {
            roundStr = "F2";
            currentRateStr = currentRate.ToString(roundStr);
        }
        else if (currentRate >= 9.9995)
        {
            roundStr = "F3";
            currentRateStr = currentRate.ToString(roundStr);
        }
        else
        {
            roundStr = "F4";
            currentRateStr = currentRate.ToString(roundStr);
        }

        currentRateLabel.Text = currentRateStr;

        string rateFactorDescription = $"{numCurrency.Symbol} = 1 {denumCurrency.Symbol}";
        rateDescription.Text = rateFactorDescription;


        if (dataController.CheckHistoricalUpdate())
        {
            await dataController.UpdateHistoricalData();
        }

        tenDayData = await dataController.MakeTenDayGraphData(numCurrency, denumCurrency);
        oneYearData = await dataController.MakeOneYearGraphData(numCurrency, denumCurrency);
        threeYearData = await dataController.MakeThreeYearGraphData(numCurrency, denumCurrency);

        await UpdateGraph();

        int tenDaydateLength = dataController.DatesForTenDays.Count;
        string tenDayStartDate = $"{dataController.DatesForTenDays[0].Day.ToString("D2")}/{dataController.DatesForTenDays[0].Month.ToString("D2")}/{dataController.DatesForTenDays[0].Year}";
        string tenDayEndDate = $"{dataController.DatesForTenDays[tenDaydateLength - 1].Day.ToString("D2")}/{dataController.DatesForTenDays[tenDaydateLength - 1].Month.ToString("D2")}/{dataController.DatesForTenDays[tenDaydateLength - 1].Year}";
        string tenDayPeriod = $"{tenDayStartDate} ~ {tenDayEndDate} (10 Days)";
        
        graphTenPeriod.Text = tenDayPeriod;


        float tenDayMax = tenDayData.Max();
        string tenDayMaxStr = tenDayMax.ToString(roundStr);
        float tenDayMin = tenDayData.Min();
        string tenDayMinStr = tenDayMin.ToString(roundStr);

        tenDayMaxLabel.Text = tenDayMaxStr;
        tenDayMinLabel.Text = tenDayMinStr;



        int oneYeardateLength = dataController.DatesForOneYear.Count;
        string oneYearStartDate = $"{dataController.DatesForOneYear[0].Month.ToString("D2")}/{dataController.DatesForOneYear[0].Year}";
        string oneYearEndDate = $"{dataController.DatesForOneYear[oneYeardateLength - 1].Month.ToString("D2")}/{dataController.DatesForOneYear[oneYeardateLength - 1].Year}";
        string oneYearPeriod = $"{oneYearStartDate} ~ {oneYearEndDate} (12 Months)";

        graphOneYearPeriod.Text = oneYearPeriod;


        float oneYearMax = oneYearData.Max();
        string oneYearMaxStr = oneYearMax.ToString(roundStr);
        float oneYearMin = oneYearData.Min();
        string oneYearMinStr = oneYearMin.ToString(roundStr);

        oneYearMaxLabel.Text = oneYearMaxStr;
        oneYearMinLabel.Text = oneYearMinStr;


        int threeYeardateLength = dataController.DatesForThreeYears.Count;
        string threeYearStartDate = $"{dataController.DatesForThreeYears[0].Month.ToString("D2")}/{dataController.DatesForThreeYears[0].Year}";
        string threeYearEndDate = $"{dataController.DatesForThreeYears[threeYeardateLength - 1].Month.ToString("D2")}/{dataController.DatesForThreeYears[threeYeardateLength - 1].Year}";
        string threeYearPeriod = $"{threeYearStartDate} ~ {threeYearEndDate} (12 Quarters)";

        graphThreeYearPeriod.Text = threeYearPeriod;


        float threeYearMax = threeYearData.Max();
        string threeYearMaxStr = threeYearMax.ToString(roundStr);
        float threeYearMin = threeYearData.Min();
        string threeYearMinStr = threeYearMin.ToString(roundStr);

        threeYearMaxLabel.Text = threeYearMaxStr;
        threeYearMinLabel.Text = threeYearMinStr;


        int apiNumber = dataController.GetApiRequestNumber();

        if (apiNumber != 0)
        {
            DisplayAlert($"{apiNumber} API Request(s) Used", "Always try the limited use of API request", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await UpdateData();
    }
}