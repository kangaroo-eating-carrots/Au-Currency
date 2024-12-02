namespace MobileProject;
using MobileProject.Models;
using MobileProject.Services;
using System.Numerics;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
//using Android.Media;
//using static Android.Security.Identity.CredentialDataResult;

public partial class CurrentRatePage : ContentPage
{
    ServiceController dataController = App.DataController;

    public CurrentRatePage()
    {
        InitializeComponent();
        InterestOneLabel.Text = $"{dataController.InterestingOne.Symbol}/{dataController.MyCurrency.Symbol}";
        InterestTwoLabel.Text = $"{dataController.InterestingTwo.Symbol}/{dataController.MyCurrency.Symbol}";

    }

    private ChartEntry[] GetChartEntries(List<float> dataList)
    {
        int entryLength = dataList.Count;
        ChartEntry[] graphEntries = new ChartEntry[entryLength];
        
        for (int i = 0; i < graphEntries.Length; i++)
        {
            graphEntries[i] = new ChartEntry(dataList[i])
            {
                Color = SKColor.Parse("#FFFFFF")
            };
        }

        return graphEntries;
    }
    private async Task TabOpen()
    {
        InterestOneLabel.Text = $"{dataController.InterestingOne.Symbol}/{dataController.MyCurrency.Symbol}";
        InterestTwoLabel.Text = $"{dataController.InterestingTwo.Symbol}/{dataController.MyCurrency.Symbol}";

        if (dataController.CheckLatestUpdate())
        {
            await dataController.CurrentRateUpdate();
        }

        float firstRate = dataController.InterestingOne.LatestRatePerUSD / dataController.MyCurrency.LatestRatePerUSD;
        string firstRateStr;

        if (firstRate >= 9999.5)
        {
            firstRateStr = firstRate.ToString("F0");
        }
        else if (firstRate >= 999.95)
        {
            firstRateStr = firstRate.ToString("F1");
        }
        else if (firstRate >= 99.995)
        {
            firstRateStr = firstRate.ToString("F2");
        }
        else if (firstRate >= 9.9995)
        {
            firstRateStr = firstRate.ToString("F3");
        }
        else
        {
            firstRateStr = firstRate.ToString("F4");
        }


        float secondRate = dataController.InterestingTwo.LatestRatePerUSD / dataController.MyCurrency.LatestRatePerUSD;
        string secondRateStr;

        if (secondRate >= 9999.5)
        {
            secondRateStr = secondRate.ToString("F0");
        }
        else if (secondRate >= 999.95)
        {
            secondRateStr = secondRate.ToString("F1");
        }
        else if (secondRate >= 99.995)
        {
            secondRateStr = secondRate.ToString("F2");
        }
        else if (secondRate >= 9.9995)
        {
            secondRateStr = secondRate.ToString("F3");
        }
        else
        {
            secondRateStr = secondRate.ToString("F4");
        }

        InterestOneRate.Text = firstRateStr;
        InterestTwoRate.Text = secondRateStr;


        if (dataController.CheckHistoricalUpdate())
        {
            await dataController.UpdateHistoricalData();
        }

        int dateLength = dataController.DatesForTenDays.Count;
        string graphStartDate = $"{dataController.DatesForTenDays[0].Day.ToString("D2")}/{dataController.DatesForTenDays[0].Month.ToString("D2")}/{dataController.DatesForTenDays[0].Year}";
        string graphEndDate = $"{dataController.DatesForTenDays[dateLength - 1].Day.ToString("D2")}/{dataController.DatesForTenDays[dateLength - 1].Month.ToString("D2")}/{dataController.DatesForTenDays[dateLength - 1].Year}";
        string graphPeriod = $"{graphStartDate} ~ {graphEndDate} (10 Days)";

        graphTenFirstPeriod.Text = graphPeriod;
        graphTenSecondPeriod.Text = graphPeriod;

        List<float> firstGraphData =  await dataController.MakeTenDayGraphData(dataController.InterestingOne, dataController.MyCurrency);
        float minValueForFirstGraph = firstGraphData.Min() * 0.95f;
        float maxValueForFirstGraph = firstGraphData.Max() * 1.05f;

        ChartEntry[] firstGraphEntries = GetChartEntries(firstGraphData);

        graphTenFirstView.Chart = new LineChart
        {
            Entries = firstGraphEntries,
            BackgroundColor = SKColor.Parse("#000000"),
            LineMode = LineMode.Straight,
            Margin = 20,
            MinValue = minValueForFirstGraph,
            MaxValue = maxValueForFirstGraph
        };

        List<float> secondGraphData = await dataController.MakeTenDayGraphData(dataController.InterestingTwo, dataController.MyCurrency);
        float minValueForSecondGraph = secondGraphData.Min() * 0.95f;
        float maxValueForSecondGraph = secondGraphData.Max() * 1.05f;

        ChartEntry[] secondGraphEntries = GetChartEntries(secondGraphData);

        graphTenSecondView.Chart = new LineChart
        {
            Entries = secondGraphEntries,
            BackgroundColor = SKColor.Parse("#000000"),
            LineMode = LineMode.Straight,
            Margin = 20,
            MinValue = minValueForSecondGraph,
            MaxValue = maxValueForSecondGraph
        };

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